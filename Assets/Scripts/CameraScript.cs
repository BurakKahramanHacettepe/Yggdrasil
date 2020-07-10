using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject player;
    public GameObject bg_controller;


    // How long the object should shake for.
    public float shakeDuration = 0f;

    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;


    Vector3 originalPos;


    float x;
    // Start is called before the first frame update
    public void miniShake()
    {
        shakeDuration = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            if (player.transform.position.x + 0.25f > x)
            {
                x = player.transform.position.x + 0.25f;

            }
            gameObject.transform.position = new Vector3(x, 0, -10);

            bg_controller.GetComponent<BG_ControllerScript>().Swap(transform.position.x);
        }
        

        originalPos = transform.position;

        if (shakeDuration > 0)
        {
            transform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shakeDuration = 0f;
            transform.localPosition = originalPos;
        }


    }

}
