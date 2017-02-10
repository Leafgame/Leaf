using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Misc
{
    [RequireComponent(typeof(Collider2D))]
	public class LeverPad : MonoBehaviour
	{
		public List<GameObject> LeverConnectedWindObjects = new List<GameObject>();

	    private Animator _animator;

	    private bool _playerInRangeOfLever;
	    private bool _leverReady;

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

	    private void Update()
	    {
	        if ( Input.GetButtonDown("Fire2") && _playerInRangeOfLever )
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
			}
		}


		public void LeverStateOn()
	    {
	        _leverReady = true;
	    }

        public void LeverStateOff()
        {
            _leverReady = true;
        }

        private void OnTriggerEnter2D(Collider2D col)
	    {
	        if (col.tag == "Player")
	        {
	            _playerInRangeOfLever = true;
	        }
	    }

	    private void OnTriggerExit2D(Collider2D col)
	    {
	        if (col.tag == "Player")
	        {
	            _playerInRangeOfLever = false;
	        }
	    }

	}
}

