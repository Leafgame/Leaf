using UnityEngine;

namespace Assets.Scripts.PlayerScripts
{
	public class NodeCamera : MonoBehaviour
	{
        /// <summary>
        /// All the cameras currently positioned in the world
        /// </summary>
		public CameraPosition[] CameraPositions;

        /// <summary>
        /// Currently active camera
        /// </summary>
		public int CurrentCameraIndex { get; set; }

        /// <summary>
        /// Main camera reference
        /// </summary>
		private Camera _main;

		protected void Start()
		{
			_main = Camera.main;
			_main.orthographic = true;
			var cameras = GameObject.FindGameObjectsWithTag("CameraPos");
			CameraPositions = new CameraPosition[cameras.Length];
			for (var i = 0; i < cameras.Length ; i++)
			{
				CameraPositions[i] = cameras[i].GetComponent<CameraPosition>();
			}

		}

		protected void Update()
		{
			_main.transform.position = Vector3.Lerp(_main.transform.position, 
				CameraPositions[CurrentCameraIndex].Position, Time.deltaTime * 
				CameraPositions[CurrentCameraIndex].CameraMoveSpeed);
			_main.orthographicSize = Mathf.Lerp(_main.orthographicSize, 
				CameraPositions[CurrentCameraIndex].Size, Time.deltaTime *
				CameraPositions[CurrentCameraIndex].CameraMoveSpeed);

		}
	}
}
