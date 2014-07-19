using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]

public class Bullet : MonoBehaviour {

	public float lemonSpeed = 5f;
	public float hDir;

	void Start(){

		Destroy(this.gameObject, 2f);

	}

	public void fireRight(){

		rigidbody2D.AddForce(Vector2.right*lemonSpeed, ForceMode2D.Impulse);

	}

	public void fireLeft(){

		rigidbody2D.AddForce(new Vector2(-1, 0)*lemonSpeed, ForceMode2D.Impulse);

	}

}
