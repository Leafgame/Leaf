﻿using System;
using UnityEngine;

namespace Assets.Scripts.PlayerScripts
{
    public class CameraBehaviour : MonoBehaviour
    {
        [Header("Camera")]
        public Camera MainCameraView;
        public float LerpTime = 100.0f;
        public float CameraPanSpeed = 1.0f;
        public float CameraSize = 15f;
        public float MaxRadius = 2.0f;
	    public float InterpolateCamAmount = 10.0f;
	    public float YOffset = 5f;
	    public bool LockY = true;

        private Vector3 _referenceVec;
        private bool _freeMovingCamera;
		private Rigidbody2D _rigidbody2D;


		public void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            MainCameraView = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            MainCameraView.orthographic = true;
            MainCameraView.orthographicSize = CameraSize;
            MainCameraView.transform.position = new Vector3(0, 0, -20);
	        YOffset = 5f;
        }

        public void Update()
        {
            var verticalCamera = Input.GetAxis("VerticalCamera") * CameraPanSpeed;
            var horizontalCamera = Input.GetAxis("HorizontalCamera") * CameraPanSpeed;
            var diff = (new Vector2(MainCameraView.transform.position.x + horizontalCamera, MainCameraView.transform.position.y + verticalCamera) -
                new Vector2(transform.position.x, transform.position.y));
            var distance = diff.magnitude;

	        if ((Math.Abs(_rigidbody2D.velocity.x) < 0.1 && Math.Abs(_rigidbody2D.velocity.y) < 0.1)
	            && (Math.Abs(Input.GetAxis("VerticalCamera")) > 0.1 || Math.Abs(Input.GetAxis("HorizontalCamera")) > 0.1))
	        {
		        _freeMovingCamera = true;
	        }
	        else if((Math.Abs(_rigidbody2D.velocity.x) > 0.1 || Math.Abs(_rigidbody2D.velocity.y) > 0.1))
	        {
		        _freeMovingCamera = false;
	        }
	        

			// Camera follow calculations
			if (!_freeMovingCamera)
            {
                _referenceVec = new Vector3(transform.position.x, transform.position.y, MainCameraView.transform.position.z);

	            var interpolatedVec = new Vector3(_rigidbody2D.velocity.x * InterpolateCamAmount + _referenceVec.x, Mathf.Clamp(_rigidbody2D.velocity.y, -2, 2) * InterpolateCamAmount + _referenceVec.y + YOffset, -20);
				MoveObject(MainCameraView.transform, MainCameraView.transform.position, interpolatedVec, LerpTime);
			}

			if (distance < MaxRadius && _freeMovingCamera)
			{
				MainCameraView.transform.Translate(horizontalCamera, verticalCamera, 0);
			}
			if (distance > MaxRadius && _freeMovingCamera)
			{
				var clampedPos = Vector2.ClampMagnitude(diff, MaxRadius);
				MainCameraView.transform.position = transform.position + new Vector3(clampedPos.x, clampedPos.y, -20);
			}
			if(LockY)
				MainCameraView.transform.position = new Vector3(MainCameraView.transform.position.x, transform.position.y + YOffset, -20);
		}

		public void MoveObject(Transform movingObject, Vector3 startpos, Vector3 endpos, float time)
		{
			var rate = 1.0f / time;
			var i = 0.0f;

			if (i < 1.0f)
			{
				i += Time.deltaTime * rate;
				movingObject.position = Vector3.Lerp(startpos, endpos, i);
			}
		}
	}
}
