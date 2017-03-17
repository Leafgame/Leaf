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

		protected virtual void Start()
		{
			_playerReference = GameObject.FindGameObjectWithTag("Player");
			CurrentCheckpoitPos = _playerReference.transform.position;
		}

		protected virtual void Update()
		{
			if (_playerReference == null)
			{
				Start();
			}
			if ( _playerReference.GetComponent<PlatformerCharacter2D>().Grounded 
				&& Input.GetButtonDown("Fire1") )
			{
				CurrentCheckpoitPos = _playerReference.transform.position;
			}
		}
	}
}
