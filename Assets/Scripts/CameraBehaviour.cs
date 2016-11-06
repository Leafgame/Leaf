using System;
using UnityEngine;

namespace Assets.Scripts
{
	public class CameraBehaviour : MonoBehaviour
	{
		[Header( "Camera" )]
		public Camera MainCameraView;
		public Rigidbody2D _rigidbody;
		public float SmoothFollowSpeed = 10.0f;
		public float CameraPanSpeed = 1.0f;

		private bool _isGrounded;
		private Vector3 _referenceVec;
		private bool _cameraFollow;

		public void Update( )
		{
			float verticalCamera = Input.GetAxis("VerticalCamera") * CameraPanSpeed;
			float horizontalCamera = Input.GetAxis("HorizontalCamera") * CameraPanSpeed;

			if (  Math.Abs(_rigidbody.velocity.x) > 0.1 || Math.Abs(_rigidbody.velocity.y) > 0.1 )
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
		}
	}
}
