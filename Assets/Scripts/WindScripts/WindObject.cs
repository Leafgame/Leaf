using System.Collections.Generic;
using Assets.Scripts.PlayerScripts;
using UnityEngine;

namespace Assets.Scripts.WindScripts
{
	/// <summary>
	/// Wind objects base class, most wind based objects will derive from this.
	/// </summary>
	[RequireComponent(typeof(BoxCollider2D))]
	public class WindObject : MonoBehaviour
	{
		private const int ModelOffset = 5;
		public float WindForce;
		public float WindForceClose;
		public Vector3 WindDirection;
		public BoxCollider2D WindTrigger;
		public bool IsActive = true;

		public readonly List<GameObject> ObjectsInWindZone = new List<GameObject>();

		private float _maxBoxSize;

		public virtual void ApplyWindPhysics(GameObject gO)
		{
			var rigidBody2D = gO.GetComponent<Rigidbody2D>();
			var windSource = rigidBody2D.transform.position - transform.position;
			var distanceToWindSource = windSource.magnitude;
	
			rigidBody2D.AddForce(WindDirection * WindForce
				+ WindDirection * WindForceClose / Mathf.Clamp(distanceToWindSource, 0.01f, 1f)
				);

			if (rigidBody2D.tag == "Player")
			{
				rigidBody2D.velocity = WindDirection;
			}

		}

		public void FixedUpdate()
		{
			if (!IsActive) return;

			foreach (var rigidbodyObject in ObjectsInWindZone)
			{
				if (rigidbodyObject.tag == "Player")
				{
					var character = rigidbodyObject.GetComponent<PlatformerCharacter2D>();

					if (!character.WindNegationActive)
					{
						// Apply force to player
						ApplyWindPhysics(rigidbodyObject.gameObject);
						print("applying physics to char");
					}
				}
				else // Apply force to all other objects
				{
					ApplyWindPhysics( rigidbodyObject.gameObject );
					print("Applying physics to other object");
				}
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
				var player = col.GetComponent<PlatformerCharacter2D>();
				if (!player.WindNegationActive)
				{
					player.InWindZone = true;
				}
			}

			if (HeavyObjectCheck(col))
			{
				_maxBoxSize = GetComponent<BoxCollider2D>().size.x + 2f;
			}

			// No Rigidbody2D on object: return
			if (!RigidbodyCheck(col) || HeavyObjectCheck(col)) return;
			if(!ObjectsInWindZone.Contains(col.gameObject))
				ObjectsInWindZone.Add(col.gameObject);
		}

		private static bool HeavyObjectCheck(Collider2D col)
		{
			return col.tag == "HeavyObject";
		}

		public void OnTriggerExit2D(Collider2D col)
		{
			if (!IsActive) return;
			if (col.tag == "Player")
			{
				col.GetComponent<PlatformerCharacter2D>().InWindZone = false;
			}

			if (HeavyObjectCheck(col))
			{
				var box = GetComponent<BoxCollider2D>();
				FixWindZoneArea( col , new Vector2( box.size.x - 2f, box.size.y ));
			}

			// No Rigidbody2D on object: return
			if (!RigidbodyCheck(col)) return;
			if(ObjectsInWindZone.Contains(col.gameObject))
				ObjectsInWindZone.Remove(col.gameObject);
		}

		public void OnTriggerStay2D(Collider2D col)
		{
			// HeavyObject Enters
			if (HeavyObjectCheck( col ))
			{
				var box = GetComponent<BoxCollider2D>();
				if (box.size.x <= _maxBoxSize)
				{
					var vec = col.transform.position - box.transform.position;
					FixWindZoneArea( col, new Vector2( Mathf.Abs( vec.x ) - ModelOffset, 3 ));
				}
			}
		}	

		public static bool RigidbodyCheck(Collider2D col)
		{
			return col.GetComponent<Rigidbody2D>() != null;
		}

		public void FixWindZoneArea(Collider2D col, Vector2 size)
		{
			var box = GetComponent<BoxCollider2D>();
			var vec = col.transform.position - box.transform.position;
			var dir = vec.x < 0 ? -1 : 1;
			box.size = size;
			box.offset = new Vector2( dir * box.size.x / 2 - ModelOffset * -dir, 0 );
		}
	}
}

