﻿using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.PlayerScripts;

namespace Assets.Scripts.GeneratedCode
{
	/// <summary>
	/// Wind objects base class, most wind based objects will derive from this.
	/// </summary>
	[RequireComponent(typeof(BoxCollider2D))]
	public class WindObject : MonoBehaviour
	{
		public float WindForce;
		public Vector3 WindDirection;
		public BoxCollider2D WindTrigger;

        private readonly List<Collider2D> _objectsInWindZone = new List<Collider2D>();

		public virtual void ApplyWindPhysics(Collider2D col)
		{
			var rigidBody2D = col.GetComponent<Rigidbody2D>();
			rigidBody2D.AddForce(WindDirection*WindForce);
		}

		private void FixedUpdate()
		{
			foreach (var rigidbodyObject in _objectsInWindZone)
			{
				ApplyWindPhysics(rigidbodyObject);
			}
		}

		public void Start()
		{
			WindTrigger = GetComponent<BoxCollider2D>();
			WindTrigger.isTrigger = true;
		}

		public void OnTriggerEnter2D(Collider2D col)
		{
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

