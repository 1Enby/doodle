using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType
    {
        Grass,
        Sand,
        Tree,
        Rock
    }
    [ExecuteAlways]

public class RandomTile : MonoBehaviour
{
    public TileType type = TileType.Grass;

    public Transform tile_prefab;
    
    // Start is called before the first frame update
    void Start()
    {
        
        Transform object_pool = null;
        for(int i = transform.childCount; i>0;i--)
        {
            var child = transform.GetChild(0);
            if(transform.GetChild(0).name.Contains("Grass"))
                {
                    object_pool = GameObject.Find("ObjectPools").transform.Find("Grass");
                    child.parent = object_pool;
                    child.localPosition = new Vector3(0,0,0);
                }
        }

        for(int i = transform.childCount; i>0;i--)
        {
            var child = transform.GetChild(0);
            if(transform.GetChild(0).name.Contains("Rock"))
                {
                    object_pool = GameObject.Find("ObjectPools").transform.Find("Rock");
                    child.parent = object_pool;
                    child.localPosition = new Vector3(0,0,0);
                }
        }

        for(int i = transform.childCount; i>0;i--)
        {
            var child = transform.GetChild(0);
            if(transform.GetChild(0).name.Contains("Sand"))
                {
                    object_pool = GameObject.Find("ObjectPools").transform.Find("Sand");
                    child.parent = object_pool;
                    child.localPosition = new Vector3(0,0,0);
                }
        }

        for(int i = transform.childCount; i>0;i--)
        {
            var child = transform.GetChild(0);
            if(transform.GetChild(0).name.Contains("Tree"))
                {
                    object_pool = GameObject.Find("ObjectPools").transform.Find("Tree");
                    child.parent = object_pool;
                    child.localPosition = new Vector3(0,0,0);
                }
        }
    
        switch (type)
        {
            case TileType.Grass:
                object_pool = GameObject.Find("ObjectPools").transform.Find("Grass");
                break;
            case TileType.Rock:
                object_pool = GameObject.Find("ObjectPools").transform.Find("Rock");
                break;
            case TileType.Sand:
                object_pool = GameObject.Find("ObjectPools").transform.Find("Sand");
                break;
            case TileType.Tree:
                object_pool = GameObject.Find("ObjectPools").transform.Find("Tree");
                break;
        }
        if(object_pool==null)
            return;

        object_pool.SendMessage("Remove");
        tile_prefab = object_pool.GetChild(0);

        //reparent tile prefab to tile object
        tile_prefab.parent = transform;
        //move child to prefab position
        tile_prefab.localPosition = new Vector3(0,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
