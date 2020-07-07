using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float dmg = 20f;
    void Start()
    {
        Physics2D.IgnoreCollision(GameObject.FindGameObjectWithTag("Collider").GetComponent<BoxCollider2D>(), GetComponent<CircleCollider2D>());

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.tag.Equals("Player"))
        {
            col.gameObject.GetComponent<PlayerMovementScript>().Hurt(dmg);
            Debug.Log(col.gameObject.GetComponent<PlayerMovementScript>().hp);

        }
    }
}
