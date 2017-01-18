using Assets.Standard_Assets._2D.Scripts;
using UnityEngine;

namespace Assets.Scripts.GeneratedCode
{
	/// <summary>
	/// Wind objects base class, most wind based objects will derive from this.
	/// </summary>
	[RequireComponent(typeof(BoxCollider2D))]
	public class WindObject : MonoBehaviour
	{
		public float WindForce;
		public float WindSpeed;
		public Vector3 WindDirection;
		public BoxCollider2D WindTrigger;

		public virtual void ApplyWindPhysics(Collider2D col)
		{
			// No Rigidbody2D on object: return
			if (!RigidbodyCheck(col)) return;

			var rigidbody2d = col.GetComponent<Rigidbody2D>();
			rigidbody2d.AddForce(WindDirection*WindForce);

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
        }

		public void OnTriggerStay2D(Collider2D col)
		{
			ApplyWindPhysics(col);
		}

	    public void OnTriggerExit2D(Collider2D col)
	    {
	        if (col.tag == "Player")
	        {
	            col.GetComponent<PlatformerCharacter2D>().InWindZone = false;
	        }
	    }

		public bool RigidbodyCheck(Collider2D col)
		{
			return col.GetComponent<Rigidbody2D>() != null;
		}
	}
}

