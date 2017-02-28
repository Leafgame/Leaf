using Assets.Scripts.PlayerScripts;
using UnityEngine;

namespace Assets.Scripts.PositionRelated
{
	public class CheckpointManager : MonoBehaviour
	{
		public Vector3 CurrentCheckpoitPos;

		private GameObject _playerReference;

		public Transform[] Checkpoints;

		protected virtual void Start()
		{
			Checkpoints = GetComponentsInChildren<Transform>();
			_playerReference = GameObject.FindGameObjectWithTag("Player");
			CurrentCheckpoitPos = _playerReference.transform.position;
		}

		protected virtual void Update()
		{
			if (_playerReference == null)
			{
				Start();
			}
			if (_playerReference.GetComponent<PlatformerCharacter2D>().Grounded 
				&& Input.GetButtonDown("Fire1") )
			{
				CurrentCheckpoitPos = _playerReference.transform.position;
			}
		}
	}
}
