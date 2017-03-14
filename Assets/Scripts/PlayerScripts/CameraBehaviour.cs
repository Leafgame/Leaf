using System;
using UnityEngine;

namespace Assets.Scripts.PlayerScripts
{
    public class CameraBehaviour : MonoBehaviour
    {
        /// <summary>
        /// The main camrea currently rendering the scene
        /// </summary>
        [Header("Camera")]
        public Camera MainCameraView;

        /// <summary>
        /// The speed at which the linear interpolation happens
        /// </summary>
        public float LerpTime = 100.0f;

        /// <summary>
        /// The speed at which you can manually move the camera
        /// </summary>
        public float CameraPanSpeed = 1.0f;

        /// <summary>
        /// The size of the orthographic camera in 16:9 aspect
        /// </summary>
        public float CameraSize = 15f;

        /// <summary>
        /// Maximum radius the camera can be away from the player
        /// </summary>
        public float MaxRadius = 2.0f;

        /// <summary>
        /// The amount which the velocty is being extrapolated to put the camera ahead of the player
        /// </summary>
	    public float ExtrapolationAmout = 10.0f;

        /// <summary>
        /// The maximum anmout the player can move vertically before the camera needs to verically reposition
        /// </summary>
	    private float _yMax = -5f;

        /// <summary>
        /// The minimum anmout the player can move vertically before the camera needs to vertically reposition
        /// </summary>
        private float _yMin = 5f;

        /// <summary>
        /// The offset from the players position of where to put the camera e.g. 5 units above the player to
        /// focus of whats above not bellow
        /// </summary>
	    public float YOffset = 5f;

        /// <summary>
        /// The reference vec to hold the next position of the interpolated/extrapolated camera
        /// </summary>
        private Vector3 _referenceVec;

        /// <summary>
        /// Wether the camera is free moving or not 
        /// </summary>
        private bool _freeMovingCamera;

        /// <summary>
        /// Rigidbody2D reference to the player to be able to extrapolate the camera based on velocity
        /// </summary>
		private Rigidbody2D _rigidbody2D;


		public void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            MainCameraView = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            MainCameraView.orthographic = true;
            MainCameraView.orthographicSize = CameraSize;
            MainCameraView.transform.position = new Vector3(0, 0, -20);
			_yMax = -YOffset;
			_yMin = YOffset;
        }

        public void Update()
        {
            var verticalCamera = Input.GetAxis("VerticalCamera") * CameraPanSpeed;
            var horizontalCamera = Input.GetAxis("HorizontalCamera") * CameraPanSpeed;
			var diff = GetVectorFromPlayerToCamera(horizontalCamera, verticalCamera);
            var distance = diff.magnitude;

	        if (PlayerIsStandingStill())
	        {
		        _freeMovingCamera = true;
	        }
	        else if(PlayerIsMoving())
	        {
		        _freeMovingCamera = false;
	        }

	        if (PlayerNotFallingOrJumping())
	        {
				_referenceVec = GetCenterOfPlayerWithCorrectZCamPos();

				var interpolatedVec = new Vector3( MainCameraView.transform.position.x, transform.position.y + YOffset, -20 );
				MoveObject(MainCameraView.transform, MainCameraView.transform.position, interpolatedVec, LerpTime);
			}

			// Camera follow calculations
			if (!_freeMovingCamera)
            {
				_referenceVec = GetCenterOfPlayerWithCorrectZCamPos();

	            var interpolatedVec = new Vector3(_rigidbody2D.velocity.x * ExtrapolationAmout + _referenceVec.x, MainCameraView.transform.position.y, -20);
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
			var distToCam = GetVectorFromPlayerToCamera(0, 0);

	        if (distToCam.y < _yMax)
			{
				OffsetCamera(-YOffset);
			}
			else if (distToCam.y > _yMin)
	        {
				OffsetCamera(YOffset);
			}			
		}


		private void OffsetCamera(float offset)
		{
			MainCameraView.transform.position = new Vector3(MainCameraView.transform.position.x, transform.position.y + offset, -20);
		}

		private Vector3 GetCenterOfPlayerWithCorrectZCamPos()
		{
			return new Vector3(transform.position.x, transform.position.y, MainCameraView.transform.position.z);
		}

		private bool PlayerNotFallingOrJumping()
		{
			return Mathf.Abs(_rigidbody2D.velocity.y) < 0.1f;
		}

		private bool PlayerIsMoving()
		{
			return (Math.Abs(_rigidbody2D.velocity.x) > 0.1 || Math.Abs(_rigidbody2D.velocity.y) > 0.1);
		}

		private bool PlayerIsStandingStill()
		{
			return (Math.Abs(_rigidbody2D.velocity.x) < 0.1 && 
					Math.Abs(_rigidbody2D.velocity.y) < 0.1) && 
				   (Math.Abs(Input.GetAxis("VerticalCamera")) > 0.1 || 
				    Math.Abs(Input.GetAxis("HorizontalCamera")) > 0.1);
		}

		private Vector3 GetVectorFromPlayerToCamera(float h, float v)
		{
			return new Vector2(MainCameraView.transform.position.x + h, MainCameraView.transform.position.y + v) -
					new Vector2(transform.position.x, transform.position.y);
		}


		/// <summary>
		/// Linearly interpolates from startpos to endpos based on time.
		/// </summary>
		/// <param name="movingObject"></param>
		/// <param name="startpos"></param>
		/// <param name="endpos"></param>
		/// <param name="time"></param>
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
