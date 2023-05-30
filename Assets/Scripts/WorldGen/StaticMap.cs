using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public struct map_point
{
    public TileType t_type;
    public float x_pos;
    public float y_pos;
    public float z_pos;
    public bool neighbor;


    public map_point(TileType t, float x, float y, float z)
    {
        t_type = t;
        x_pos = x;
        y_pos = y;
        z_pos = z;
        neighbor = false;
    }
}

public class StaticMap
{
    public static map_point[] the_world_map;
    public static int[] current_neighbors;
    //the relationship array contains 0 at each index where there is no neighbor and 1 at the index where there is a neighbor
    public static int[] relationship_array;

    public static int width;
    public static int height;

    public static int rad;
    public static void Init(int w, int h, int radius)
    {
        var max_map_size = w * h;
        the_world_map = new map_point[max_map_size];

        for (int i = 0; i < max_map_size; i++) //initalize the map as all GRASS
        {
            StaticMap.the_world_map[i] = new map_point(TileType.Grass, 0, 0, 0);
        }

        width = w;
        height = h;

        rad = radius;

        int length = w * h;

        relationship_array = new int[length];
        for (int i = 0; i < length; i++)
        {
            relationship_array[i] = 0;
        }

        GetNeighborsAtRadius(0, rad, w, h);
    }
    public static Vector2 IndexAsCoords(int the_val)
    {
        if (width == 0 || height == 0)
            return new Vector2(0, 0);

        return new Vector2(the_val % width, (int)the_val / height);
    }
    public static int CoordsAsIndex(int x, int y)
    {
        int index = x * width + y;
        return index;
    }

    public static int[] GetNeighborsAtRadius(int val, int rad, int w, int h)
    {
        int length = (int)Mathf.Pow((2 * rad + 1), 2);
        int extents = (int)Mathf.Sqrt(length);

        int[] l = new int[length];
        int[] wm = new int[length];
        int[] rm = new int[length];

        for (int x = 0; x < length; x++)
        {
            rm[x] = (int)(x / extents) - rad;
            wm[x] = (x % extents - rad) * w;
            int sub = rm[x] + wm[x];
            int neighbor = val + sub;
            l[x] = neighbor;


            // cannot have a negative bound
            if (neighbor < 0)
                l[x] = -100;
            //cannot have a bound greater than the grid size
            if (neighbor > w * h - 1)
                l[x] = -100;

            // check left edge
            bool left_edge = (IndexAsCoords(val)[0] + rad < IndexAsCoords(neighbor)[0]);

            if (val % w - rad <= 0 && left_edge)
                l[x] = -100;

            // check right edge
            bool right_edge = (IndexAsCoords(val)[0] - rad > IndexAsCoords(neighbor)[0]);

            if ((int)(val % w) - rad >= 0 && right_edge)
                l[x] = -100;
        }
        current_neighbors = l;
        return l;
    }


}




