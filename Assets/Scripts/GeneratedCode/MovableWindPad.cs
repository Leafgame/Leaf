using UnityEngine;

namespace Assets.Scripts.GeneratedCode
{
	[RequireComponent(typeof(CircleCollider2D))]
	public class MovableWindPad : WindPad
	{
		public CircleCollider2D GrabCircle;
		public float GrabRadius = 3f;

		private Vector3 _previousPosition = new Vector3();
		private GameObject _playerReference;

		public new void Start()
		{
			base.Start();
			GrabCircle = GetComponent<CircleCollider2D>();
			GrabCircle.radius = GrabRadius;
			GrabCircle.isTrigger = true;
			
		}

		public void Update()
		{
			if (_playerReference == null)
			{
				_playerReference = GameObject.FindGameObjectWithTag("Player");
			}
			var distance = (transform.position - _previousPosition).magnitude;
			if (Input.GetButton("Fire2") && distance < GrabRadius)
			{
				Move();
			}
			_previousPosition = _playerReference.transform.position;
		}

		public virtual void Move()
		{
			transform.position = new Vector3(_previousPosition.x+1, transform.position.y, 0);
		}
			
		public new void OnTriggerStay2D(Collider2D col)
		{
			base.OnTriggerStay2D(col);


		}

	}
}

