using Assets.Scripts.Misc;
using UnityEngine;

namespace Assets.Scripts.WindScripts
{
	public class FloatingStairs : FloatObject
	{
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

		protected void OnCollisionStay2D(Collision2D col)
		{
			foreach (var contact in col.contacts)
			{
				Debug.DrawRay(contact.point, contact.normal, Color.white);
			}
			if (col.transform.tag == "Player" && Input.GetAxis("Vertical") > 0.0)
			{
				GetComponent<Collider2D>().isTrigger = true;
			
			}
		}

		protected void OnTriggerExit2D(Collider2D col)
		{
			if (col.tag == "Player")
			{
				GetComponent<Collider2D>().isTrigger = false;
			}
		} 
	}
}

