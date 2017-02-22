using UnityEngine;

namespace Assets.Scripts.PlayerScripts
{
	public class AirDashPickup : ItemPickup {

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
