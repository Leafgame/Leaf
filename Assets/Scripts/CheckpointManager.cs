using UnityEngine;

namespace Assets.Scripts
{
	public class CheckpointManager : MonoBehaviour
	{
		public Transform CurrentCheckpoit;

		public Transform[] Checkpoints;
		// Use this for initialization
		protected virtual void Start ()
		{
			Checkpoints = GetComponentsInChildren<Transform>();
		}
	
		// Update is called once per frame
		protected virtual void Update () {
		
		}
	}
}
