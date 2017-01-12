using System;
using UnityEngine;

namespace Assets.Scripts
{
	public class CameraBehaviour : MonoBehaviour
	{
		[Header( "Camera" )]
		public Camera MainCameraView;
		public Rigidbody2D Rigidbody;
		public float LerpSpeed = 100.0f;
		public float CameraPanSpeed = 1.0f;
		public float CameraSize = 15f;

		private Vector3 _referenceVec;
		private bool _cameraFollow;

		public void Start()
		{
			Rigidbody = GetComponent<Rigidbody2D>();
			MainCameraView = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
			MainCameraView.orthographic = true;
			MainCameraView.orthographicSize = CameraSize;
			MainCameraView.transform.position = new Vector3(0,0,-20);
		}

		public void Update( )
		{
			float verticalCamera = Input.GetAxis("VerticalCamera") * CameraPanSpeed;
			float horizontalCamera = Input.GetAxis("HorizontalCamera") * CameraPanSpeed;

			if (Math.Abs(Rigidbody.velocity.x) > 0.1 || Math.Abs(Rigidbody.velocity.y) > 0.1)
				_cameraFollow = true;
			else
			{
				MainCameraView.transform.Translate(horizontalCamera, verticalCamera, 0);
			}

			// Camera follow calculations
			if (_cameraFollow)
			{
				_referenceVec = new Vector3(transform.position.x, transform.position.y, MainCameraView.transform.position.z);
				MainCameraView.transform.position = Vector3.Lerp(MainCameraView.transform.position, _referenceVec,
					LerpSpeed * Time.deltaTime);

				// Back at root position of player
				if (Math.Abs(MainCameraView.transform.localPosition.x - transform.position.x) < 0.5f &&
				    Math.Abs(MainCameraView.transform.localPosition.y - transform.position.y) < 0.5f)
				{
					_cameraFollow = false;
				}
			}
		}
	}
}
