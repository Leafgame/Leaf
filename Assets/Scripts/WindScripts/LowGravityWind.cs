using System.Collections;
using Assets.Scripts.PlayerScripts;
using UnityEngine;

namespace Assets.Scripts.WindScripts
{
	/// <summary>
	/// Purpose of this class is to lower the acceleration of gravity in a windzone so that 
	/// the wind can make the player fly around in the wind stream.
	/// Depricated
	/// </summary>
	/* DEPRICATED
	public class LowGravityWind : WindObject
	{
		private Vector3 _windDirection;

		public void FixedUpdate()
		{
			//print("LOW GRAV");
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
				var direction = _windDirection - rigidbdy.transform.position;
				
				rigidbdy.transform.position += direction.normalized * WindForce * Time.deltaTime;
				
			}
		}

		public new void OnTriggerEnter2D(Collider2D col)
		{
			base.OnTriggerEnter2D(col);
			if (col.tag == "Player")
			{
				col.GetComponent<PlayerItemsController>().InVerticalWindZone = true;
			}
		}

		public new void OnTriggerExit2D(Collider2D col)
		{
			base.OnTriggerExit2D(col);
			if (col.tag == "Player")
			{
				col.GetComponent<PlayerItemsController>().InVerticalWindZone = false;
			}
		}
	}
	*/
}
