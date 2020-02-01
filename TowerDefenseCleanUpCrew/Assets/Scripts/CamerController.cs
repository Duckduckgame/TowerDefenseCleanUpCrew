using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerController : MonoBehaviour
{

    Camera mainCam;
    GameObject player;
    Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
        player = GameObject.FindGameObjectWithTag("Player");
        offset = new Vector3(0, 50, 0); //player.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        mainCam.orthographicSize += Input.mouseScrollDelta.y * -1;
        mainCam.orthographicSize = Mathf.Clamp(mainCam.orthographicSize, 20, 55);
        transform.position = player.transform.position + offset;
    }
}
