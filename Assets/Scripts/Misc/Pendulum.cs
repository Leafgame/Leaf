using UnityEngine;

namespace Assets
{
	public class Pendulum : MonoBehaviour
	{
		public Vector2 Anchor;
		private float _angle = Mathf.PI / 4,
			_angleAccel,
			_angleVelocity = 0;

		[SerializeField]
		private readonly int _length = 50;

		protected void FixedUpdate()
		{
			transform.position = new Vector3(-Mathf.Sin(_angle) * _length, -Mathf.Cos(_angle)*_length, 0) + new Vector3(Anchor.x, Anchor.y, 0);

			_angleAccel = (float) (-9.81 / _length * Mathf.Sin(_angle));
			_angleVelocity += _angleAccel * Time.deltaTime;
			_angle += _angleVelocity * Time.deltaTime;
		}
	}
}
