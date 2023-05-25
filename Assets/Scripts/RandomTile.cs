using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
public enum TileType
{
    Grass,
    Water,
    Tree,
    Rock
}
[ExecuteAlways]

public class RandomTile : MonoBehaviour
{
    public TileType type = TileType.Grass;

    public Transform tile_prefab;

    // Start is called before the first frame update
    void ParentTheVisual()
    {
        Transform object_pool = null;

        switch (type)
        {
            case TileType.Grass:
                object_pool = GameObject.Find("ObjectPools").transform.Find("Grass");
                break;
            case TileType.Rock:
                object_pool = GameObject.Find("ObjectPools").transform.Find("Rock");
                break;
            case TileType.Water:
                object_pool = GameObject.Find("ObjectPools").transform.Find("Water");
                break;
            case TileType.Tree:
                object_pool = GameObject.Find("ObjectPools").transform.Find("Tree");
                break;
        }
        if (object_pool == null)
            return;

        object_pool.SendMessage("Remove");

        if(object_pool.childCount<1)
            return;
            
        tile_prefab = object_pool.GetChild(0);

        //reparent tile prefab to tile object
        tile_prefab.parent = transform;
        //move child to prefab position
        tile_prefab.localPosition = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Reset()
    {
        Transform object_pool = null;
        for (int i = transform.childCount; i > 0; i--)
        {
            var child = transform.GetChild(0);
            if (transform.GetChild(0).name.Contains("Grass"))
            {
                object_pool = GameObject.Find("ObjectPools").transform.Find("Grass");

            }
            if (transform.GetChild(0).name.Contains("Rock"))
            {
                object_pool = GameObject.Find("ObjectPools").transform.Find("Rock");

            }
            if (transform.GetChild(0).name.Contains("Water"))
            {
                object_pool = GameObject.Find("ObjectPools").transform.Find("Water");

            }
            if (transform.GetChild(0).name.Contains("Tree"))
            {
                object_pool = GameObject.Find("ObjectPools").transform.Find("Tree");

            }
            child.parent = object_pool;
            child.localPosition = new Vector3(0, 0, 0);
        }
    }
}

*/

