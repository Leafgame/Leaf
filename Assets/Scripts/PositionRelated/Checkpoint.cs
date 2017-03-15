using UnityEngine;

namespace Assets.Scripts.PositionRelated
{
    [RequireComponent(typeof(Collider2D))]
	public class Checkpoint : MonoBehaviour
	{

        protected void OnTriggerEnter2D(Collider2D col)
		{
			var checkpointManager = GetComponentInParent<CheckpointManager>();
			checkpointManager.CurrentCheckpoitPos = transform.position;
		}
	}
}
