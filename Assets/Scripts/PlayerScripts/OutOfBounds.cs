using Assets.Scripts.PositionRelated;
using UnityEngine;

namespace Assets.Scripts.PlayerScripts
{
	public class OutOfBounds : MonoBehaviour
	{
		private CheckpointManager _checkpointManager;

		protected void Start()
		{
			_checkpointManager = GameObject.FindGameObjectWithTag("CheckpointManager").GetComponent<CheckpointManager>();
		}


		public void OnTriggerEnter2D(Collider2D col)
		{	
			// PLAYER
			if (col.tag == "Player")
			{
				col.transform.position = _checkpointManager.CurrentCheckpoitPos;
				ResetVelocity(col);
			}

			// HEAVY OBJECT
			else if (col.tag == "HeavyObject")
			{
				col.transform.position = col.GetComponent<HeavyObject>().InitialPosition;
				ResetVelocity(col);
			}
		}

		public void ResetVelocity(Collider2D col)
		{
			var rb = col.GetComponent<Rigidbody2D>();
			rb.velocity = new Vector2(0, 0);
		}
	}
}
