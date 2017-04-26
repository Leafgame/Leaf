using UnityEngine;

namespace Assets.Scripts.Misc
{
	public class MovableObject : MonoBehaviour
	{
		public float GrabRadius = 3f;
		public bool IsMovable = true;

		private Transform _playerReference;
        private Rigidbody2D _rigidbody2d;
		private Vector3 _ogPosition;
		public void Start()
		{
			_ogPosition = transform.position;
			_rigidbody2d = GetComponentInParent<Rigidbody2D>();
			if (_playerReference == null)
			{
				_playerReference = GameObject.FindGameObjectWithTag("Player").transform;
			}
		}

		public void Update()
		{
			var diffVec = ( _playerReference.position - transform.position );
			var distance = diffVec.magnitude;

            if (Input.GetButton("Fire2") && distance < GrabRadius && IsMovable)
			{
				var player = _playerReference.GetComponent<Player>();
               _rigidbody2d.velocity = new Vector2(player.velocity.x, 0);
			}
			if (!IsMovable)
			{
				transform.position = _ogPosition;
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

