using System;
using UnityEngine;

namespace Assets.Scripts
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

        private Vector3 _referenceVec;
        private bool _cameraFollow;
		private Rigidbody2D _rigidbody2D;


		public void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            MainCameraView = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            MainCameraView.orthographic = true;
            MainCameraView.orthographicSize = CameraSize;
            MainCameraView.transform.position = new Vector3(0, 0, -20);
        }

        public void Update()
        {
            var verticalCamera = Input.GetAxis("VerticalCamera") * CameraPanSpeed;
            var horizontalCamera = Input.GetAxis("HorizontalCamera") * CameraPanSpeed;
            var diff = (new Vector2(MainCameraView.transform.position.x + horizontalCamera, MainCameraView.transform.position.y + verticalCamera) -
                new Vector2(transform.position.x, transform.position.y));
            var distance = diff.magnitude;

	        if ((Math.Abs(_rigidbody2D.velocity.x) > 0.1 || Math.Abs(_rigidbody2D.velocity.y) > 0.1)
	            && (Math.Abs(Input.GetAxis("VerticalCamera")) < 0.1 || Math.Abs(Input.GetAxis("HorizontalCamera")) < 0.1))
	        {
		        _cameraFollow = true;
	        }
	        else if (distance < MaxRadius)
	        {
		        MainCameraView.transform.Translate(horizontalCamera, verticalCamera, 0);
		        _cameraFollow = false;
	        }
	        else if (distance > MaxRadius)
	        {
		        var clampedPos = Vector2.ClampMagnitude(diff, MaxRadius - 0.2f);
		        MainCameraView.transform.position = transform.position + new Vector3(clampedPos.x, clampedPos.y, -20);
				_cameraFollow = false;
			}



			// Camera follow calculations
			if (_cameraFollow)
            {
                _referenceVec = new Vector3(transform.position.x, transform.position.y, MainCameraView.transform.position.z);

	            var interpolatedVec = (_rigidbody2D.velocity * InterpolateCamAmount) + new Vector2(_referenceVec.x, _referenceVec.y);

				MoveObject(MainCameraView.transform, MainCameraView.transform.position, interpolatedVec, LerpTime);

                // Back at root position of player
                if (Math.Abs(MainCameraView.transform.localPosition.x - transform.position.x) < 0.5f &&
                    Math.Abs(MainCameraView.transform.localPosition.y - transform.position.y) < 0.5f)
                {
                    _cameraFollow = false;
                }
            }
        }

		public void MoveObject(Transform movingObject, Vector2 startpos, Vector2 endpos, float time)
		{
			var rate = 1.0f / time;
			var i = 0.0f;

			if (i < 1.0f)
			{
				i += Time.deltaTime * rate;
				movingObject.position = Vector3.Lerp(startpos, endpos, i) + new Vector3(0,0,-20);
			}
		}
	}
}
