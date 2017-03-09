﻿using Assets.Scripts.PlayerScripts;
using UnityEngine;

namespace Assets
{
	public class CameraController : MonoBehaviour
	{
		[SerializeField] private Camera _mainCamera;
		[SerializeField] private CameraBehaviour _standardCamera;
		[SerializeField] private NodeCamera _nodeCamera;


		protected void Start()
		{
			_mainCamera = Camera.main;
			_standardCamera = GameObject.FindGameObjectWithTag("Player").GetComponent<CameraBehaviour>();
			_nodeCamera = _mainCamera.GetComponent<NodeCamera>();
			SetToStandardCamera();
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
