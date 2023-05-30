using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    [SerializeField]
    [Range(0.0f,10.0f)]
    float speed;

    private void Update() {
        Vector3 move_vector = player.transform.forward*speed * Input.GetAxis("Vertical") * Time.deltaTime;
        player.transform.position += (move_vector);

        player.transform.Rotate(new Vector3(0, Input.GetAxis("Horizontal")*speed * Time.deltaTime*10,0));
    }
}
