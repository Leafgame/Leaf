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

		private Direction _direction = Direction._up;

		private BoxCollider2D _windZone;

		private float _sizeX;
		private float _sizeY;

		protected new virtual void Start()
		{
			base.Start();
			_windZone = GetComponent<BoxCollider2D>();
			_sizeX = _windZone.size.x;
			_sizeY = _windZone.size.y;
			SetRight(_windZone);
		}

		protected new virtual void Update()
		{
			base.Update();
			switch (_direction)
			{
				case Direction._left:
					WindDirection = new Vector3(-WindForce,0f,0f);
					break;
				case Direction._right:
					WindDirection = new Vector3(WindForce, 0f,0f);
					break;
				case Direction._down:
					WindDirection = new Vector3(0f,-WindForce, 0f);
					break;
				case Direction._up:
					WindDirection = new Vector3(0f, WindForce, 0f);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		public void SetLeft(BoxCollider2D windZone)
		{
			windZone.size = new Vector2(_sizeY, _sizeX);
			windZone.offset = new Vector2(-windZone.size.x/2f, 0f);
			_direction = Direction._left;
		}

		public void SetRight(BoxCollider2D windZone)
		{
			windZone.size = new Vector2(_sizeY, _sizeX);
			windZone.offset = new Vector3(windZone.size.x/2f, 0f);
			_direction = Direction._right;
		}

		public void SetUp(BoxCollider2D windZone)
		{
			windZone.size = new Vector2(_sizeX, _sizeY);
			windZone.offset = new Vector2(0f, windZone.size.y/2f);
			_direction = Direction._up;
		}

		public void SetDown(BoxCollider2D windZone)
		{
			windZone.size = new Vector2(_sizeX, _sizeY);
			windZone.offset = new Vector3(0f, -windZone.size.y/2f);
			_direction = Direction._down;
		}
	}
}

