using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

namespace Assets.Scripts
{

	public class LevelSwitcher : MonoBehaviour
	{
		[Header("Next Scene Name")]
		public string NextSceneName;

		[Header("Loading Screen")]
		public Image BackgroundImage;
		public float LoadProgress;
		public Scrollbar ProgressBar;

		[SerializeField]
		private Text _loadingText;

		private bool _loading;

		public void Update()
		{
		}

		public void LoadScene(string sceneName)
		{
			_loadingText.text = "Loading...";
			BackgroundImage.enabled = true;
			ProgressBar.gameObject.SetActive(true);
			StartCoroutine("LoadingScreen", sceneName);
		}

		private IEnumerator LoadingScreen(string sceneName)
		{
			var loader = SceneManager.LoadSceneAsync(sceneName);
			while (!loader.isDone)
			{
				ProgressBar.size = loader.progress;
				yield return null;
			}
			_loading = false;
			yield return null;
		}

		public void OnTriggerEnter2D(Collider2D col)
		{
			print("Loading");
			if (col.tag == "Player" && !_loading)
			{
				LoadScene(NextSceneName);
				_loading = true;
			}
		}
	}
}
