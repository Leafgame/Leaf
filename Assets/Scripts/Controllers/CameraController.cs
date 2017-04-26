using Assets.Scripts.PlayerScripts;
using UnityEngine;

namespace Assets
{
	public class CameraController : MonoBehaviour
	{
		private Camera _mainCamera;
		private CameraBehaviour _standardCamera;
		private NodeCamera _nodeCamera;

        public bool IsStandardCamera; 

		protected void Start()
		{
			_mainCamera = Camera.main;
			_standardCamera = GameObject.FindGameObjectWithTag("Player").GetComponent<CameraBehaviour>();
			_nodeCamera = _mainCamera.GetComponent<NodeCamera>();
            if (IsStandardCamera)
            {
                SetToStandardCamera();
            }
            else
            {
                SetToNodeCamera();
            }
        }

        /// <summary>
        /// Reequires there to be camera nodes in the scene
        /// </summary>
        public void SetToNodeCamera()
		{
			_nodeCamera.enabled = true;
			_standardCamera.enabled = false;
		}

		/// <summary>
		/// Automatically follows the player wherever he goes
		/// </summary>
		public void SetToStandardCamera()
		{
			_nodeCamera.enabled = false;
			_standardCamera.enabled = true;
		}
	}
}
