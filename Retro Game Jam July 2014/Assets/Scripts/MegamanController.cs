using UnityEngine;
using System.Collections;

public class MegamanController : MoveableEntity{

	private bool firstGroundHit = false;
	private Animator anim;

	public override void Start(){

		anim = this.GetComponent<Animator>();
		base.Start();

	}

	public override void Update () {

		if(Input.GetKeyDown(KeyCode.Space)){
			tryToJump();
		}

		if(Input.GetKeyDown(KeyCode.K)){
			tryToShoot();
		}

		if(!grounded){
			print (rigidbody2D.velocity.y);
		}

		base.Update();
	
	}

	public override void FixedUpdate(){

		setHorizontal(Input.GetAxis("Horizontal"));
		setVertical(Input.GetAxis("Vertical"));

		base.FixedUpdate();

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
}
