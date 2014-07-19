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
	
	}

	public override void FixedUpdate(){

		setHorizontal(Input.GetAxis("Horizontal"));
		setVertical(Input.GetAxis("Vertical"));

		base.FixedUpdate();

		if(grounded && !firstGroundHit){
			firstGroundHit = true;
			anim.SetBool("TeleportHitGround", true);
		}

		if(grounded){
			anim.SetFloat("moveSpeed", Mathf.Abs(h));
		}

	}
}
