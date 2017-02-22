using UnityEngine;

namespace Assets.Scripts
{
	public class ParallaxLayer : MonoBehaviour
	{
		public Camera MainCamera;
		public Vector3 Direction;
		[Range(0,1)]
		public float FollowAmount;

		public void Start()
		{
			MainCamera = Camera.main;
		}

		public void FixedUpdate()
		{
			Direction = Vector3.Scale((MainCamera.transform.position -
				transform.position),
				new Vector3(1, 1, 0));
			transform.position += Direction * FollowAmount;
		}

	}
}
