using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
	public class ButtonClickEvents : MonoBehaviour
	{
        /// <summary>
        /// Options panel
        /// </summary>
		public GameObject OptionsPanel;

		/// <summary>
		/// Called when the user clicks GUI button Start
		/// </summary>
		public void GameStartButtonClicked()
		{
			SceneManager.LoadScene(1);
		}

		/// <summary>
		/// Called when the user clicks GUI button Credits
		/// </summary>
		public void CreditsButtonClicked()
		{
			print("TODO credits scene");
		}

		/// <summary>
		/// Called when the user clicks GUI button Options
		/// </summary>
		public void OptionsButtonClicked()
		{
			OptionsPanel.SetActive(!OptionsPanel.activeSelf);
		}

		/// <summary>
		/// Called when the user clicks GUI button Quit
		/// </summary>
		public void ExitButtonClicked()
		{
			Application.Quit();
		}

		/// <summary>
		/// Resets the currently active scene
		/// </summary>
	    public void ResetLevelButtonClicked()
	    {
	        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            
	    }

		/// <summary>
		/// Opens the settings menu
		/// </summary>
	    public void SettingsButtonClicked()
	    {
            print("TODO options screen");
        }

		/// <summary>
		/// Loads the scene that is saved in the current proggres playerprefs.
		/// </summary>
		public static void ContinueButtonClicked()
		{
			SceneManager.LoadScene(PlayerPrefs.GetInt("CurrentProgress"));
		} 

    }
}
