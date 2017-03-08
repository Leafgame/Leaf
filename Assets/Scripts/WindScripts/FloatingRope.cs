using Assets.Scripts.Misc;
using UnityEngine;

namespace Assets.Scripts.WindScripts
{
	public class FloatingRope : FloatObject
	{
		public int RopeLength;

		public GameObject RopeSegment;
		private GameObject[] _ropeSegments;

		protected new void Start()
		{
			base.Start();

			_ropeSegments = new GameObject[RopeLength];

			for (var i = 0; i < RopeLength; i++)
			{
				var gameObj = Instantiate(RopeSegment, transform.position - new Vector3(0, i, 0), Quaternion.identity);
				_ropeSegments[i] = gameObj;
			}

			for (var index = RopeLength-1; index > 0; index--)
			{
				var hinge = _ropeSegments[index].GetComponent<HingeJoint2D>();
				hinge.connectedBody = _ropeSegments[index - 1].GetComponent<Rigidbody2D>();
			}
		}
	}
}

