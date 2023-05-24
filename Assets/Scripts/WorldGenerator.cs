using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteAlways]
public class WorldGenerator : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    GameObject player;
    Vector2 player_pos;

    [SerializeField]
    int cols = 5;
    [SerializeField]

    int rows = 5;

    [SerializeField]
    int tWidth = 1;
    [SerializeField]
    int tLength = 1;


    void Start()
    {
        player_pos = new Vector2();

        Transform tile_pool = GameObject.Find("TilePool").transform;

        var max_map_size = rows * cols;
        StaticMap.the_world_map = new map_point[max_map_size];
        for (int i = 0; i < max_map_size; i++) // Set map as all GRASS
        {
            StaticMap.the_world_map[i] = new map_point(TileType.Grass, 0, 0, 0);
        }

        RemoveAll();

        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < cols; y++)
            {
                var height = Random.Range(0.0f, 1.0f);
                Vector3 grid_pos = new Vector3(x, height, y);
                var rand_type = (TileType)Random.Range(0, 4);


                var index = (rows * x) + y;
                StaticMap.the_world_map[index] = new map_point(rand_type, grid_pos.x, grid_pos.y, grid_pos.z);

            }
        }
        player_pos = new Vector2((int)player.transform.position.x, (int)player.transform.position.z);
        int centertile = (rows * (int)player_pos.x) + (int)player_pos.y;
        int[] n = StaticMap.GetNeighborsAtRadius(centertile, 2, rows, cols);
        for (int i = 0; i < n.Length; i++)
        {
            if (n[i] == -100)
                continue;
            PlaceTilesAtPosition(n[i]);
        }




    }
    void RemoveAll()
    {
        Transform tile_pool = GameObject.Find("TilePool").transform;

        for (int i = transform.childCount; i > 0; i--)
        {
            var origin = new Vector3(0, 0, 0);
            var child = transform.GetChild(0);
            child.parent = tile_pool;
            child.localPosition = origin;
            child.GetComponent<RandomTile>().SendMessage("Reset");

        }
    }

    void Update()
    {
        player_pos = new Vector2((int) player.transform.position.x, (int)player.transform.position.z);
        int centertile = (rows * (int)player_pos.x) + (int)player_pos.y;
        int[] n = StaticMap.GetNeighborsAtRadius(centertile, 2, rows, cols);
        for (int i = 0; i < n.Length; i++)
        {
            if(n[i]==-100)
            continue;
            PlaceTilesAtPosition(n[i]);
        }

    }


    void PlaceTilesAtPosition(int world_tile)
    {
        Transform tile_pool = GameObject.Find("TilePool").transform;

        tile_pool.SendMessage("Remove");
        var new_tile = tile_pool.GetChild(0);
        new_tile.parent = transform;


        new_tile.localPosition = new Vector3(
        StaticMap.the_world_map[world_tile].x_pos,
            StaticMap.the_world_map[world_tile].y_pos,
                StaticMap.the_world_map[world_tile].z_pos
                );


        RandomTile tilescript = new_tile.GetComponent<RandomTile>();

        tilescript.type = StaticMap.the_world_map[world_tile].type;
        new_tile.SendMessage("ParentTheVisual");
    }
}

