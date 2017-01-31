namespace Assets.Scripts.Misc
{
	/// <summary>
	/// Description:
	/// Stationary windpad that emits wind in one direction.
	/// The wind elevates any physics affected object that enters
	/// the BoxCollider2D trigger.
	/// </summary>
	public class WindPad : WindObject
	{
		public virtual bool ActiveState
		{
			get;
			set;
		}

	}
}

