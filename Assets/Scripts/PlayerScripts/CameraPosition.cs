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

		public float CameraMoveSpeed = 1f;

		public float SwapOffsetLeft = 4f;
		public float SwapOffsetRight = 5f;

		public float Width { get; private set; }
		public float Height { get; private set; }

		public Vector3 Position;
		private Camera _mainCamera;
		private BoxCollider2D _cameraBox;
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
				_playerLocation.position.x < Position.x + Width/2 + SwapOffsetRight)
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

		protected void OnTriggerStay2D(Collider2D col)
		{
			if(col.tag == "CameraPos")
				print(col.tag);
		}

		protected void OnTriggerEnter2D(Collider2D col)
		{
			
		}

		protected void OnTriggerExit2D(Collider2D col)
		{
		}

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
