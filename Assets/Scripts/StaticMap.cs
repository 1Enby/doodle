using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct map_point
{
    public TileType type;
    public float x_pos;
    public float y_pos;
    public float z_pos;

    public map_point(TileType t, float x, float y, float z)
    {
        type = t;
        x_pos = x;
        y_pos = y;
        z_pos = z;
    }
}

public class StaticMap
{
    public static map_point[] the_world_map;

    public static Vector2 IndexAsCoords(int the_val, int width, int height)
    {
        return new Vector2(the_val % width, (int)the_val / height);
    }
    public static int CoordsAsIndex(int x, int y, int w, int h)
    {
        int index = x * w + y;
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
            // cannot be a neighbor with yourself
            if (neighbor == val)
                l[x] = -100;

            // cannot have a negative bound
            if (neighbor < 0)
                l[x] = -100;
            //cannot have a bound greater than the grid size
            if (neighbor > w * h - 1)
                l[x] = -100;

            // check left edge
            bool left_edge = (IndexAsCoords(val, w, h)[0] + rad < IndexAsCoords(neighbor, w, h)[0]);


            if (val % w - rad <= 0 && left_edge)
                l[x] = -100;

            // check right edge
            bool right_edge = (IndexAsCoords(val, w, h)[0] - rad > IndexAsCoords(neighbor, w, h)[0]);

            if ((int)(val % w) - rad >= 0 && right_edge)
                l[x] = -100;
        }
        return l;
    }
}




