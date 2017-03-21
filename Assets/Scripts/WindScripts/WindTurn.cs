using System;
using UnityEngine;

namespace Assets.Scripts.WindScripts
{
	public class WindTurn : WindObject
	{
		public enum Direction
		{
			_left,
			_right,
			_up,
			_down
		}

		public Direction CurrentDirection = Direction._up;

		private BoxCollider2D _windZone;
		public bool Up;
        public bool Down;
        public bool Left;
        public bool Right;

		private float _sizeX;
		private float _sizeY;

		public void Start()
		{
			_windZone = GetComponent<BoxCollider2D>();
			_sizeX = _windZone.size.x;
			_sizeY = _windZone.size.y;
		}

		public void LateUpdate()
		{
			switch (CurrentDirection)
			{
				case Direction._left:
					WindDirection = new Vector3(-WindForce, 0f, 0f);
					break;
				case Direction._right:
					WindDirection = new Vector3(WindForce, 0f, 0f);
					break;
				case Direction._down:
					WindDirection = new Vector3(0f, -WindForce, 0f);
					break;
				case Direction._up:
					WindDirection = new Vector3(0f, WindForce, 0f);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		public void SetLeft()
		{
			_windZone.size = new Vector2(_sizeY, _sizeX);
			_windZone.offset = new Vector2(-_windZone.size.x/2f, 0f);
			CurrentDirection = Direction._left;
		}

		public void SetRight()
		{
			_windZone.size = new Vector2(_sizeY, _sizeX);
			_windZone.offset = new Vector3(_windZone.size.x/2f, 0f);
			CurrentDirection = Direction._right;
		}

		public void SetUp()
		{
			_windZone.size = new Vector2(_sizeX, _sizeY);
			_windZone.offset = new Vector2(0f, _windZone.size.y/2f);
			CurrentDirection = Direction._up;
		}

		public void SetDown()
		{
			_windZone.size = new Vector2(_sizeX, _sizeY);
			_windZone.offset = new Vector3(0f, -_windZone.size.y/2f);
			CurrentDirection = Direction._down;
		}
	}
}

