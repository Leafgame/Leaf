using UnityEngine;

namespace Assets.Scripts.GeneratedCode
{
	[RequireComponent(typeof(CircleCollider2D))]
	public class MovableWindPad : WindPad
	{
		public CircleCollider2D GrabCircle;
		public float GrabRadius = 3f;

		private Vector3 _previousPosition;
		private GameObject _playerReference;

		public new void Start()
		{
			base.Start();
			GrabCircle = GetComponent<CircleCollider2D>();
			GrabCircle.radius = GrabRadius;
			GrabCircle.isTrigger = true;
			WindTrigger.isTrigger = false;

		}

		public void Update()
		{
			if (_playerReference == null)
			{
				_playerReference = GameObject.FindGameObjectWithTag("Player");
			}
			var diffVec = (transform.position - _previousPosition);
			var distance = diffVec.magnitude;
			/* MOVE THE PAD WITH E ?
			if (Input.GetButton("Fire2") && distance < GrabRadius && diffVec.x < 0.0f)
			{
				Move(distance, -1);
			}
			else if(Input.GetButton("Fire2") && distance < GrabRadius && diffVec.x > 0.0f)
			{
				Move(distance, 1);
			}
			*/
			_previousPosition = _playerReference.transform.position;
		}

		public virtual void Move(float distance, int offset)
		{
			transform.position = new Vector3(_previousPosition.x + offset, transform.position.y, 0);
		}
			
		public new void OnTriggerStay2D(Collider2D col)
		{

		}

	    public new void OnTriggerEnter2D(Collider2D col)
	    {
	        
	    }

        public new void OnTriggerExit2D(Collider2D col)
        {

        }

    }
}

