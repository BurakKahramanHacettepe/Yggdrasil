using System;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
	public float m_JumpForce = 400f;							// Amount of force added when the player jumps.
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;							// Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.

	[Header("Projectile")]
	[Space]

	public GameObject projectile;


	const float k_GroundedRadius = .1f; // Radius of the overlap circle to determine if grounded
	private bool m_Grounded;            // Whether or not the player is grounded.

	

	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;


	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }


	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

		
	}

	private void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;
		m_Grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}
	}


	public void Move(float move, bool jump)
	{
		

		//only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl)
		{
			if (m_Rigidbody2D.velocity.x > 0.1f)
			{
			
				//GetComponents<AudioSource>()[2].Play(); //Footsteps

			}

			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);

			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

			if (move > 0 && !m_FacingRight)
			{
				Flip();
			}

			else if (move < 0 && m_FacingRight)
			{
				Flip();
			}
		}

		if (m_Grounded && jump)
		{
			m_Grounded = false;
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
		}
	}
	public void Attack(bool melee, bool ranged)
	{
		if (melee)
		{
			Melee();

		}
		else if (ranged)
		{
			Invoke("Ranged", 0.3f);
			//Ranged();
		}
	}

	private void Ranged()
	{
		Vector3 offset = m_FacingRight ? new Vector3(0.2f,0,0) : new Vector3(-0.2f, 0, 0);
		GameObject p = Instantiate(projectile, gameObject.transform.position + offset, Quaternion.identity);
		p.GetComponent<ProjectileScript>().y = gameObject.transform.position.y;
		p.GetComponent<ProjectileScript>().direction = m_FacingRight;
		GetComponent<PlayerEssentials>().cd = 0.0f;

	}

	private void Melee()
	{
		GetComponent<PlayerEssentials>().melee_cd = 0.0f;
		GetComponent<AudioSource>().Play();
	}

	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	
}
