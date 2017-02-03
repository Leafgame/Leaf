using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
	public class ButtonClickEvents : MonoBehaviour
	{
		public GameObject OptionsPanel;
		/*
		 * Called when the user clicks GUI button Start
		 */
		public void GameStartButtonClicked()
		{
			SceneManager.LoadScene(1);
		}

		/*
		 * Called when the user clicks GUI button Credits
		 */
		public void CreditsButtonClicked()
		{
			print("TODO credits scene");
		}

		/*
		 * Called when the user clicks GUI button Options
		 */
		public void OptionsButtonClicked()
		{
			OptionsPanel.SetActive(!OptionsPanel.activeSelf);
		}

		/*
		 * Called when the user clicks GUI button Quit
		 */
		public void ExitButtonClicked()
		{
			Application.Quit();
		}

	    public void ResetLevelButtonClicked()
	    {
	        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	    }

	    public void SettingsButtonClicked()
	    {
            print("TODO options screen");
        }

		public static void ContinueButtonClicked()
		{
			SceneManager.LoadScene(PlayerPrefs.GetInt("CurrentProgress"));
		} 

    }
}
