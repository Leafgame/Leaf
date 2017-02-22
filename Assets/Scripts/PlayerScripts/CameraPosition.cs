using UnityEngine;

namespace Assets.Scripts.PlayerScripts
{
	[ExecuteInEditMode]
	[RequireComponent( typeof( BoxCollider2D ) )]
	public class CameraPosition : MonoBehaviour
	{
		/// <summary>
		/// Size of the orthographic Camera
		/// </summary>
		public float Size;

		public float CameraMoveSpeed = 1f;

		public float Width { get; private set; }
		public float Height { get; private set; }

		private Vector3 _position;
		private Camera _mainCamera;
		private BoxCollider2D _cameraBox;
		public bool MoveCamera;

		public void Start( )
		{
			_cameraBox = GetComponent<BoxCollider2D>();
			_position = new Vector3( transform.position.x, transform.position.y, -20 );
			_mainCamera = Camera.main;
			Height = Size * 2f;
			Width = Height * Screen.width / Screen.height;
			_cameraBox.size = new Vector2( Width, Height );
			_cameraBox.isTrigger = true;
		}


		protected void Update( )
		{
			if (MoveCamera)
			{
				_mainCamera.transform.position = Vector3.Lerp( _mainCamera.transform.position, _position, Time.deltaTime * CameraMoveSpeed );
				_mainCamera.orthographicSize = Mathf.Lerp(_mainCamera.orthographicSize, Size, Time.deltaTime * CameraMoveSpeed );
			}
		}


		public void MoveCameraToThisPosition( )
		{
			_mainCamera.transform.position = _position;
			_mainCamera.orthographicSize = Size;
		}

		protected void OnDrawGizmos( )
		{
			Gizmos.color = new Color( 255, 255, 0, 0.1f );
			Gizmos.DrawCube( _position, new Vector3( Width, Height, 0 ) );
		}

		protected void OnTriggerEnter2D( Collider2D col )
		{
			if (col.tag == "Player" && col is BoxCollider2D )
			{
				MoveCamera = true;
			}
		}

		protected void OnTriggerExit2D( Collider2D col )
		{
			if (col.tag == "Player")
			{
				MoveCamera = false;
			}
		}
	}
}
