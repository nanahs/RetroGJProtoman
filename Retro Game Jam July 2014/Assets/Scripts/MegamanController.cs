using UnityEngine;
using System.Collections;

public class MegamanController : MoveableEntity{

	private bool firstGroundHit = false;
	private Animator anim;

	private float runShootTimer = 0f;
	private float runShootCooldown = .25f;
	private bool shootTimer = false;

	public override void Start(){

		anim = this.GetComponent<Animator>();
		base.Start();

	}

	public override void Update () {

		if(Input.GetKeyDown(KeyCode.Space)){
			tryToJump();
		}

		if(Input.GetKeyDown(KeyCode.K) && (shootTimer == false)){
			tryToShoot();
			shootTimer = true;
		}

		if(!grounded){

		}

		base.Update();

		if(shootTimer == true){
			runShootTimer += Time.deltaTime;

			if(runShootTimer > runShootCooldown){
				shootTimer =  false;
				runShootTimer = 0f;

			}

		}

	
	}

	public override void FixedUpdate(){

		setHorizontal(Input.GetAxis("Horizontal"));
		setVertical(Input.GetAxis("Vertical"));

		base.FixedUpdate();

		if(shoot && !hasAttacked){
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

		anim.SetBool("grounded", grounded);


		if(grounded && !firstGroundHit){
			firstGroundHit = true;
			anim.SetBool("TeleportHitGround", true);
		}

		if(grounded){
			anim.SetFloat("moveSpeed", Mathf.Abs(h));
		}else{

			anim.SetFloat("verticalVelocity", rigidbody2D.velocity.y);


			if(hasAttacked && (anim.GetCurrentAnimatorStateInfo(0).IsName("fall"))){
				anim.Play("fallShoot");
				hasAttacked = false;
			}
			else if(hasAttacked){
				anim.Play("Jump and Shoot Blend Tree");
				hasAttacked = false;
			}
		}

		if(grounded && hasAttacked && (Mathf.Abs(h) > .1f)){
			anim.SetBool("attacking", hasAttacked);
		}
		else if(grounded && hasAttacked && (Mathf.Abs(h) < .1f)){

			anim.Play("shoot");
			//anim.SetBool("attacking", hasAttacked);
			hasAttacked = false;
			//anim.SetBool("attacking", hasAttacked);

		}



	}

	public void stopShooting(){
		anim.SetBool("attacking", false);
		hasAttacked = false;
		Debug.Log("herro");
	}
}
