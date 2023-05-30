using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using TMPro;

public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    GameObject prefab;

    [SerializeField]
    int limit;

    int count;

    private void Start()
    {
        if (name == "TilePool")
            limit = (int)Mathf.Pow((2 * StaticMap.rad + 1), 2);
        count = 0;
    }
    Vector3 origin = new Vector3(0, 0, 0);
    GameObject tmp;

    void Remove()
    {
        if (transform.childCount < 1 && count < limit)
        {
            tmp = PrefabUtility.InstantiatePrefab(prefab, transform) as GameObject;
            count++;
        }
    }

    void RemoveAll()
    {
        for (int i = transform.childCount; i > 0; i--)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
    }
}
