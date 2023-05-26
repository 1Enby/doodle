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

    [Range(0, 20)]
    [SerializeField]
    float min_lake_radius;
    [Range(0, 20)]
    [SerializeField]
    float max_lake_radius;

    [SerializeField]
    int radius = 15;

    Transform tile_pool;

    [SerializeField]
    public TextMeshProUGUI debug_text;
    private void Awake()
    {
        StaticMap.Init(tile_countX, tile_countY, radius);
    }
    // Start is called before the first frame update
    void Start()
    {
        tile_pool = GameObject.Find("TilePool").transform;

        player_pos = new Vector2();

        //return all previous children to pool
        RemoveAll();

        //get the pool of available tiles
        //use a nested for loop to make the grid.
        for (int x = 0; x < tile_countX; x++)
        {
            for (int y = 0; y < tile_countY; y++)
            {
                //set a random height and a position based on the x and y variables.
                //and the other generated properties of the tile!
                var height = Random.Range(0.0f, 1.0f);
                Vector3 grid_pos = new Vector3(x, height, y);
                var rand_type = (TileType)Random.Range(0, 3); //select the tile type randomly, it's just a number between 0 and 2

                //map_point point = StaticMap.the_world_map[i];

                //###############//
                var index = (tile_countY * x) + y;
                StaticMap.the_world_map[index] = new map_point(rand_type, grid_pos.x, grid_pos.y, grid_pos.z);
                //##############//
            }
        }
    }

    bool water_pass = false;
    int[] n;
    int last_tile;

    private void Update()
    {
        var tmp = player.transform.position;// + (player.transform.forward * -2);

        player_pos = new Vector2((int)tmp.x, (int)tmp.z);

        int centertile = (tile_countY * (int)player_pos.x) + (int)player_pos.y;

        if (last_tile != centertile)
        {
            n = StaticMap.GetNeighborsAtRadius(centertile, radius, tile_countX, tile_countY);

            if (StaticMap.relationship_array == null)
                return;

            RemoveAll();

            for (int i = 0; i < n.Length; i++)
            {
                if (n[i] == -100)
                {
                    continue;
                }

                StaticMap.relationship_array[n[i]]--;
                PlaceTilesAtPosition(n[i]);

                //StaticMap.ResetNeighbors();
            }
        }
        last_tile = centertile;
    }

    void GenWater()
    {
        for (int i = 0; i < num_lakes; i++)
        {
            //Water generation with spherecasts
            origin = new Vector3(Random.Range(0, tile_countX), 100, Random.Range(0, tile_countY));
            ray = -Vector3.up * 1000;

            RaycastHit[] hits;

            hits = Physics.SphereCastAll(
            origin,
            Random.Range(min_lake_radius, max_lake_radius),
            ray,
            5000);

            foreach (RaycastHit h in hits)
            {
                var ts = h.collider.GetComponentInParent<RandomTile>();
                if (ts == null)
                    continue;

                ts.type = TileType.Water;
                h.collider.GetComponentInParent<RandomTile>().SendMessage("ParentTheVisual");
            }
        }
    }

    void RemoveAll()
    {
        if (transform.childCount < StaticMap.current_neighbors.Length)
            return;

        for (int i = transform.childCount; i > StaticMap.current_neighbors.Length; i--)
        {
            var child = transform.GetChild(0);
            child.SetParent(tile_pool, false);
        }
    }

    void PlaceTilesAtPosition(int world_tile)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<RandomTile>().index == world_tile)
                return;
        }
        //signal that we are grabbing a tile from the pool
        //then grab the tile
        tile_pool.SendMessage("Remove");

        Transform new_tile;

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

        tilescript.type = StaticMap.the_world_map[world_tile].type;

        new_tile.SendMessage("ParentTheVisual"); //update the visual

    }
}