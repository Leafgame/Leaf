using Assets.Scripts.PlayerScripts;
using UnityEngine;

namespace Assets.Scripts
{
	public class CheckpointManager : MonoBehaviour
	{
		public Vector3 CurrentCheckpoitPos;

		private GameObject _playerReference;

		public Transform[] Checkpoints;

		protected virtual void Start ()
		{
			Checkpoints = GetComponentsInChildren<Transform>();
			_playerReference = GameObject.FindGameObjectWithTag("Player");
		}
	
		protected virtual void Update () {
			if (_playerReference.GetComponent<PlatformerCharacter2D>().Grounded && Input.GetButtonDown("Fire1"))
			{
				CurrentCheckpoitPos = _playerReference.transform.position;
			}
		}
	}
}
