using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GeneratedCode
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
            Idle,
            On,
            Off
        }

	    private LeverState _leverState;

	    private void Start()
	    {
	        _leverState = LeverState.Off;
	        _animator = GetComponent<Animator>();
	        _leverReady = true;
	    }

	    private void Update()
	    {
	        if ( Input.GetButtonDown("Fire2") && _playerInRangeOfLever )
	        {
                print(_animator.GetBool("LeverState"));
	            if (_animator.GetBool("LeverState") && _leverReady)
	            {
	                _leverReady = false;
                    _animator.SetBool("LeverState", false);
	            }
                else if(!_animator.GetBool("LeverState") && _leverReady)
                {
                    _leverReady = false;
                    _animator.SetBool("LeverState", true);
                }
            }
	        if (_animator.GetCurrentAnimatorStateInfo(0).IsTag("Idle"))
	        {
	            _leverReady = true;
	        }

	        if (_leverState == LeverState.Idle) return;

            if(_leverState == LeverState.On)
            {
                ActivateWindPads();
            }
            else if (_leverState == LeverState.Off)
            {
                DeactivateWindPads();
            }
            _leverState = LeverState.Idle;
        }

		public virtual void ActivateWindPads()
		{
		    foreach (var windObject in LeverConnectedWindObjects)
		    {
                windObject.SetActive(true);
            }
            print("Windpads is activated");
		}

        public virtual void DeactivateWindPads()
	    {
            foreach (var windObject in LeverConnectedWindObjects)
            {
                windObject.SetActive(false);
            }
            print("Windpads is deactivated");
	    }


	    public void LeverStateOn()
	    {
	        _leverReady = true;
            _leverState = LeverState.On;
	    }

        public void LeverStateOff()
        {
            _leverReady = true;
            _leverState = LeverState.Off;
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

