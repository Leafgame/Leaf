using UnityEngine;

namespace Assets.Scripts.Misc
{
	public class FloatObject : MonoBehaviour
	{
	    public float FloatIntensity = 5.0f;
		public float PlatformOffset = 1f;
		private Vector3 _initalPosition;
		private Rigidbody2D _rigidbody2D;
		public bool IsWindActive = true;

		protected virtual void Start()
		{
			_initalPosition = transform.position;
			_rigidbody2D = GetComponent<Rigidbody2D>();
		}

		public virtual void Float()
		{
		    var cosTheta = Mathf.Cos(Time.time) * FloatIntensity;
			_rigidbody2D.velocity = new Vector2(0, cosTheta);
		}


	    protected virtual void FixedUpdate()
	    {
			if (!IsWindActive)
		    {
			    LowerPlatform();
		    }
		    else
		    {
			    RaisePlatform();
				Float();
			}
		}

		public virtual void LowerPlatform()
		{
			transform.position = Vector3.Lerp(transform.position, transform.parent.position + new Vector3(0,PlatformOffset,0), Time.deltaTime);
		}

		public virtual void RaisePlatform()
		{
			transform.position = Vector3.Lerp(transform.position, transform.parent.position + new Vector3(0,_initalPosition.y, 0), Time.deltaTime);
		}

	}
}

