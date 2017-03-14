using UnityEngine;

namespace Assets.Scripts.Misc
{
	[RequireComponent(typeof(CircleCollider2D))]
	public class MovableWindPad : MonoBehaviour
	{
		public CircleCollider2D GrabCircle;
		public float GrabRadius = 3f;

		private Vector3 _distanceToObject;
		private Transform _playerReference;
        private Rigidbody2D _rigidbody2d;

		public void Start()
		{
			GrabCircle = GetComponent<CircleCollider2D>();
			GrabCircle.radius = GrabRadius;
			GrabCircle.isTrigger = true;
            _rigidbody2d = GetComponent<Rigidbody2D>();
		}

		public void Update()
		{
			if (_playerReference == null)
			{
				_playerReference = GameObject.FindGameObjectWithTag("Player").transform;
			}
			var diffVec = ( _playerReference.position - transform.position );
			var distance = diffVec.magnitude;
            if (Input.GetButtonDown("Fire2") && distance < GrabRadius)
            {
                _distanceToObject = _playerReference.position - transform.position;
            }

            if (Input.GetButton("Fire2") && distance < GrabRadius)
			{
                //non physics system transform if that ever comes in handy
                //transform.Translate(diffVec - _distanceToObject);
                var rb = _playerReference.GetComponent<Rigidbody2D>();
               _rigidbody2d.velocity = new Vector2(rb.velocity.x, 0);
			}
		}

		public virtual void Move(Vector3 distance)
		{
		}
			
		public void OnTriggerStay2D(Collider2D col)
		{

		}

	    public void OnTriggerEnter2D(Collider2D col)
	    {
	        
	    }

        public void OnTriggerExit2D(Collider2D col)
        {
            if(col.tag == "Player")
            {
                _rigidbody2d.velocity = new Vector3(0, 0);
            }
        }

    }
}

