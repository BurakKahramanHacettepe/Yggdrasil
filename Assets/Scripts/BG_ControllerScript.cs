using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG_ControllerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bg1;
    public GameObject bg2;

    private float lim_x = 2.487f;
    public void Swap(float x)
    {
        GameObject behind;
        if (x>lim_x)
        {
            if (bg1.transform.position.x > bg2.transform.position.x)
            {
                behind = bg2;
            }
            else
            {
                behind = bg1;
            }

            behind.transform.position += 2 * new Vector3(2.487f, 0, 0);
            lim_x += 2.487f;
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
