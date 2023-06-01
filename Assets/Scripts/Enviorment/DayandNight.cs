
using UnityEngine;

public class DayandNight : MonoBehaviour
{
    Vector3 rot=Vector3.zero;
    
    [SerializeField]
    float degpersec=6;

    [SerializeField]
    Camera cam;

    // Update is called once per frame
    void Update()
    {
        rot.x=degpersec*Time.deltaTime;
        transform.parent.Rotate(rot,Space.World);

        transform.parent.rotation = Quaternion.AngleAxis(degpersec, transform.parent.right) * transform.parent.rotation;
        Debug.Log(transform.parent.eulerAngles);


        var lerp = Mathf.InverseLerp(-180, 180, transform.parent.eulerAngles.x);
            Debug.Log(lerp);
        cam.backgroundColor = new Color(.65f*lerp,.75f*lerp,lerp,1);
        
    }
}
