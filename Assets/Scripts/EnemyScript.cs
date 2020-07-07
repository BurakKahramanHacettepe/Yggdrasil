using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Animator animator;

    public bool Moving = false;
    public bool Attacking = false;
    public float speed = 0.8f;
    // Start is called before the first frame update
    private bool m_FacingRight = true;
    private RaycastHit2D hit;
    private GameObject cam;
    private GameObject melee;

    void Start()
    {
        melee = transform.GetChild(1).gameObject;
        melee.SetActive(false);

        cam = GameObject.FindGameObjectWithTag("MainCamera");
        Physics2D.IgnoreCollision(cam.GetComponentInChildren<BoxCollider2D>(), GetComponent<BoxCollider2D>());
        StartCoroutine("Guard");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 direction = m_FacingRight ? Vector3.right : Vector3.left;
 
        hit = Physics2D.Raycast(transform.position- 0.1f *direction, direction);
        Debug.DrawRay(transform.position - 0.1f * direction, direction, Color.green);
        if ((hit.collider != null) && (hit.collider.gameObject.tag.Equals("Player")))
        {
            if (hit.distance <= 1.0f)
            {
                Moving = true;
                animator.SetBool("Moving", true);
            }
          
        }
       
    }
    private void Update()
    {
        if (Vector2.Distance(transform.position, cam.transform.position)>3.0f )
        {
            Destroy(gameObject);
        }
        if (Moving && hit.collider.gameObject.tag.Equals("Player"))
        {
            if (Vector2.Distance(transform.position, hit.collider.gameObject.transform.position)<0.3f)
            {
                melee.SetActive(true);
                Attacking = true;
                animator.SetBool("Attacking", true);

            }
            transform.position = Vector2.MoveTowards(transform.position, hit.collider.gameObject.transform.position, speed * Time.deltaTime);

        }
        else if((hit.collider != null) && !hit.collider.gameObject.tag.Equals("Player"))
        {
            melee.SetActive(false);

            animator.SetBool("Moving", false);
            animator.SetBool("Attacking", false);
            Moving = false;
        }
    }

    IEnumerator Guard()
    {
        while (!Moving)
        {
            m_FacingRight = !m_FacingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
            yield return new WaitForSeconds(3f);
        }
        
        
    }
}
