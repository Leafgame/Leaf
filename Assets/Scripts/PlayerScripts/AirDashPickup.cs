using UnityEngine;

namespace Assets.Scripts.PlayerScripts
{
	public class AirDashPickup : ItemPickup
    {
        /// <summary>
        /// When the player collides with the trigger give him the AirDash item
        /// </summary>
        /// <param name="col"></param>
		protected new void OnTriggerEnter2D( Collider2D col )
		{
			base.OnTriggerEnter2D( col );
			if (col.tag == "Player")
			{
				col.GetComponent<PlayerItems>().HasAirDashEquipped = true;
			}
		}
	}
}
