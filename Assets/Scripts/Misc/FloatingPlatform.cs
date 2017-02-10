using UnityEngine;

namespace Assets.Scripts.Misc
{
	public class FloatingPlatform : FloatObject
	{

		public virtual BoxCollider2D Platform
		{
			get;
			set;
		}


	    public override void Float()
	    {
	        var cosThetha = Mathf.Cos(Time.time) * FloatIntensity;
	    }
	}
}

