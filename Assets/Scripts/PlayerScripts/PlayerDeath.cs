using Assets.Scripts.PositionRelated;
using UnityEngine;

namespace Assets.Scripts.PlayerScripts
{
	public class PlayerDeath : MonoBehaviour
	{
		private CheckpointManager _checkpointManager;

		protected void Start()
		{
			_checkpointManager = GameObject.FindGameObjectWithTag("CheckpointManager").GetComponent<CheckpointManager>();
		}


		public void OnTriggerEnter2D(Collider2D col)
		{	
			if (col.tag == "Player")
			{
				col.transform.position = _checkpointManager.CurrentCheckpoitPos;
				col.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
			}
		}
	}
}
