using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.UIElements;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{

    public CharacterController2D controller;
    public Animator animator;

    public float runSpeed = 40f;


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
            if (GetComponent<PlayerEssentials>().melee_cd >= 1.0f)
            {
                melee = true;
                animator.SetTrigger("Melee");
            }

        }
        if (Input.GetMouseButtonDown(1))
        {
            if (GetComponent<PlayerEssentials>().cd>= 1.0f)
            {
                ranged = true;
                animator.SetTrigger("Ranged");
            }
            else
            {
                GetComponents<AudioSource>()[1].Play();
            }
           

        }

       

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