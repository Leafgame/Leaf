using System.Collections;
using Assets.Scripts.Misc;
using UnityEngine;

namespace Assets.Scripts.WindScripts
{
	/// <summary>
	/// Floating stairs that uses 1 way colliders
	/// </summary>
	public class FloatingStairs : FloatObject
	{
		public Collider2D platform;
		private int horizontalRayCount;
		private int topRayCount;

		public virtual float Height
		{
			get;
			set;
		}

		public virtual float Width
		{
			get;
			set;
		}

		public virtual float StepHeight
		{
			get;
			set;
		}

		public virtual void CreateStairs()
		{
			throw new System.NotImplementedException();
		}

		protected new void Start()
		{
			IsWindActive = false;

		}

		protected void Update()
		{
			if (Input.GetAxis("Vertical") > 0.0)
			{
				platform.gameObject.layer = 2;
				var box = GetComponent<BoxCollider2D>();
				box.enabled = false;
				StartCoroutine("EnableBox", box);
			}
		}

		protected void OnTriggerStay2D(Collider2D col)
		{
			if(col.tag == "Player")
			{
				var player = col.GetComponent<Player>();
				player.velocity = new Vector3(player.velocity.x, 7, 0);

			}
		}

		protected void OnTriggerEnter2D(Collider2D col)
		{
			if (col.tag == "Player")
			{
			}
		}
		protected void OnTriggerExit2D(Collider2D col)
		{
			if (col.tag == "Player")
			{
				var player = col.GetComponent<Player>();
				var controller = col.GetComponent<Controller2D>();
				platform.gameObject.layer = 0;
			}	
		}

		public IEnumerator EnableBox(BoxCollider2D box)
		{
			yield return new WaitForSeconds(1);
			box.enabled = true;
		}
	}
}

