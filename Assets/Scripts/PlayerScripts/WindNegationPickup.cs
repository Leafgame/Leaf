using Assets.Scripts.PlayerScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindNegationPickup : ItemPickup
{
	protected new void OnTriggerEnter2D( Collider2D col )
	{
		base.OnTriggerEnter2D( col );
		if (col.tag == "Player")
		{
			col.GetComponent<PlayerItems>().HasWindNegationEquipped = true;
		}
	}
}
