using Assets.Scripts.GeneratedCode;
using UnityEngine;

namespace Assets.Scripts.Misc
{
	[RequireComponent(typeof(CircleCollider2D))]
	public class MovableWindPad : MonoBehaviour
	{
		public CircleCollider2D GrabCircle;
		public float GrabRadius = 3f;

		private Vector3 _previousPosition;
		private GameObject _playerReference;

		public void Start()
		{
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
			
		public void OnTriggerStay2D(Collider2D col)
		{

		}

	    public void OnTriggerEnter2D(Collider2D col)
	    {
	        
	    }

        public void OnTriggerExit2D(Collider2D col)
        {

        }

    }
}

