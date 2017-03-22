using System;
using UnityEngine;

namespace Assets.Scripts.PlayerScripts
{
	public class PlayerItemsController : MonoBehaviour
	{
		[SerializeField] public bool InWindZone;                         // Whether or not the player is in a wind zone
		[SerializeField] public bool InVerticalWindZone;

		public float GlideBoost = 50;
		public float GlideFallVelocity = 2.0f;
		public float MaxDashTime;
		public float DashSpeed;
		public float DashStoppingSpeed;
		public float MaxWindNegationTime;
		public float WindNegationCooldown;
		public bool WindNegationActive;

		private float _currentJumpTime;
		private float _currentWindNegationTime;
		private float _currentWindNegCooldown;
		private bool _canDoubleJump;
		private bool _dashLeft;
		private bool _dashRight;
		private bool _startGlide;
		private bool _gliding;

		private Animator _animator;                 // Reference to the player's animator component.
		private bool _facingRight = true;           // For determining which way the player is currently facing.
		private PlayerItems _playerItems;
		private float _currentDashTime;
		private bool _fire1;
		private Controller2D _controller;
		private Player _player;
	
		protected void Awake()
		{
			_animator = GetComponent<Animator>();
			_playerItems = GetComponentInParent<PlayerItems>();
			_controller = GetComponentInParent<Controller2D>();
			_player = GetComponentInParent<Player>();
		}

		protected void Start()
		{
			Debug.Log(Environment.Version);
		}

		protected void Update()
		{
			_fire1 = Input.GetButton("Fire1");
			var h = Input.GetAxis("Horizontal");

			FacingDirection(h);

			if (!_controller.collisions.below && Input.GetButtonUp("Fire1") && !_controller.collisions.above)
			{
				_startGlide = true;
			}
			else if ( _controller.collisions.below )
			{
				_startGlide = false;
			}

			if(_controller.collisions.below && MaxDashTime <= _currentDashTime)
			{
				_currentDashTime = 0;
			}
			
			CheckWindNegation();
		}

		private void CheckWindNegation()
		{
			if (Input.GetButtonDown("WindNegation") && !WindNegationActive && 
				_currentWindNegCooldown < 0.0 && _playerItems.HasWindNegationEquipped)
			{
				WindNegationActive = true;
				_currentWindNegationTime = 0.0f;
				_currentWindNegCooldown = WindNegationCooldown;
			}

			if (WindNegationActive)
			{
				_currentWindNegationTime += Time.deltaTime;
			}
			else
			{
				_currentWindNegCooldown -= Time.deltaTime;
			}
			if (_currentWindNegationTime > MaxWindNegationTime)
			{
				WindNegationActive = false;
			}
		}

		public void CheckDash(DashDirection direction)
		{
			if (!_controller.collisions.below && _playerItems.HasAirDashEquipped && !_dashLeft && !_dashRight && !_gliding)
			{
				var facing = transform.localScale.x < 0 ? -1 : 1;
				if (direction == DashDirection.Right)
				{
					_dashRight = true;
					if (facing == -1) Flip();
				}
				else if (direction == DashDirection.Left)
				{
					_dashLeft = true;
					if (facing == 1) Flip();
				}
			}
		}

		protected void FixedUpdate()
		{
			_animator.SetBool("Ground", _controller.collisions.below);
			_animator.SetFloat("vSpeed", _player.velocity.y);

			if(_startGlide)
				Glide();

			Dash();
		}

		private void Glide()
		{
			if (!_controller.collisions.below && _fire1 && _playerItems.HasGliderEquipped && 
				_player.velocity.y < 0 && !InWindZone && !_dashLeft && !_dashRight)
			{
				transform.GetChild(2).gameObject.SetActive(true);
				_player.velocity = new Vector2(GlideBoost * transform.localScale.x, -GlideFallVelocity);
				_gliding = true;
			}
			else if (InWindZone && _fire1 && _playerItems.HasGliderEquipped)
			{
				transform.GetChild(2).gameObject.SetActive(true);
				_gliding = true;
			}
			else
			{
				transform.GetChild(2).gameObject.SetActive(false);
				_gliding = false;
			}
		}

		public void FacingDirection(float move)
		{
			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !_facingRight)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && _facingRight)
			{
				// ... flip the player.
				Flip();
			}

		}


		private void Flip()
		{
			// Switch the way the player is labelled as facing.
			_facingRight = !_facingRight;

			// Multiply the player's x local scale by -1.
			var theScale = transform.localScale;
			theScale.x *= -1;
			transform.localScale = theScale;
		}

		protected void OnCollisionEnter2D(Collision2D col)
		{
			// set dash to false when colliding with walls
			_dashLeft = false;
			_dashRight = false;
		}

		public void Dash()
		{
			if (!_controller.collisions.below && _currentDashTime <= MaxDashTime && (_dashLeft || _dashRight) && !_gliding)
			{
				if (_dashLeft)
				{
					_player.velocity = new Vector2(-1 * DashSpeed, 0);
				}
				else if (_dashRight)
				{
					_player.velocity = new Vector2(DashSpeed, 0);
				}
				_currentDashTime += DashStoppingSpeed;
			}
			if (_currentDashTime > MaxDashTime)
			{
				_dashLeft = false;
				_dashRight = false;
			}
		}
	}
}
