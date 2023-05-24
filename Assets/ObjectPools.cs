using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteAlways]
public class ObjectPools : MonoBehaviour
{
   
    [SerializeField] 
    GameObject prefab;
    void Remove()
    {
       
       GameObject tmp;
       Vector3 origin = new Vector3(0,0,0);
       
       if(transform.childCount<1)
        {
            tmp = PrefabUtility.InstantiatePrefab(prefab, transform)as GameObject; 
        }

        /*
        var go = transform.GetChild(0).gameObject;

        go.name = gameObject.name.Split("Pool")[0];

        if (tmp == null)
            Debug.LogWarning("Issue with tile instantiating");

        go.transform.localPosition = origin;
        */
    }


}
