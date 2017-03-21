using UnityEngine;

namespace Assets.Scripts.Misc
{
	[RequireComponent(typeof(CircleCollider2D))]
	public class MovableWindPad : MonoBehaviour
	{
		public CircleCollider2D GrabCircle;
		public float GrabRadius = 3f;

		private Transform _playerReference;
        private Rigidbody2D _rigidbody2d;

		public void Start()
		{
			GrabCircle = GetComponent<CircleCollider2D>();
			GrabCircle.radius = GrabRadius;
			GrabCircle.isTrigger = true;
            _rigidbody2d = transform.parent.GetComponent<Rigidbody2D>();
		}

		public void Update()
		{
			if (_playerReference == null)
			{
				_playerReference = GameObject.FindGameObjectWithTag("Player").transform;
			}
			var diffVec = ( _playerReference.position - transform.position );
			var distance = diffVec.magnitude;

            if (Input.GetButton("Fire2") && distance < GrabRadius)
			{
				var player = _playerReference.GetComponent<Player>();
               _rigidbody2d.velocity = new Vector2(player.velocity.x, 0);
			}
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

