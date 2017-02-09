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
		public float JumpHoldTime = 0.5f;
	    public float LateJumpTime = 0.2f;
		public float HoldForceMultiplier = 10f;
		private float _currentJumpTime;
	    private float _currentAirTime;
	    private bool _canDoubleJump;

		private Transform _groundCheck;             // A position marking where to check if the player is grounded.
	    private const float GroundedRadius = .2f;   // Radius of the overlap circle to determine if grounded
        private Transform _ceilingCheck;            // A position marking where to check for ceilings
	    private const float CeilingRadius = .01f;   // Radius of the overlap circle to determine if the player can stand up
        private Animator _animator;                 // Reference to the player's animator component.
        private Rigidbody2D _rigidbody2D;
        private bool _facingRight = true;           // For determining which way the player is currently facing.
	    private PlayerItems _playerItems;
	    private float _speed;
	    private float _acceleration = 10f;

        private void Awake()
        {
            // Setting up references.
            _groundCheck = transform.Find("GroundCheck");
            _ceilingCheck = transform.Find("CeilingCheck");
            _animator = GetComponent<Animator>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
	        _playerItems = GetComponent<PlayerItems>();
        }

	    protected void Start()
	    {
	    }


	    private void FixedUpdate()
	    {
		    Grounded = false;
		    // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		    // This can be done using layers instead but Sample Assets will not overwrite your project settings.
		    var colliders = Physics2D.RaycastAll(_groundCheck.position, Vector2.down, GroundedRadius, WhatIsGround);
		    foreach (var t in colliders)
		    {
			    if (t.transform.gameObject != gameObject)
			    {
				    Grounded = true;
				    _currentAirTime = 0f;
			    }
		    }

		    _animator.SetBool("Ground", Grounded);

		    // Set the vertical animation
		    _animator.SetFloat("vSpeed", _rigidbody2D.velocity.y);

		    // GLIDING
		    if (!Grounded && Input.GetButton("Fire1") && _playerItems.HasGliderEquipped && _rigidbody2D.velocity.y < 0 &&
		        !InWindZone)
		    {
			    transform.GetChild(2).gameObject.SetActive(true);
			    _rigidbody2D.velocity = new Vector2(0, -GlideFallVelocity);
			    _rigidbody2D.AddForce(new Vector2(GlideBoost * transform.localScale.x, 0));
		    }
		    else if (InWindZone && Input.GetButton("Fire1") && _playerItems.HasGliderEquipped)
		    {
			    transform.GetChild(2).gameObject.SetActive(true);
		    }
		    else
		    {
			    transform.GetChild(2).gameObject.SetActive(false);
		    }
			if (_rigidbody2D.velocity.magnitude > MaxRigidBodySpeed)
			{
				_rigidbody2D.velocity = _rigidbody2D.velocity.normalized * MaxRigidBodySpeed;
			}
		}

	    public void Move(float move, bool crouch, bool jump)
	    {
		    var friction = GetComponent<CircleCollider2D>();
			if (Math.Abs(move) < 0.1 && Grounded)
			{
				friction.sharedMaterial.friction = 1f;
				friction.enabled = false;
				friction.enabled = true;
			}
			else
			{
				friction.sharedMaterial.friction = 0;
				friction.enabled = false;
				friction.enabled = true;
			}

			// If crouching, check to see if the character can stand up
			if (!crouch && _animator.GetBool("Crouch"))
            {
                // If the character has a ceiling preventing them from standing up, keep them crouching
                if (Physics2D.OverlapCircle(_ceilingCheck.position, CeilingRadius, WhatIsGround))
                {
                    crouch = true;
                }
            }

            // Set whether or not the character is crouching in the animator
            _animator.SetBool("Crouch", crouch);

            //only control the player if grounded or airControl is turned on
            if ((Grounded || AirControl) && !InVerticalWindZone)
            {
                // Reduce the speed if crouching by the crouchSpeed multiplier
                move = (crouch ? move*CrouchSpeed : move);

                // The Speed animator parameter is set to the absolute value of the horizontal input.
                _animator.SetFloat("Speed", Mathf.Abs(move));

                // Move the character
	            if (_speed < MaxSpeed && Math.Abs(move) > 0) {
		            _speed = _speed + _acceleration * Time.deltaTime;
	            }
	            else if(Math.Abs(move) < 0.01) {
		            _speed = 0;
	            }
	            _rigidbody2D.velocity = new Vector2(move*_speed, _rigidbody2D.velocity.y);

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

			// If the player should jump...
			if ((Grounded || _currentAirTime < LateJumpTime) && jump && !InWindZone)
            {
                // Add a vertical force to the player.
                Grounded = false;
                _animator.SetBool("Ground", false);
				_rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 0);
				_rigidbody2D.AddForce(new Vector2(0f, JumpForce));
	            _canDoubleJump = true;
            }
			else if (_canDoubleJump && jump && !InWindZone)
			{
				_canDoubleJump = false;
				_animator.SetBool("Ground", false);
				_rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 0);
				_rigidbody2D.AddForce(new Vector2(0f, JumpForce));
				_currentJumpTime = JumpHoldTime;
			}

			if (Input.GetButtonUp("Fire1"))
			{
				_currentJumpTime = 0;
			}

			// Reset the JumpHold on grounded
			if (Grounded)
	        {
		        _currentJumpTime = JumpHoldTime;
	        }
	        else
	        {
		        _currentAirTime += Time.deltaTime;
	        }
			if (_currentJumpTime > 0 && Input.GetButton("Fire1") && !Grounded )
			{
				_rigidbody2D.AddForce(new Vector3(0.0f, HoldForceMultiplier));
				_currentJumpTime -= Time.deltaTime;
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
	}
}
