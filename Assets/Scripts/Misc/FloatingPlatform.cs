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
	}
}

