using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//defining the types of tiles we can have.
public enum TileType
{
    Grass,
    Water,
    Rock,
    Tree
}


public class RandomTile : MonoBehaviour
{
    [SerializeField]
    public TileType type = TileType.Grass;

    [SerializeField]
    public Transform tile_prefab;
    Transform object_pool;
    Transform tile_object_pool = null;

    public bool ResetNext = false;
    void Init()
    {
        type = TileType.Grass;
    }

    public int index;

    private void Start()
    {
        tile_object_pool = GameObject.Find("TilePool").transform;
    }
    private void Update()
    {
        if (StaticMap.relationship_array == null)
            return;

        if (index > StaticMap.relationship_array.Length)
            return;

        ResetNext = StaticMap.relationship_array[index] == 0;


    }
    void Reset()
    {
        //send child objects back to the object pool they belong in.
        for (int i = transform.childCount; i > 0; i--)
        {
            var child = transform.GetChild(0);

            if (child.name.Contains("Water"))
            {
                object_pool = GameObject.Find("ObjectPools").transform.Find("WaterTiles");

            }
            if (child.name.Contains("Rock"))
            {
                object_pool = GameObject.Find("ObjectPools").transform.Find("RockTiles");

            }
            if (child.name.Contains("Grass"))
            {
                object_pool = GameObject.Find("ObjectPools").transform.Find("GrassTiles");

            }
            if (child.name.Contains("Tree"))
            {
                object_pool = GameObject.Find("ObjectPools").transform.Find("TreeTiles");

            }
            child.SetParent(object_pool, false);
            //child.localPosition = new Vector3(0, 0, 0);
        }
    }


    //this is the funciton that swaps the current tile for a different tile
    void ParentTheVisual()
    {
        //return the current tile to the pool
        Reset();

        if (StaticMap.relationship_array[index] == 1)
            return;

        //grab the tile from the pool
        switch (type)
        {
            case TileType.Grass:
                object_pool = GameObject.Find("ObjectPools").transform.Find("GrassTiles");
                break;
            case TileType.Rock:
                object_pool = GameObject.Find("ObjectPools").transform.Find("RockTiles");
                break;
            case TileType.Water:
                object_pool = GameObject.Find("ObjectPools").transform.Find("WaterTiles");
                break;
            case TileType.Tree:
                object_pool = GameObject.Find("ObjectPools").transform.Find("TreeTiles");
                break;
        }

        if (object_pool == null)
            return;

        object_pool.SendMessage("Remove");

        if (object_pool.childCount < 1)
            return;

        tile_prefab = object_pool.GetChild(0);

        //reparent the tile prefab to our tile object. 
        tile_prefab.SetParent(transform, false);
        //move the child prefab to our position.
        //tile_prefab.localPosition = new Vector3(0, 0, 0);
        StaticMap.relationship_array[index] = 1;
    }

    private void OnTransformParentChanged()
    {
        if (transform.parent.name.Contains("Pool"))
            Reset();
    }
}
