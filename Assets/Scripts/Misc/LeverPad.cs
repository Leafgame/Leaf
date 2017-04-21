using System.Collections.Generic;
using Assets.Scripts.WindScripts;
using UnityEngine;

namespace Assets.Scripts.Misc
{
	[RequireComponent(typeof(Collider2D))]
	public class LeverPad : MonoBehaviour
	{
		/// <summary>
		/// The object which is affected by this lever
		/// </summary>
		public List<GameObject> LeverConnectedWindObjects = new List<GameObject>();

		/// <summary>
		/// The lever animator
		/// </summary>
	    private Animator _animator;

		/// <summary>
		/// Player range check bool
		/// </summary>
	    private bool _playerInRangeOfLever;

		/// <summary>
		/// Lever ready check bool
		/// </summary>
	    private bool _leverReady;

		/// <summary>
		/// The radius which to check if the player is inside.
		/// </summary>
		public float InteractRadius;

		/// <summary>
		/// The players world transform
		/// </summary>
		private Transform _playerReference;

		/// <summary>
		/// The lever states
		/// </summary>
        public enum LeverState
        { 
			Off,
            On,
            Idle
		}

	    private void Start()
	    {
	        _animator = GetComponent<Animator>();
	        _leverReady = true;
	    }

	    public void Update()
	    {

			if (_playerReference == null)
			{
				_playerReference = GameObject.FindGameObjectWithTag("Player").transform;
			}
			var diffVec = (_playerReference.position - transform.position);
			var distance = diffVec.magnitude;

			if ( Input.GetButtonDown("Fire2") && distance < InteractRadius )
	        {
	            if (_animator.GetBool("LeverState") && _leverReady)
	            {
	                _leverReady = false;
                    _animator.SetBool("LeverState", false);
					FlipActiveState();

				}
				else if(!_animator.GetBool("LeverState") && _leverReady)
                {
                    _leverReady = false;
                    _animator.SetBool("LeverState", true);
					FlipActiveState();
				}
			}
	        if (_animator.GetCurrentAnimatorStateInfo(0).IsTag("Idle"))
	        {
	            _leverReady = true;
	        }
        }

		/// <summary>
		/// Turns the lever on/off based on what it was previously set to
		/// </summary>
		public virtual void FlipActiveState()
		{
			foreach (var windObject in LeverConnectedWindObjects)
			{
				var windObj = windObject.GetComponentInChildren<WindObject>();
				windObj.IsActive = !windObj.IsActive;

				var floatingObj = windObject.GetComponentInChildren<FloatObject>();
				if (floatingObj != null)
				{
					floatingObj.IsWindActive = !floatingObj.IsWindActive;
				}

                SwapWindTurnDirection(windObject);
			}
		}

		/// <summary>
		/// Swaps the windturn to the next possible wind direction
		/// </summary>
		/// <param name="windObject"></param>
        private void SwapWindTurnDirection(GameObject windObject)
        {
            var wturn = windObject.GetComponentInChildren<WindTurn>();
            if(wturn != null)
            {
				wturn.IsActive = true;
                switch (wturn.CurrentDirection)
                {
                    case WindTurn.Direction._left:
                        wturn.SetUp();
						break;
					case WindTurn.Direction._up:
						wturn.SetRight();
						break;
					case WindTurn.Direction._right:
						wturn.SetDown();
						break;
					case WindTurn.Direction._down:
						wturn.SetLeft();
						break;
                }
            }
        }

		/// <summary>
		/// Set lever ready
		/// </summary>
        public void LeverStateOn()
	    {
	        _leverReady = true;
	    }

		/// <summary>
		/// Set lever not ready
		/// </summary>
        public void LeverStateOff()
        {
            _leverReady = true;
        }

	}
}

