using System.Collections.Generic;
using Assets.Scripts.PlayerScripts;
using UnityEngine;

namespace Assets.Scripts.Misc
{
	/// <summary>
	/// Wind objects base class, most wind based objects will derive from this.
	/// </summary>
	[RequireComponent(typeof(BoxCollider2D))]
	public class WindObject : MonoBehaviour
	{
		public float WindForce;
		public float WindForceClose;
		public Vector3 WindDirection;
		public BoxCollider2D WindTrigger;
		public bool IsActive = true;

		public readonly List<GameObject> ObjectsInWindZone = new List<GameObject>();

		public virtual void ApplyWindPhysics(GameObject gO)
		{
			var rigidBody2D = gO.GetComponent<Rigidbody2D>();
			var windSource = rigidBody2D.transform.position - transform.position;
			var distanceToWindSource = windSource.magnitude;

			rigidBody2D.AddForce(WindDirection * WindForce
				+ WindDirection * WindForceClose / Mathf.Clamp(distanceToWindSource, 0.01f, 1f)
				);

			//print("Distance: " + distanceToWindSource + " Force: " + WindDirection * WindForce
			//	+ WindDirection * WindForceClose / Mathf.Log(distanceToWindSource));
		}

		public void FixedUpdate()
		{
			if (!IsActive) return;

			foreach (var rigidbodyObject in ObjectsInWindZone)
			{
				ApplyWindPhysics(rigidbodyObject.gameObject);
			}
		}

		public void Update()
		{
			WindTrigger.enabled = IsActive;
		}

		public void Start()
		{
			WindTrigger = GetComponent<BoxCollider2D>();
			WindTrigger.isTrigger = true;
		}

		public void OnTriggerEnter2D(Collider2D col)
		{
			if (!IsActive) return;
			if (col.tag == "Player")
			{
				col.GetComponent<Animator>().SetBool("Ground", false);
				col.GetComponent<PlatformerCharacter2D>().InWindZone = true;
			}

			// No Rigidbody2D on object: return
			if (!RigidbodyCheck(col)) return;
			if(!ObjectsInWindZone.Contains(col.gameObject))
				ObjectsInWindZone.Add(col.gameObject);
		}

		public void OnTriggerExit2D(Collider2D col)
		{
			if (!IsActive) return;
			if (col.tag == "Player")
			{
				col.GetComponent<PlatformerCharacter2D>().InWindZone = false;
			}

			// No Rigidbody2D on object: return
			if (!RigidbodyCheck(col)) return;
			if(ObjectsInWindZone.Contains(col.gameObject))
				ObjectsInWindZone.Remove(col.gameObject);
		}

		public static bool RigidbodyCheck(Collider2D col)
		{
			return col.GetComponent<Rigidbody2D>() != null;
		}
	}
}

