using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFinishScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControllerScript>().Finish();
            col.gameObject.GetComponent<PlayerMovementScript>().enabled = false;
        }
    }
    
}
