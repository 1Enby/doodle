using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WorldGenerator : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    Vector2 player_pos;

    [SerializeField]
    int tile_countX = 5;
    [SerializeField]
    int tile_countY = 5;

    Vector3 origin;
    Vector3 ray;

    [Range(0, 100)]
   
    [SerializeField]
    int num_lakes;
    [SerializeField]
    float min_lake_radius;
    [Range(0, 20)]
    [SerializeField]
    float max_lake_radius;

    [SerializeField]
    int radius = 15;

    Transform tile_pool;

    static bool GenerationComplete = false;

    [SerializeField]
    int num_mountains;

    [Range(0, 20)]
   
    [SerializeField]
    float min_mountain_radius;
    [Range(0, 20)]
    [SerializeField]
    float max_mountain_radius;



    // Start is called before the first frame update
    void Start()
    {
        if (!GenerationComplete)
        {
            StaticMap.Init(tile_countX, tile_countY, radius);
            StartCoroutine("GenerateWorld");
        }
    }

    bool water_pass = false;
    int[] n;
    int last_tile;

    private void Update()
    {
        if (!GenerationComplete)
            return;

        var tmp = player.transform.position;// + (player.transform.forward * -2);

        player_pos = new Vector2((int)tmp.x, (int)tmp.z);

        int centertile = (tile_countY * (int)player_pos.x) + (int)player_pos.y;

        if (last_tile != centertile)
        {
            PlaceTilesAroundCenterPoint(centertile, radius);
        }
        last_tile = centertile;
    }

    void RemoveAll()
    {
        tile_pool = GameObject.Find("TilePool").transform;

        for (int i = transform.childCount; i > 0; i--)
        {
            var child = transform.GetChild(0);
            child.SetParent(tile_pool, false);
        }
    }

    void RemoveAllNeighbors()
    {
        if (transform.childCount < StaticMap.current_neighbors.Length)
            return;

        for (int i = transform.childCount; i > StaticMap.current_neighbors.Length; i--)
        {
            var child = transform.GetChild(0);
            child.SetParent(tile_pool, false);
        }
    }

    Transform new_tile;
    void PlaceTilesAtPosition(int world_tile)
    {
        //check all the tiles to see if we have already place a tile in that world position
        var children = GetComponentsInChildren<RandomTile>();
        for (int i = 0; i < children.Length; i++)
        {
            if (children[i].index == world_tile)
                return;
        }
        //signal that we are grabbing a tile from the pool
        //then grab the tile
        tile_pool.SendMessage("Remove");

        if (tile_pool.childCount < 1)
            return;
        new_tile = tile_pool.GetChild(0);

        if (new_tile == null || StaticMap.the_world_map == null)
            return;

        new_tile.SetParent(transform); //parent the new tile to the world

        //reset its position
        new_tile.transform.localPosition = new Vector3(
        StaticMap.the_world_map[world_tile].x_pos,
         StaticMap.the_world_map[world_tile].y_pos,
          StaticMap.the_world_map[world_tile].z_pos
        );

        var tilescript = new_tile.GetComponent<RandomTile>();
        tilescript.index = world_tile;

        tilescript.type = StaticMap.the_world_map[world_tile].t_type;

        new_tile.SendMessage("ParentTheVisual"); //update the visual

    }

    IEnumerator GenerateWorld()
    {
        tile_pool = GameObject.Find("TilePool").transform;

        player_pos = new Vector2();

        //return all previous children to pool
        RemoveAllNeighbors();

        //INITIALIZE ALL TILES IN THE STATIC MAP
        for (int x = 0; x < tile_countX; x++)
        {
            for (int y = 0; y < tile_countY; y++)
            {
                //set a random height and a position based on the x and y variables.
                //and the other generated properties of the tile!
                var height = Random.Range(0.0f, 0.0f);
                Vector3 grid_pos = new Vector3(x, height, y);
                var rand_type = (TileType)Random.Range(0, 1); //select the tile type randomly, it's just a number between 0 and 2
                var index = (tile_countY * x) + y;

                StaticMap.the_world_map[index] = new map_point(rand_type, grid_pos.x, grid_pos.y, grid_pos.z);
            }
        }


        ///////GENERATE WATER
        for (int l = 0; l < num_lakes; l++)
        {
            //pick a random point in the map
            int index = Random.Range(0, StaticMap.the_world_map.Length);

            float lake_size = Random.Range(min_lake_radius, max_lake_radius);

            int[] n = StaticMap.GetNeighborsAtRadius(index, (int)lake_size + Random.Range(1, 5), StaticMap.width, StaticMap.height);
            for (int i = 0; i < n.Length; i++)
            {
                if (n[i] != -100)
                {
                    StaticMap.the_world_map[n[i]].t_type = TileType.Sand;
                }

            }

            n = StaticMap.GetNeighborsAtRadius(index, (int)lake_size, StaticMap.width, StaticMap.height);
            for (int i = 0; i < n.Length; i++)
            {
                if (n[i] != -100)
                {
                    StaticMap.the_world_map[n[i]].y_pos = -0.1f;
                    StaticMap.the_world_map[n[i]].t_type = TileType.Water;
                }
            }
        }

        for (int l = 0; l < num_mountains; l++)
        {
            //pick a random point in the map
            int index = Random.Range(0, StaticMap.the_world_map.Length);

            float mountain_size = Random.Range(min_mountain_radius, max_mountain_radius);

            int[] n = StaticMap.GetNeighborsAtRadius(index, (int)mountain_size + Random.Range(1, 5), StaticMap.width, StaticMap.height);
            for (int i = 0; i < n.Length; i++)
            {
                if (n[i] != -100)
                {
                    StaticMap.the_world_map[n[i]].t_type = TileType.Rock;
                }

            }

            n = StaticMap.GetNeighborsAtRadius(index, (int)mountain_size, StaticMap.width, StaticMap.height);
            for (int i = 0; i < n.Length; i++)
            {
                if (n[i] != -100)
                {
                    StaticMap.the_world_map[n[i]].y_pos = 0.2f;
                    StaticMap.the_world_map[n[i]].t_type = TileType.Rock;
                }
            }
        }
        /*
        //Water generation with spherecasts
        origin = new Vector3(Random.Range(centerpointofchunk.x - radius, centerpointofchunk.x + radius),
        100,
        Random.Range(centerpointofchunk.y - radius, centerpointofchunk.y + radius));

        origin = new Vector3(centerpointofchunk.x, 100, centerpointofchunk.y);
        ray = -Vector3.up * 1000;

        RaycastHit[] hits;

        hits = Physics.SphereCastAll(
        origin,
        Random.Range(min_lake_radius, max_lake_radius),
        ray,
        5000);

        Debug.DrawRay(origin,ray,Color.red,10.0f);

        foreach (RaycastHit h in hits)
        {
            var ts = h.collider.GetComponentInParent<RandomTile>();
            if (ts == null)
                continue;

            ts.type = TileType.Water;
            h.collider.GetComponentInParent<RandomTile>().SendMessage("ParentTheVisual");
            var pt = new Vector2(ts.transform.localPosition.x, ts.transform.transform.localPosition.z);
            var ind = StaticMap.CoordsAsIndex((int)pt.x, (int)pt.y);
            StaticMap.the_world_map[ind].t_type = TileType.Water;
        }
        */

        //print out the types of the static map
        for (int i = 0; i < StaticMap.the_world_map.Length; i++)
        {
            // Debug.Log(StaticMap.the_world_map[i].t_type);
        }
        GenerationComplete = true;
        yield break;
    }

    void PlaceTilesAroundCenterPoint(int centertile, int rad)
    {
        n = StaticMap.GetNeighborsAtRadius(centertile, rad, tile_countX, tile_countY);
        if (StaticMap.relationship_array == null)
            return;
        RemoveAllNeighbors();
        for (int i = 0; i < n.Length; i++)
        {
            if (n[i] == -100)
            {
                continue;
            }

            StaticMap.relationship_array[n[i]] = 0;
            PlaceTilesAtPosition(n[i]);

        }
    }
}