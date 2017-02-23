using Assets.Scripts.Misc;
using UnityEngine;

namespace Assets.Scripts.WindScripts
{
	public class FloatingPlatform : FloatObject
	{

		public virtual BoxCollider2D Platform
		{
			get;
			set;
		}

		protected new virtual void Start()
		{
			base.Start();
			InitalPosition = transform.position;
		}

		public virtual void LowerPlatform()
		{
			transform.position = Vector3.Lerp(transform.position, transform.parent.position + new Vector3(0, PlatformOffset, 0), Time.deltaTime);
		}

		public virtual void RaisePlatform()
		{
			transform.position = Vector3.Lerp(transform.position, transform.parent.position + new Vector3(0, InitalPosition.y, 0), Time.deltaTime);
		}

		protected override void FixedUpdate()
		{
			base.FixedUpdate();
			if (!IsWindActive)
			{
				LowerPlatform();
			}
			else
			{
				RaisePlatform();
			}
		}
	}
}

