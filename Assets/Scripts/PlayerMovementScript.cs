using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{

    public CharacterController2D controller;
    public Animator animator;

    public float runSpeed = 40f;

    public float hp = 100f;

    float horizontalMove = 0f;
    bool jump = false;
    bool melee = false;
    bool ranged = false;
    // Update is called once per frame
    void Update()
    {

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetTrigger("Jump");
        }
        if (Input.GetMouseButtonDown(0))
        {
            melee = true;
            animator.SetTrigger("Melee");
        }
        if (Input.GetMouseButtonDown(1))
        {
            ranged = true;
            animator.SetTrigger("Ranged");

        }



    }

   public void Hurt(float dmg)
    {
        hp -= dmg;
        if (hp<0)
        {
            GetComponent<BoxCollider2D>().enabled = false;
        }
        animator.SetFloat("Hp", hp);
        animator.SetTrigger("Hurt");

    }

    void FixedUpdate()
    {
        // Move our character
        controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
        jump = false;

        controller.Attack(melee, ranged);
        melee = false;
        ranged = false;
    }
}