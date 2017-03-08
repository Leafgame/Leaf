using UnityEngine;

namespace Assets.Scripts.WindScripts
{
	public class WindTurn : WindObject
	{
		[SerializeField] private bool _left;
		[SerializeField] private bool _right;
		[SerializeField] private bool _down;

		private BoxCollider2D _windZone;

		private float _sizeX;
		private float _sizeY;
		private float _xOffset;
		private float _yOffset;

		protected new virtual void Start()
		{
			base.Start();
			_windZone = GetComponent<BoxCollider2D>();
			_sizeX = _windZone.size.x;
			_sizeY = _windZone.size.y;
			_xOffset = _windZone.offset.x;
			_yOffset = _windZone.offset.y;

			if (_down)
			{
				// 
			}

		}

		protected new virtual void Update()
		{
			base.Update();

		}

		public void SetLeft()
		{
			_windZone.size = new Vector2(_sizeY, _sizeX);
			transform.localPosition = new Vector3(-_windZone.size.x/2, 0f, 0f);
		}

		public void SetRight()
		{
			_windZone.size = new Vector2(_sizeY, _sizeX);
			transform.localPosition = new Vector3(_windZone.size.x/2f, 0f, 0f);
		}

		public void SetUp()
		{
			_windZone.size = new Vector2(_sizeX, _sizeY);
			transform.localPosition = new Vector3(0f, _windZone.size.y/2f, 0f);
		}

		public void SetDown()
		{
			_windZone.size = new Vector2(_sizeX, _sizeY);
			transform.localPosition = new Vector3(0f, -_windZone.size.y/2f, 0f);
		}

		
	}
}

