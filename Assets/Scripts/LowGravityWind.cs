using System.Collections.Generic;
using Assets.Scripts.Misc;
using UnityEngine;

/*
 * Purpose of this class is to lower the acceleration of gravity in a windzone so that 
 * the wind can make the player fly around in the wind stream.
 */
namespace Assets
{
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

				rigidbdy.AddForce(direction * WindForce
					+ _windDirection * WindForceClose / Mathf.Log(direction.magnitude)
					);
				rigidbdy.velocity = new Vector2(rigidbdy.velocity.x, rigidbdy.velocity.y * 0.6f);
			}
		}
	}
}
