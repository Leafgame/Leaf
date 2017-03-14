using UnityEngine;
using System.Collections;
using System;

public class InputHandler : MonoBehaviour
{
	private Command DashLeftButton;
	private Command DashRightButton;
	private Command JumpButton;
	private Command HorizontalAxis;
	private Command VerticalAxis;
	private Command InteractButton;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		
		HandleInput();
	}

	private void HandleInput()
	{
		if (Input.GetButton("Fire1"))
		{
			JumpButton.Execute(gameObject);
		}
		else if (Input.GetButton("Fire2"))
		{
			InteractButton.Execute(gameObject);
		}
		else if (Input.GetButtonDown("DashLeft"))
		{
			DashLeftButton.Execute(gameObject);
		}
		else if (Input.GetButtonDown("DashRight"))
		{
			DashRightButton.Execute(gameObject);
		}
		
		
	}
}
