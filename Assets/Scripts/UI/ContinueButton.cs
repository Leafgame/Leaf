﻿using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
	public class ContinueButton : MonoBehaviour
	{
		public GameObject ButtonGameObject;

		protected virtual void Start()
		{
			if (PlayerPrefs.HasKey("CurrentProgress")) //debug
			{
				CreateContinueButton();
			}
		}

		public void CreateContinueButton()
		{
			var button = Instantiate(ButtonGameObject, transform.position + new Vector3(0,60,0), Quaternion.identity, transform);
			button.GetComponentInChildren<Text>().text = "Continue";
			var uiButton = button.GetComponent<Button>();
			uiButton.onClick.AddListener(ButtonClickEvents.ContinueButtonClicked);
		}

	}
}
