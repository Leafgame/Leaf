using System;
using UnityEngine;

namespace Assets.Scripts
{
	public class PlayerController : MonoBehaviour
	{
		[Header( "Camera" )]
		public Camera MainCameraView;
		public float SmoothFollowSpeed = 10.0f;

		[Header( "Player Stats" )]
		public float MovementSpeed = 1.0f;
		public float JumpSpeed = 2.0f;
		public float Gravity = 2.81f;
		public float GravityForce = 3.0f;
		public float AirTime = 2.0f;
		public float Health = 100.0f;
		public float Damage = 10.0f;

		private bool _isGrounded;
		private Vector3 _referenceVec;
		private bool _cameraFollow;
		private Rigidbody2D _rigidbody;
		public void Start( )
		{
			MainCameraView = GameObject.FindGameObjectWithTag( "MainCamera" ).GetComponent<Camera>();
			_rigidbody = GetComponent<Rigidbody2D>();
		}

		public void Update( )
		{
			//float verticalVelocity = Input.GetAxis( "Vertical" ) * MovementSpeed;
			float horizontalVelocity = Input.GetAxis( "Horizontal" ) * MovementSpeed;
			float jumpButton = Input.GetAxis( "Fire1" );
			float verticalCamera = Input.GetAxis("VerticalCamera");
			float horizontalCamera = Input.GetAxis("HorizontalCamera");

			_rigidbody.AddForce(new Vector2(horizontalVelocity*MovementSpeed,0));


			if (  Math.Abs(_rigidbody.velocity.x) > 0.1 || Math.Abs(_rigidbody.velocity.y) > 0.1 || Math.Abs(horizontalVelocity) > 0.1 )
				_cameraFollow = true;
			else
				_cameraFollow = false;

			// Camera follow calculations
			if (_cameraFollow)
			{
				_referenceVec = new Vector3(transform.position.x, transform.position.y, MainCameraView.transform.position.z);
				MainCameraView.transform.position = Vector3.Lerp(MainCameraView.transform.position, _referenceVec,
					SmoothFollowSpeed * Time.deltaTime);
			}

			MainCameraView.transform.Translate(horizontalCamera, verticalCamera, 0);

			// Jump calculations
			Debug.Log( "Fire1: " + jumpButton );
			if (jumpButton > 0 && _isGrounded)
			{
				_rigidbody.AddForce(new Vector2(0, JumpSpeed));
			}

			GroundCheck();
		}

		private void GroundCheck()
		{
			var modelOffset = transform.position - new Vector3( 0f, 4f );
			var groundCheck = Physics2D.Raycast( modelOffset, Vector2.down, float.PositiveInfinity );
			_isGrounded = groundCheck.distance < 0.5;
			Debug.DrawRay(modelOffset , Vector3.down, Color.red);
			Debug.Log("Distance: " + groundCheck.distance + " isGrounded " + _isGrounded);
		}
	}
}
