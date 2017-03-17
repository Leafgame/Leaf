using System;
using UnityEngine;

namespace Assets.Scripts.PlayerScripts
{
	public class PlatformerCharacter2D : MonoBehaviour
    {
        [SerializeField] public float MaxSpeed = 10f;					 // The fastest the player can travel in the x axis.
	    [SerializeField] public float MaxRigidBodySpeed = 50f;			 // The max speed of the rigidbody
        [SerializeField] public float JumpForce = 400f;                  // Amount of force added when the player jumps.
        [Range(0, 1)] [SerializeField] public float CrouchSpeed = .36f;  // Amount of maxSpeed applied to crouching movement. 1 = 100%
        [SerializeField] public bool AirControl = false;                 // Whether or not a player can steer while jumping;
        [SerializeField] public LayerMask WhatIsGround;                  // A mask determining what is ground to the character
        [SerializeField] public bool InWindZone;						 // Whether or not the player is in a wind zone
	    [SerializeField] public bool InVerticalWindZone;
		[SerializeField] public bool Grounded;							 // Whether or not the player is grounded.

		public float GlideBoost = 50;
	    public float GlideFallVelocity = 2.0f;
		public float MaxDashTime;
	    public float DashSpeed;
	    public float DashStoppingSpeed;
	    public float MaxWindNegationTime;
	    public float WindNegationCooldown;
		public bool WindNegationActive;

		private float _currentJumpTime;
	    private float _currentAirTime;
	    private float _currentWindNegationTime;
	    private float _currentWindNegCooldown;
	    private bool _canDoubleJump;
	    private bool _dashLeft;
	    private bool _dashRight;

		private Transform _groundCheck;             // A position marking where to check if the player is grounded.
		private const float GroundedRadius = 0.25f; // Radius of the overlap circle to determine if grounded
        private Transform _ceilingCheck;            // A position marking where to check for ceilings
	    private const float CeilingRadius = .01f;   // Radius of the overlap circle to determine if the player can stand up
        private Animator _animator;                 // Reference to the player's animator component.
        private bool _facingRight = true;           // For determining which way the player is currently facing.
	    private PlayerItems _playerItems;
	    private float _speed;
	    private const float Acceleration = 10f;
	    private float _currentDashTime;
		private bool _fire1;
		private Controller2D _controller;
		private Player _player;

	    protected void Awake()
        {
            // Setting up references.
            _groundCheck = transform.Find("GroundCheck");
            _ceilingCheck = transform.Find("CeilingCheck");
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
			Move(Input.GetAxis("Horizontal"));
			CheckWindNegation();
		}

		private void CheckWindNegation()
		{
			// wind negation
			if (Input.GetButtonDown("WindNegation") && !WindNegationActive && _currentWindNegCooldown < 0.0 && _playerItems.HasWindNegationEquipped)
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
			if (!Grounded && _playerItems.HasAirDashEquipped && !_dashLeft && !_dashRight)
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

		/*
		protected virtual void OnDrawGizmos()
	    {
			Gizmos.DrawRay(_groundCheck.position, Vector3.down*0.25f);
			var hit = Physics2D.Raycast( _groundCheck.position, Vector2.down, 0.5f, LayerMask.GetMask("Default"));
			Gizmos.color = new Color(0, 255, 0);
		    if (hit)
		    {
				Gizmos.DrawRay(transform.position, hit.normal);
				Gizmos.color = new Color(255, 0, 0);
				var facing = hit.normal.x < 0 ? 1 : -1;
				Gizmos.DrawRay(transform.position, new Vector3( facing*hit.normal.y, -facing * hit.normal.x ) );
			}
			Gizmos.DrawSphere(_groundCheck.position, GroundedRadius);
		}
		*/

	    protected void FixedUpdate()
	    {
		    Grounded = false;
		    // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		    // This can be done using layers instead but Sample Assets will not overwrite your project settings.
		    var colliders = Physics2D.CircleCastAll(_groundCheck.position, GroundedRadius, Vector2.down, 0.25f, WhatIsGround);
		    foreach (var t in colliders)
		    {
			    if (t.transform.gameObject != gameObject)
			    {
					// ON GROUND reset variables
				    Grounded = true;
				    _currentAirTime = 0f;
				    _currentDashTime = 0f;
			    }
		    }

		    _animator.SetBool("Ground", Grounded);

			// TODO this is something thats going to have to be done
			_animator.SetFloat("vSpeed", 0);

			Glide();

			// The input checking is done in update
			Dash();
		}

		private void Glide()
		{
			// GLIDING
			if (!Grounded && _fire1 && _playerItems.HasGliderEquipped && _player.velocity.y < 0 && !InWindZone)
			{
				transform.GetChild(2).gameObject.SetActive(true);
				_player.velocity = new Vector2(GlideBoost * transform.localScale.x, -GlideFallVelocity);
			}
			else if (InWindZone && _fire1 && _playerItems.HasGliderEquipped)
			{
				transform.GetChild(2).gameObject.SetActive(true);
			}
			else
			{
				transform.GetChild(2).gameObject.SetActive(false);				
			}
		}

		public void Move(float move)
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
			if (!Grounded && _currentDashTime < MaxDashTime && (_dashLeft || _dashRight))
			{
				if (_dashLeft)
				{
					_player.velocity = (Vector2.Lerp( new Vector2( 0, 0 ), new Vector2( -1 * DashSpeed, 0 ), Time.deltaTime ));
				}
				else if (_dashRight)
				{
					_player.velocity = (Vector2.Lerp( new Vector2( 0, 0 ), new Vector2( DashSpeed, 0 ), Time.deltaTime ));
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
