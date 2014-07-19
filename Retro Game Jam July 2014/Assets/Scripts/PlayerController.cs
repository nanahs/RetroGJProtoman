using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {
	
	public float moveSpeed = 5f;
	public float jumpForce = 3f;
	
	private float h;
	private float v;
	private bool facingRight = false;
	
	private Transform groundCheck;
	private Transform ceilingCheck;
	private float groundedRadius = .1f;
	private float ceilingRadius = .1f;
	public bool grounded = false;
	public LayerMask whatIsGround;
	
	private bool jump = false;
	
	
	void Start () {
		
		// Setting up references.
		groundCheck = transform.Find("GroundCheck");
		ceilingCheck = transform.Find("CeilingCheck");
		
	}
	
	void Update () {
		
		if(Input.GetKeyDown(KeyCode.Space) && (grounded) ){
			jump = true;
		}
		
		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundedRadius, whatIsGround);
		
	}
	
	void FixedUpdate(){
		
		h = Input.GetAxis("Horizontal");
		v = Input.GetAxis("Vertical");
		
		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundedRadius, whatIsGround);
		
		//Sets the direction that the player needs to face based on the movement
		if(h > 0 && !facingRight){
			FlipSprite();
		}else if(h < 0 && facingRight){
			FlipSprite();
		}
		
		//Set Play animations
		rigidbody2D.velocity = new Vector2(h * moveSpeed, rigidbody2D.velocity.y);
		
		
		// If the player should jump...
		if (grounded && jump) {
			// Add a vertical force to the player.
			rigidbody2D.AddForce(new Vector2(0f, jumpForce));
			jump = false;
		}
		
	}
	
	private void FlipSprite(){
		
		facingRight = !facingRight;
		
		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		
		transform.localScale = theScale;
		
	}
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
}