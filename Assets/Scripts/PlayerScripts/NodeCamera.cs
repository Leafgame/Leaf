using UnityEngine;

namespace Assets.Scripts.PlayerScripts
{
	public class NodeCamera : MonoBehaviour
	{
		public CameraPosition[] CameraPositions;
		public int CurrentCameraIndex { get; set; }
		protected void Start()
		{
			// Moves the camera to the first Node
			Camera.main.orthographic = true;
			CameraPositions[0].MoveCameraToThisPosition();
		}

		protected void Update()
		{

		} 
	}
}
