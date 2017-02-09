﻿using Assets.Scripts.Misc;
using Assets.Scripts.PlayerScripts;
using UnityEngine;


namespace Assets.Scripts
{
	/// <summary>
	/// Purpose of this class is to lower the acceleration of gravity in a windzone so that 
	/// the wind can make the player fly around in the wind stream.
	/// </summary>
	public class LowGravityWind : WindObject
	{
		private Vector3 _windDirection;

		public new void FixedUpdate()
		{
			var windObjects = GetComponent<WindObject>();

			foreach (var col in windObjects.ObjectsInWindZone)
			{
				var rigidbdy = col.GetComponent<Rigidbody2D>();
				var targets = GetComponentsInChildren<Transform>();
				foreach (var transfrm in targets)
				{
					if (transfrm.tag == "SteerTarget")
					{
						_windDirection = transfrm.position;
					}
				}
				var direction = _windDirection - col.transform.position;
				if (direction.magnitude > 2f)
				{
					rigidbdy.velocity = new Vector2(rigidbdy.velocity.x, rigidbdy.velocity.y * .1f);
					rigidbdy.AddForce(direction.normalized * WindForce
						+ _windDirection * WindForceClose / direction.magnitude * Time.deltaTime
						);
				}
			}
		}

		public new void OnTriggerEnter2D(Collider2D col)
		{
			base.OnTriggerEnter2D(col);
			if (col.tag == "Player")
			{
				col.GetComponent<PlatformerCharacter2D>().InVerticalWindZone = true;

			}
		}

		public new void OnTriggerExit2D(Collider2D col)
		{
			base.OnTriggerExit2D(col);
			if (col.tag == "Player")
			{
				col.GetComponent<PlatformerCharacter2D>().InVerticalWindZone = false;
			}
		}
	}
}
