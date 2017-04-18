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

        /// <summary>
        /// If to use a background image or not
        /// </summary>
	    [Header("Loading Screen")]
        public bool UseBackgoundImage;

        /// <summary>
        /// If to use progressbar or not
        /// </summary>
	    public bool UseProgressBar;

        /// <summary>
        /// if to use on screen loading text or not
        /// </summary>
	    public bool UseLoadText;

        /// <summary>
        /// The background image to the loading screen
        /// </summary>
		public Image BackgroundImage;

        /// <summary>
        /// The current loading progress
        /// </summary>
		public float LoadProgress;

        /// <summary>
        /// The progress bar
        /// </summary>
		public Scrollbar ProgressBar;

        /// <summary>
        /// The text to use when UseLoadingText is enabled
        /// </summary>
		[SerializeField]
		private Text _loadingText;

        /// <summary>
        /// If we're loading or not
        /// </summary>
		private bool _loading;

		public void Start()
		{
			PlayerPrefs.SetInt("CurrentProgress", SceneManager.GetActiveScene().buildIndex);
		}

		public void LoadScene(string sceneName)
		{
            if(UseLoadText)
			    _loadingText.text = "Loading...";
            if(UseBackgoundImage)
			    BackgroundImage.enabled = true;
            if(UseProgressBar)
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

		public void OnTriggerStay2D(Collider2D col)
		{
			if (!_loading && col.tag == "Player")
			{
				LoadScene(NextSceneName);
				_loading = true;
			}
		}
	}
}
