using UnityEngine;

namespace Assets.Scripts.PlayerScripts
{
	public class GliderPickup : ItemPickup
	{
        /// <summary>
        /// Give the glider to the player
        /// </summary>
        /// <param name="col"></param>
		protected new void OnTriggerEnter2D(Collider2D col)
		{
			base.OnTriggerEnter2D(col);
			if (col.tag == "Player")
			{
				col.GetComponent<PlayerItems>().HasGliderEquipped = true;
			}
		}
	}
}
