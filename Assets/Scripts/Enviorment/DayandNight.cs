
using UnityEngine;

public class DayandNight : MonoBehaviour
{
    Vector3 rot=Vector3.zero;
    
    [SerializeField]
    Camera cam;

    Color daylight = new Color(.33f,.33f,1.0f,1.0f);
    Color nightlight = new Color(0.0f,0.0f,0.25f,1.0f);

    // Update is called once per frame
    void Update()
    {
       // rot.x=degpersec*Time.deltaTime;
       // transform.parent.Rotate(rot,Space.World);

       // transform.parent.rotation = Quaternion.AngleAxis(degpersec, transform.parent.right) * transform.parent.rotation;
       // Debug.Log(transform.parent.eulerAngles);

        float rot = transform.eulerAngles.x;
        float lerp = 0;

        if(rot<180){
            lerp = Mathf.InverseLerp(0, 180, rot);
        }
        else if(rot >=180)
        {
            lerp = Mathf.InverseLerp(360, 180, rot);
        }

       cam.backgroundColor = Color.Lerp(daylight,nightlight,lerp);

        
    }
}
