using System;
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
		public int ModelOffset = 5;
		[Range(1f, 50f)]
		public float WindForce;
		[Range(0f, 0.1f)]
		public float HorizontalControll = 0.5f;
		public float ExitForce = 20f;
		public Vector3 WindDirection = Vector3.up;
		public BoxCollider2D WindTrigger;
		public bool IsActive = true;

		public List<GameObject> ObjectsInWindZone = new List<GameObject>();

		private float _maxBoxSize;

		public virtual void ApplyWindPhysics(GameObject actor)
		{
			var rigidBody2D = actor.GetComponent<Rigidbody2D>();

			var velocity = CalculateVelocity(actor.transform);

			rigidBody2D.velocity = (velocity)*100;
		}

		private Vector2 CalculateVelocity(Transform target)
		{
			Vector3 position = transform.position;
			Vector3 targetPosition = target.position;
			Vector3 direction = targetPosition - position;
			direction.Normalize();
			WindDirection.Normalize();
			Vector2 velocity =
				WindDirection * WindForce * Time.deltaTime +
				new Vector3(-1 * direction.x, direction.y) * Time.deltaTime;
			
			return velocity;
		}

		public void Update()
		{
			EnableTrigger();
            if (!IsActive) return;

			foreach (var rigidbodyObject in ObjectsInWindZone)
			{
				if (rigidbodyObject.tag == "Player")
				{
					var character = rigidbodyObject.GetComponentInChildren<PlayerItemsController>();

					if (!character.WindNegationActive)
					{
						// Apply force to player
						ApplyPlayerWind(rigidbodyObject.GetComponent<Player>());
					}
				}
				else // Apply force to all other objects
				{
					ApplyWindPhysics( rigidbodyObject.gameObject );
				}
			}
        }

		private void ApplyPlayerWind(Player player)
		{
			var velocity = CalculateVelocity(player.transform);
			player.velocity = new Vector2(velocity.x + Input.GetAxis("Horizontal") * HorizontalControll, velocity.y) * 100;
		}

		protected virtual void EnableTrigger()
        {
            WindTrigger.enabled = IsActive;
        }

        public void Awake()
		{
			WindTrigger = GetComponent<BoxCollider2D>();
			WindTrigger.isTrigger = true;
            gameObject.tag = "WindZone";
			WindDirection.Normalize();
		}

		public void OnTriggerEnter2D(Collider2D col)
		{
			if (!IsActive) return;
			if (col.tag == "Player")
			{
				var player = col.GetComponentInChildren<PlayerItemsController>();
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
			if (!RigidbodyCheck(col) || HeavyObjectCheck(col) || NonWindInteractingTags(col)) return;
			if(!ObjectsInWindZone.Contains(col.gameObject))
				ObjectsInWindZone.Add(col.gameObject);

		}

		private static bool HeavyObjectCheck(Collider2D col)
		{
			return col.tag == "HeavyObject";
		}

        private static bool NonWindInteractingTags(Collider2D col)
        {
            return col.tag == "FloatingPlatform";
        }

		public void OnTriggerExit2D(Collider2D col)
		{
			if (!IsActive) return;
			if (col.tag == "Player")
			{
				col.GetComponentInChildren<PlayerItemsController>().InWindZone = false;
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

			AddExitForce(col);
		}

		private void AddExitForce(Collider2D col)
		{
			Vector3 position = transform.position;
			Vector3 targetPosition = col.transform.position;
			Vector3 direction = targetPosition - position;
			direction.Normalize();

			if(col.tag == "Player")
			{
				var controller = col.GetComponent<Player>();
				controller.velocity = (direction * ExitForce);
			}
			else
			{
				var rb = col.GetComponent<Rigidbody2D>();
				rb.velocity = (direction * ExitForce) * Time.deltaTime;
			}
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
					FixWindZoneArea( col, new Vector2( Mathf.Abs( vec.x ) - ModelOffset, box.size.y ));
				}
			}
		}	

		public static bool RigidbodyCheck(Collider2D col)
		{
			return col.GetComponent<Rigidbody2D>() != null || col.GetComponentInChildren<Rigidbody2D>() != null;
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

