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
		public float MaxHeight;
		public Vector3 WindDirection;
		public BoxCollider2D WindTrigger;
		public bool IsActive = true;

		private readonly List<Collider2D> _objectsInWindZone = new List<Collider2D>();

		public virtual void ApplyWindPhysics(Collider2D col)
		{
			var rigidBody2D = col.GetComponent<Rigidbody2D>();
			var windSource = rigidBody2D.transform.position - transform.position;
			var distanceToWindSource = windSource.magnitude;

			rigidBody2D.AddForce(WindDirection * WindForce);
			rigidBody2D.AddForce(WindDirection * WindForce / distanceToWindSource);
		}

		public void FixedUpdate()
		{
			if (!IsActive) return;

			foreach (var rigidbodyObject in _objectsInWindZone)
			{
				ApplyWindPhysics(rigidbodyObject);
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
			_objectsInWindZone.Add(col.GetComponent<Collider2D>());
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
			_objectsInWindZone.Remove(col.GetComponent<Collider2D>());

		}

		public bool RigidbodyCheck(Collider2D col)
		{
			return col.GetComponent<Rigidbody2D>() != null;
		}
	}
}

