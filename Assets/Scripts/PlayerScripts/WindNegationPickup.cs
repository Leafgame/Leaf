using Assets.Scripts.PlayerScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindNegationPickup : ItemPickup
{
    /// <summary>
    /// Give the player the wind negator item
    /// </summary>
    /// <param name="col"></param>
	protected new void OnTriggerEnter2D( Collider2D col )
	{
		base.OnTriggerEnter2D( col );
		if (col.tag == "Player")
		{
			col.GetComponent<PlayerItems>().HasWindNegationEquipped = true;
		}
	}
}
