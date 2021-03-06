﻿using System;
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

		private Transform _particleSystems;

		public void Start()
		{
			_windZone = GetComponent<BoxCollider2D>();
			_sizeX = _windZone.size.x;
			_sizeY = _windZone.size.y;
			_particleSystems = transform.parent.FindChild("FlyingParticles");
		}

		public void LateUpdate()
		{
			switch (CurrentDirection)
			{
				case Direction._left:
					WindDirection = -transform.right;
					break;
				case Direction._right:
					WindDirection = transform.right;
					break;
				case Direction._down:
					WindDirection = -transform.up;
					break;
				case Direction._up:
					WindDirection = transform.up;
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
			RotateParticles(new Vector3(0, 0, -90));
			if (!Left) SetUp();
		}


		public void SetRight()
		{
			_windZone.size = new Vector2(_sizeY, _sizeX);
			_windZone.offset = new Vector3(_windZone.size.x/2f, 0f);
			CurrentDirection = Direction._right;
			RotateParticles(new Vector3(0, 0, -90));
			if (!Right) SetDown();
		}

		public void SetUp()
		{
			_windZone.size = new Vector2(_sizeX, _sizeY);
			_windZone.offset = new Vector2(0f, _windZone.size.y/2f);
			CurrentDirection = Direction._up;
			RotateParticles(new Vector3(0, 0, -90));
			if (!Up) SetRight();
		}

		public void SetDown()
		{
			_windZone.size = new Vector2(_sizeX, _sizeY);
			_windZone.offset = new Vector3(0f, -_windZone.size.y/2f);
			CurrentDirection = Direction._down;
			RotateParticles(new Vector3(0, 0, -90));
			if (!Down) SetLeft();
		}


		private void RotateParticles(Vector3 rotation)
		{
			_particleSystems.rotation *= Quaternion.Euler(rotation);
		}
	}
}

