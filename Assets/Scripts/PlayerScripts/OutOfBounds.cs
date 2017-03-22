using Assets.Scripts.PositionRelated;
using UnityEngine;

namespace Assets.Scripts.PlayerScripts
{
	public class OutOfBounds : MonoBehaviour
	{
        /// <summary>
        /// The last checkpoint the player died every time he jumps 
        /// from default layer a new "checkpoint" is created.
        /// </summary>
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

        /// <summary>
        /// Resets the velicty of the rigidbody to 0
        /// </summary>
        /// <param name="col"></param>
		public void ResetVelocity(Collider2D col)
		{
			var player = col.GetComponent<Player>();
			player.velocity = new Vector2(0, 0);
		}
	}
}
