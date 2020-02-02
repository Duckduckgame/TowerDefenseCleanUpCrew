using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerController : MonoBehaviour
{

    Camera mainCam;
    GameObject player;
    Vector3 offset;

    LevelManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<LevelManager>();
        mainCam = Camera.main;
        player = GameObject.FindGameObjectWithTag("Player");
        offset = new Vector3(0, 50, 0); //player.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.crntState == LevelManager.GameState.Clean)
        {
            if(player == null)
                player = GameObject.FindGameObjectWithTag("Player");
            mainCam.orthographicSize += Input.mouseScrollDelta.y * -1;
            mainCam.orthographicSize = Mathf.Clamp(mainCam.orthographicSize, 20, 55);
            transform.position = player.transform.position + offset;
        }
    }
}
