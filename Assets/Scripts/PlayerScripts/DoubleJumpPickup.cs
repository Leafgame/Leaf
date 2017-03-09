using UnityEngine;

namespace Assets.Scripts.PlayerScripts
{
	public class DoubleJumpPickup : ItemPickup
	{
        /// <summary>
        /// Give the player double jump ability
        /// </summary>
        /// <param name="col"></param>
		protected new void OnTriggerEnter2D(Collider2D col)
		{
			base.OnTriggerEnter2D(col);
			if (col.tag == "Player")
			{
				col.GetComponent<PlayerItems>().HasDoubleJumpEquipped = true;
			}
		}

	}
}
