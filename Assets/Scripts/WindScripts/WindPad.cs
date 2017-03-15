using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts.WindScripts
{
	/// <summary>
	/// Description:
	/// Stationary windpad that emits wind in one direction.
	/// The wind elevates any physics affected object that enters
	/// the BoxCollider2D trigger.
	/// </summary>
	public class WindPad : WindObject
	{
		public new float WindForce = 30;
		private float _gravSave;

		private List<GameObject> WindObjects = new List<GameObject>();

		private new void OnTriggerEnter2D(Collider2D col)
		{
			if (WindObjects.Contains(col.gameObject)) return;
			WindObjects.Add(col.gameObject);

			Debug.Log("Object entered WindZone");
			var rb = col.GetComponent<Rigidbody2D>();
			_gravSave = rb.gravityScale;
			rb.gravityScale = 0;
		
		}

		private new void OnTriggerStay2D(Collider2D collision)
		{
			Debug.Log("Object is in WindZOne");
			Vector3 position = transform.position;
			Vector3 targetPosition = collision.transform.position;
			Vector3 direction = targetPosition - position;
			direction.Normalize();
			collision.transform.position += direction * WindForce * Time.deltaTime;
		}

		private new void OnTriggerExit2D(Collider2D col)
		{
			if (!WindObjects.Contains(col.gameObject)) return;
			WindObjects.Remove(col.gameObject);
			Debug.Log("Object left the WindZone");
			var rb = col.GetComponent<Rigidbody2D>();
			rb.gravityScale = _gravSave;
		}
	}

}

