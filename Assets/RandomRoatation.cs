using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRoatation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Vector3 euler = transform.eulerAngles;

     euler.y = Random.Range(0f, 360f);

     transform.eulerAngles = euler;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
