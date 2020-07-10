using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;

    public float runSpeed;
    public float jumpForce;

    void Start()
    {
        player.GetComponent<PlayerMovementScript>().runSpeed = runSpeed;
        player.GetComponent<CharacterController2D>().m_JumpForce = jumpForce;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
