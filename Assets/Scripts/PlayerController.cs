using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{

	public Transform cameraT;
	public Transform playerT;
	public Animator animator;
	public CharacterController character;

	public float angularSpeed = 10.0f;
	public float movementSpeed = 3.4f;
	public float jumpForce = 3.5f;
	public float autoJumpCheckDistance = 0.3f;
	public float autoJumpCheckDepth = 0.2f;

	private float gravityAcc = 0;
	private float lastArcAngle = 0;


	private void Update()
	{
		if ( character.isGrounded && Input.GetButtonDown ("Action")) 
			this.animator.SetTrigger ("Roll");

		if ( this.CheckAutoJump () ) 
		{
			gravityAcc = jumpForce;
			this.animator.SetTrigger ("Jump");
		}

		this.Locomotion ();
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Vector3 origin = this.playerT.position + this.playerT.forward * autoJumpCheckDistance + Vector3.up;
		Gizmos.DrawLine (
			origin, 
			origin + Vector3.down * (1.0f+autoJumpCheckDepth));
	}

	private bool CheckAutoJump()
	{
		Vector3 origin = this.playerT.position + this.playerT.forward * autoJumpCheckDistance + Vector3.up;
		return 
			// is ground
			this.character.isGrounded && 
			// is not is roll
			!this.animator.GetCurrentAnimatorStateInfo(0).IsName("RollForward") &&
			// running
			Mathf.Abs(this.character.velocity.x)+Mathf.Abs(this.character.velocity.z) > 2.0f &&
			// forward dont has collision
			!Physics.Raycast (this.playerT.position+Vector3.up*0.3f, this.playerT.forward, this.autoJumpCheckDistance) &&
			// down dont has collision
			!Physics.Raycast (origin, Vector3.down, 1.0f + autoJumpCheckDepth);
	}

	private void Locomotion()
	{
		/****************************** INPUTS ********************************/
		// inputs
		float vertical = Input.GetAxis ("Vertical");
		float horizontal = Input.GetAxis ("Horizontal");
		float inputSpeed = Mathf.Min(1, new Vector2 (horizontal, vertical).magnitude);

		// convert world input to camera space
		Vector3 inputRelative = cameraT.rotation * new Vector3(horizontal, vertical, vertical);
		inputRelative.y = 0;
		inputRelative.Normalize ();

		/***************************** ROTATION ********************************/
		// rotation relative input
		Quaternion result = 
			inputSpeed > 0 ? Quaternion.LookRotation (inputRelative, Vector3.up) : playerT.rotation;
		// lert rotation
		playerT.rotation = Quaternion.Lerp (
			playerT.rotation,
			result,
			Time.deltaTime * angularSpeed * inputSpeed);

		/*************************** TRANSLATION ********************************/
		// apply gravity
		gravityAcc = Physics.gravity.y * Time.deltaTime + ( character.isGrounded && gravityAcc < 0 ? -2 : gravityAcc );
		// movement
		Vector3 movement = playerT.forward * Time.deltaTime * (movementSpeed * inputSpeed + animator.GetFloat("IncrementSpeed"));
		movement.y = gravityAcc * Time.deltaTime; 
		character.Move ( movement );

		/**************************** ANIMATION ********************************/
		// animation falling
		animator.SetBool("Falling", !this.character.isGrounded);
		// animation speed
		animator.SetFloat ("Speed", inputSpeed);
		// animation arc angle
		float newArcAngle = Vector3.Angle (playerT.forward, inputRelative) * Mathf.Sign (Vector3.Dot (playerT.right, inputRelative));
		lastArcAngle = Mathf.Lerp (lastArcAngle, newArcAngle, Time.deltaTime * 5);
		animator.SetFloat ("Angle", Mathf.Clamp(lastArcAngle/30.0f, 0.0f, 1.0f));






		/*
		Vector3 direction = this.CalculateDirection ();

		float vertical = Input.GetAxis ("Vertical");
		float horizontal = Input.GetAxis ("Horizontal");
		float inputAngle = Mathf.Atan2(horizontal, vertical) * Mathf.Rad2Deg;
		float inputSpeed = Mathf.Min(1, new Vector2 (horizontal, vertical).magnitude);

		Quaternion result = Quaternion.LookRotation (direction, Vector3.up) *
		                    Quaternion.AngleAxis (inputAngle, Vector3.up);

		playerT.rotation = Quaternion.Lerp (
			playerT.rotation,
			result,
			Time.deltaTime * angularSpeed * inputSpeed);

		// movement
		Vector3 movement = playerT.forward * Time.deltaTime * movementSpeed * inputSpeed;


		if (character.isGrounded)
			gravityAcc = 0;
		else
			gravityAcc += Physics.gravity.y * Time.deltaTime;

		// gravity
		movement.y = gravityAcc * Time.deltaTime; 
		// apply
		//character.Move ( movement );

		animator.SetFloat ("Speed", inputSpeed);
		animator.SetFloat ("Angle", Vector3.Angle(playerT.forward, result*Vector3.forward) * Vector3.Dot());
		*/
	}

	private Vector3 CalculateDirection()
	{
		Vector3 direction = playerT.position - cameraT.position;
		direction.y = 0;
		direction.Normalize ();
		if (direction.sqrMagnitude < Mathf.Epsilon) direction = cameraT.up;
		return direction;
	}
}
