using Assets.Scripts.PlayerScripts;
using UnityEngine;

namespace Assets.Scripts.PositionRelated
{
	public class CheckpointManager : MonoBehaviour
	{
        /// <summary>
        /// The last valid place the user jumped from is stored as a checkpoint in this variable
        /// </summary>
		public Vector3 CurrentCheckpoitPos;

        /// <summary>
        /// Reference to the player GameObject
        /// </summary>
		private GameObject _playerReference;

		/// <summary>
		/// Reference to the controller so we know the player is located in a safe space
		/// </summary>
		private Controller2D _controller;

		protected virtual void Start()
		{
			_playerReference = GameObject.FindGameObjectWithTag("Player");
			CurrentCheckpoitPos = _playerReference.transform.position;
			_controller = _playerReference.GetComponent<Controller2D>();
		}

		protected virtual void Update()
		{
			if (_playerReference == null)
			{
				Start();
			}
			if (Input.GetButtonDown("Fire1") && _controller.collisions.below )
			{
				CurrentCheckpoitPos = _playerReference.transform.position;
			}
		}
	}
}
