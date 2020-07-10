using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float range = 10f;
    public float speed = 10f;
    public bool direction = true;
    public float dmg = 34f;
    public float y;

    private Vector2 target;
    private Vector2 position;

    private GameControllerScript gameController;

    
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControllerScript>();
        position = gameObject.transform.position;

        if (direction)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            target = new Vector2(position.x + range, y);
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = true;
            target = new Vector2(position.x - range, y);
        }

    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;

        // move sprite towards the target location
        transform.position = Vector2.MoveTowards(transform.position, target, step);

        if(Vector2.Distance(transform.position, target)< 0.1f)
        {
            Destroy(gameObject);
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.tag.Equals("Enemy"))
        {
            bool isDead = col.gameObject.GetComponent<EnemyScript>().Hurt(dmg);
            if (isDead)
            {
                gameController.kill_count += 1;
            }

            Destroy(gameObject);

        }
    }
}