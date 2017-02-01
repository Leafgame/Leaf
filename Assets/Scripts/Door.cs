using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.PlayerScripts;
using UnityEngine;

namespace Assets.Scripts
{
	public class Door : MonoBehaviour
	{
		public Transform RoomEntranceLocation;
		private SpriteRenderer _fadeSpriteRenderer;
		private float _alphaFadeValue;
		private bool _fadeOut;
		private bool _fadeIn;
		private float _fadeTime = 5f;


		public void Start()
		{
			_fadeSpriteRenderer = GameObject.FindGameObjectWithTag("Fade").GetComponent<SpriteRenderer>();
		}

		public void OnTriggerStay2D(Collider2D col)
		{
			if (col.tag == "Player" && Input.GetButtonDown("Vertical"))
			{
				col.transform.position = RoomEntranceLocation.position;
				var mCam = col.GetComponent<CameraBehaviour>().MainCameraView;
				mCam.transform.position = RoomEntranceLocation.position;
				_fadeSpriteRenderer.color = new Color(0, 0, 0, 255);
				_fadeIn = true;
			}

		}

		public IEnumerable FadeIn()
		{
			yield return new WaitForSeconds(_fadeTime);
			_fadeOut = false;
			_fadeIn = true;
		}

		public void Update()
		{
			if (_fadeIn)
			{
				_alphaFadeValue -= Mathf.Clamp01(Time.deltaTime / _fadeTime);
				_fadeSpriteRenderer.color = new Color(0,0,0,_alphaFadeValue);
			}
		}

	}
}
