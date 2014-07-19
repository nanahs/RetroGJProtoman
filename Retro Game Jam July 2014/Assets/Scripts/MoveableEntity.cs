using UnityEngine;
using System.Collections;

public class MoveableEntity : MonoBehaviour {

	public GameObject smallShot;

	public float moveSpeed = 5f;
	public float jumpForce = 5f;

	protected float h;
	protected float v;
	public bool facingRight = true;

	private Transform ceilingCheck;
	private Transform groundCheck;
	private Transform bulletSpawn;
	private float groundedRadius = .1f;
	private float ceilingRadius = .1f;
	public bool grounded = false;
	public LayerMask whatIsGround;

	private bool jump = false;
	private bool wantsToJump = false;

	protected bool canShoot = true;
	private bool shoot = false;
	protected bool hasAttacked = false;
	private float attackCoolDown = 2f;
	private float attackTimer = 0f;

	public virtual void Start() {

		// Setting up references.
		groundCheck = transform.Find("GroundCheck");
		ceilingCheck = transform.Find("CeilingCheck");
		bulletSpawn = transform.Find("BulletSpawn");
	
	}

	public virtual void Update() {

		if(hasAttacked){
			attackTimer += Time.deltaTime;
		}

		if(attackTimer > attackCoolDown){
			hasAttacked = false;
			attackTimer = 0f;
		}
	
	}

	public virtual void FixedUpdate(){
		
		//h = Input.GetAxis("Horizontal");
		//v = Input.GetAxis("Vertical");
		
		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundedRadius, whatIsGround);
		
		//Sets the direction that the player needs to face based on the movement
		if(h > 0 && !facingRight){
			FlipSprite();
		}else if(h < 0 && facingRight){
			FlipSprite();
		}
		
		//Set Play animations for movement
		rigidbody2D.velocity = new Vector2(h * moveSpeed, rigidbody2D.velocity.y);
		
		
		// If the player should jump...
		if (grounded && jump) {
			// Add a vertical force to the player.
			rigidbody2D.AddForce(new Vector2(0f, jumpForce));
			jump = false;
		}

		if(shoot){
			GameObject temp = Instantiate(smallShot, bulletSpawn.transform.position, Quaternion.identity) as GameObject;

			if(facingRight){
				temp.GetComponent<Bullet>().fireRight();
			}else{
				temp.GetComponent<Bullet>().fireLeft();
			}

			shoot = false;
			canShoot = true;
			hasAttacked = true;

		}
		
	}
	
	private void FlipSprite(){
		
		facingRight = !facingRight;
		
		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		
		transform.localScale = theScale;
		
	}

	public void setHorizontal(float horizontal){

		h = horizontal;

	}

	public void setVertical(float vertical){

		v = vertical;

	}

	public float getHorizontal(){

		return h;

	}

	public float getVertical(){

		return v;

	}

	public void tryToJump(){

		if(grounded){
			jump = true;
		}

	}

	public void tryToShoot(){

		if(canShoot == true){
			shoot = true;
			canShoot = false;
		}

	}
}
