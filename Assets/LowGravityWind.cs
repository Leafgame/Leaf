using System.Collections.Generic;
using Assets.Scripts.Misc;
using UnityEngine;

/*
 * Purpose of this class is to lower the acceleration of gravity in a windzone so that 
 * the wind can make the player fly around in the wind stream.
 */
namespace Assets
{
	public class LowGravityWind : MonoBehaviour
	{
		public float AntiGravityForce = 10f;

		public Dictionary<GameObject, float> OriginalGravityScales = new Dictionary<GameObject, float>();

		public void OnTriggerEnter2D(Collider2D col)
		{
			if (WindObject.RigidbodyCheck(col) && !OriginalGravityScales.ContainsKey(col.gameObject)) // checks that object has a rigidbody
			{
				//var rigidbody2d = col.GetComponent<Rigidbody2D>();
				//OriginalGravityScales.Add(col.gameObject, rigidbody2d.gravityScale);
				//rigidbody2d.gravityScale = GravityScale;
				//print(rigidbody2d.gravityScale);
			}
		}

		public void OnTriggerExit2D(Collider2D col)
		{
			if (WindObject.RigidbodyCheck(col) && OriginalGravityScales.ContainsKey(col.gameObject))
			{
				//var rigidbody2d = col.GetComponent<Rigidbody2D>();
				//rigidbody2d.gravityScale = OriginalGravityScales[col.gameObject];
				//OriginalGravityScales.Remove(col.gameObject);
				//print(rigidbody2d.gravityScale);
			}
		}

		public void FixedUpdate()
		{
			var windObjects = GetComponent<WindObject>();

			foreach (var col in windObjects.ObjectsInWindZone)
			{
				var rigidbdy = col.GetComponent<Rigidbody2D>();
				var target = GetComponentInChildren<Transform>();
				var distance = target.position - transform.position;

				rigidbdy.velocity = (target.position - transform.position).normalized * distance.magnitude * Time.deltaTime;
			}
		}
	}
}
