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
        public bool UseBackgoundImage;
	    public bool UseProgressBar;
	    public bool UseLoadText;
		public Image BackgroundImage;
		public float LoadProgress;
		public Scrollbar ProgressBar;

		[SerializeField]
		private Text _loadingText;

		private bool _loading;

		public void Start()
		{
			PlayerPrefs.SetInt("CurrentProgress", SceneManager.GetActiveScene().buildIndex);
		}

		public void Update()
		{
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
			if (!_loading && Input.GetButtonDown("Vertical") && col.tag == "Player")
			{
				LoadScene(NextSceneName);
				_loading = true;
			}
		}
	}
}
