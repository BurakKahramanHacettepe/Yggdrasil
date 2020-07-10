using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float dmg = 51f;
    public GameControllerScript gameController;

    void Start()
    {
        
    }

    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.tag.Equals("Enemy"))
        {
            bool isDead = col.gameObject.GetComponent<EnemyScript>().Hurt(dmg);
            GetComponent<AudioSource>().Play();
            if (isDead)
            {
                gameController.kill_count += 1;
            }
            Debug.Log(col.gameObject.GetComponent<EnemyScript>().hp);

        }
    }
}
