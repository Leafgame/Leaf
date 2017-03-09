using System.Collections;
using Assets.Scripts.PlayerScripts;
using UnityEngine;

namespace Assets.Scripts
{
    public class Door : MonoBehaviour
	{
        /// <summary>
        /// The entrance of the room 
        /// </summary>
		public Transform RoomEntranceLocation;

        /// <summary>
        /// Fade sprite TODO/butchered feature
        /// </summary>
		private SpriteRenderer _fadeSpriteRenderer;

        /// <summary>
        /// Alpha fade value
        /// </summary>
		private float _alphaFadeValue;

        /// <summary>
        /// If fading or not
        /// </summary>
		private bool _fadeIn;

        /// <summary>
        /// The amount of time it takes for the fade to complete
        /// </summary>
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
