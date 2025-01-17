﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicController : MonoBehaviour 
{

	public Transform cameraT;
	public Transform playerT;
	public Animator animator;
	public float angularSpeed = 10.0f;
	public float movementSpeed = 10.0f;

	private void Update()
	{
		Vector3 direction = this.CalculateDirection ();

		float vertical = Input.GetAxis ("Vertical");
		float horizontal = Input.GetAxis ("Horizontal");
		float inputAngle = Mathf.Atan2(horizontal, vertical) * Mathf.Rad2Deg;
		float inputSpeed = Mathf.Min(1, new Vector2 (horizontal, vertical).magnitude);

		playerT.rotation = Quaternion.Lerp (
			playerT.rotation,
			Quaternion.LookRotation(direction, Vector3.up) * Quaternion.AngleAxis(inputAngle, Vector3.up),
			Time.deltaTime * angularSpeed * inputSpeed);

		playerT.position += playerT.forward * Time.deltaTime * movementSpeed * inputSpeed;

		animator.SetFloat ("Speed", inputSpeed);
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
