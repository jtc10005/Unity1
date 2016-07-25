using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour {


	public float speed = 6f;
	public float turnSpeed = 60f;
	public float turnSmothing = 15f;

	private Vector3 movement;
	private Vector3 turning;
	private Animator anim;
	private Rigidbody playerRigidBody;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Awake(){
		anim = GetComponent<Animator> ();
		playerRigidBody = GetComponent<Rigidbody>();
	}

	void FixedUpdate(){
		float lh = Input.GetAxisRaw ("Horizontal");
		float lv = Input.GetAxisRaw ("Vertical");
		Move (lh, lv);
		Animating (lh, lv);
	}

	void Move(float lh, float lv){
		movement.Set (lh, 0f, lv); //movement set (x,y,z) Y = up and down
		movement = movement.normalized * speed * Time.deltaTime;
		playerRigidBody.MovePosition (transform.position + movement);

		if(lh != 0f || lv != 0f){
			Rotating (lh, lv);
		}

	}
	void Animating(float lh, float lv){
		bool running = lh != 0f || lv != 0f;
		anim.SetBool ("isRunning",running);
	}

	void Rotating(float lh, float lv){
		Vector3 targetDirection = new Vector3 (lh, 0f, lv);
		Quaternion targetRotation = Quaternion.LookRotation (targetDirection, Vector3.up);
		Quaternion newRotation = Quaternion.Lerp (GetComponent<Rigidbody> ().rotation, targetRotation, turnSmothing * Time.deltaTime);
		GetComponent<Rigidbody> ().MoveRotation (newRotation);

	}
}
