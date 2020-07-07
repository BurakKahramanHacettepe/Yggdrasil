using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject player;
    public GameObject bg_controller;

    float x;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x+0.5f>x)
        {
            x = player.transform.position.x+0.5f;

        }
        gameObject.transform.position = new Vector3(x,0,-10);

        bg_controller.GetComponent<BG_ControllerScript>().Swap(transform.position.x);

    }

}
