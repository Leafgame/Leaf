using UnityEngine;

namespace Assets.Scripts.GeneratedCode
{
	public class FloatingPlatform : FloatObject
	{
		public virtual BoxCollider2D platform
		{
			get;
			set;
		}

	    public override void Float()
	    {
	        var cosThetha = Mathf.Cos(Time.time) * FloatIntensity;
            GetComponent<Rigidbody2D>().velocity = (new Vector2(0, cosThetha));
	    }

	    public void FixedUpdate()
	    {
	        Float();
	    }

	}
}

