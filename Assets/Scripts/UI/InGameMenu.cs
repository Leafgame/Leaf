using Assets.Scripts.Settings;
using UnityEngine;

namespace Assets.Scripts.UI
{
	public class InGameMenu : MonoBehaviour
	{
		public GameObject InGameMenuObject;

		private void Start()
		{
			InGameMenuObject.SetActive(false);
			Unpause();
		}

		private void Update()
		{
			if (Input.GetButtonDown("Menu"))
			{
				if (!InGameMenuObject.activeSelf)
				{
					Pause();
				}
				else
				{
					Unpause();
				}
			}
		}

		public void Pause()
		{
			InGameMenuObject.SetActive(true);
			Time.timeScale = 0.0f;
		}

		public void Unpause()
		{
			InGameMenuObject.SetActive(false);
			Time.timeScale = 1;
			GetComponentInChildren<SettingsManager>().SaveChanges();
			GetComponent<ButtonClickEvents>().OptionsPanel.SetActive(false);
		}
	}
}
