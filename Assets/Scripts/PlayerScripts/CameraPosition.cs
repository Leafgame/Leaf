using System;
using UnityEngine;

namespace Assets.Scripts.PlayerScripts
{
	[ExecuteInEditMode]
	[RequireComponent(typeof(BoxCollider2D))]
	public class CameraPosition : MonoBehaviour
	{
		/// <summary>
		/// Size of the orthographic Camera
		/// </summary>
		public float Size;

        /// <summary>
        /// Movement speed to the camera when transitioning.
        /// </summary>
		public float CameraMoveSpeed = 1f;

        /// <summary>
        /// The offset where the swap will happen sooner to give the player info about what is to come
        /// </summary>
		public float SwapOffsetLeft = 4f;

        /// <summary>
        /// The offset where the swap will happen sooner to give the player info about what is to come
        /// </summary>
		public float SwapOffsetRight = 5f;

        /// <summary>
        /// Width of the camera BBOX
        /// </summary>
		public float Width { get; private set; }

        /// <summary>
        /// Height of the camrea BBOX
        /// </summary>
		public float Height { get; private set; }

        /// <summary>
        /// Position in worldspace 
        /// </summary>
		public Vector3 Position;

        /// <summary>
        /// Reference to the main rendering camera
        /// </summary>
		private Camera _mainCamera;

        /// <summary>
        /// The BBOX trigger for the camera to tell if the player is inside or not
        /// </summary>
		private BoxCollider2D _cameraBox;

        /// <summary>
        /// Reference to players location
        /// </summary>
		private Transform _playerLocation;

		public void Start()
		{
			_cameraBox = GetComponent<BoxCollider2D>();
			Position = new Vector3(transform.position.x, transform.position.y, -20);
			_mainCamera = Camera.main;
			Height = Size * 2f;
			Width = Height * Screen.width / Screen.height;
			_cameraBox.size = new Vector2(Width, Height);
			_cameraBox.isTrigger = true;
			try
			{
				_playerLocation = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				_playerLocation = transform;
				print("Failed to find player");
			}
		}

		protected void OnDrawGizmos()
		{
			Gizmos.color = new Color(255, 255, 0, 0.1f);
			Gizmos.DrawCube(Position + new Vector3(0,0,10), new Vector3(Width, Height, 0));
			Gizmos.color = new Color(0, 0, 255);
			Gizmos.DrawLine(transform.position + new Vector3(-Width/2 - SwapOffsetLeft, 0, 0),
				transform.position + new Vector3(-Width / 2 - SwapOffsetLeft, -10, 0));
			Gizmos.DrawLine(transform.position + new Vector3(Width / 2 + SwapOffsetRight, 0, 0),
				transform.position + new Vector3(Width / 2 + SwapOffsetRight, -10, 0));

		}

		protected void Update()
		{
			//print("Player x: " + _playerLocation.position.x + " y: " + _playerLocation.position.y);
			if (Position.x - Width/2 - SwapOffsetLeft < _playerLocation.position.x && 
				_playerLocation.position.x < Position.x + Width/2 + SwapOffsetRight &&
				Position.y - Height/2f < _playerLocation.position.y && 
				Position.y + Height/2f > _playerLocation.position.y )
			{
				var cameras = _mainCamera.GetComponent<NodeCamera>();
				var thiscam = GetComponent<CameraPosition>();
				for (var i = 0; i < cameras.CameraPositions.Length; i++)
				{
					if (thiscam == cameras.CameraPositions[i])
					{
						cameras.CurrentCameraIndex = i;
					}
				}	
			}
		}


        /// <summary>
        /// Swaps the out the camera position for another one.
        /// </summary>
        /// <param name="col"></param>
		protected void SwapCamera(Collider2D col)
		{
			if (col.tag == "Player" && col is BoxCollider2D)
			{
				var cameras = _mainCamera.GetComponent<NodeCamera>();
				var thiscam = GetComponent<CameraPosition>();
				for (var i = 0; i < cameras.CameraPositions.Length; i++)
				{
					if (thiscam == cameras.CameraPositions[i])
					{
						cameras.CurrentCameraIndex = i;
					}
				}
			}
		}
	}
}
