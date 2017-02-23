using UnityEngine;

namespace Assets.Scripts.Misc
{
	public class FloatObject : MonoBehaviour
	{
	    public float FloatIntensity = 5.0f;
		public float PlatformOffset = 1f;
		protected Vector3 InitalPosition;
		protected Rigidbody2D Rigidbody2D;
		public bool IsWindActive = true;

		protected virtual void Start()
		{
			InitalPosition = transform.position;
			Rigidbody2D = GetComponent<Rigidbody2D>();
		}

		public virtual void Float()
		{
		    var cosTheta = Mathf.Cos(Time.time) * FloatIntensity;
			Rigidbody2D.velocity = new Vector2(0, cosTheta);
		}


	    protected virtual void FixedUpdate()
	    {
			if (!IsWindActive)
		    {
		    }
		    else
		    {
				Float();
			}
		}


	}
}

