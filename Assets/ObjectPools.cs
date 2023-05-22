using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class ObjectPools : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.childCount<2)
        {
            //copy child if none left
            var go = transform.GetChild(0).gameObject;
            GameObject tmp = Instantiate(go, transform)as GameObject;
            
            for(int i =0;i<50;i++)
                tmp.transform.localPosition = new Vector3(0,0,0);
        }

    }

    void Remove()
    {
       Debug.Log("hola");
       if(transform.childCount<2)
        {
            //copy child if none left
            var go = transform.GetChild(0).gameObject;
            GameObject tmp = Instantiate(go, transform)as GameObject;
            
            for(int i =0;i<50;i++)
                tmp.transform.localPosition = new Vector3(0,0,0);
        }

    }
}
