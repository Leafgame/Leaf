using UnityEngine;

namespace Assets.Scripts
{
	public class ParallaxLayerTest : MonoBehaviour {
		public float SpeedX;
		public float SpeedY;
		public bool MoveInOppositeDirection;

		private Transform _cameraTransform;
		private Vector3 _previousCameraPosition;
		private bool _previousMoveParallax;
		private ParallaxOption _options;

		protected virtual void Start()
		{
			var gameCamera = Camera.main.gameObject;
			_options = gameCamera.GetComponent<ParallaxOption>();
			_cameraTransform = gameCamera.transform;
			_previousCameraPosition = _cameraTransform.position;
		}

		protected virtual void Update ()
		{
			if(_options.MoveParallax && !_previousMoveParallax)
				_previousCameraPosition = _cameraTransform.position;

			_previousMoveParallax = _options.MoveParallax;

			if(!Application.isPlaying && !_options.MoveParallax)
				return;

			var distance = _cameraTransform.position - _previousCameraPosition;
			var direction = (MoveInOppositeDirection) ? -1f : 1f;
			Move(distance.magnitude*direction);

			_previousCameraPosition = _cameraTransform.position;
		}

		public void Move(float delta)
		{
			var newPos = transform.localPosition;
			newPos.x -= delta * SpeedX;
			transform.localPosition = newPos;
		}
	}
}
