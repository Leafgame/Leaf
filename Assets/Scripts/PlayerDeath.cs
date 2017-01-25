using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
	public class PlayerDeath : MonoBehaviour
	{
		// TODO make checkpoints and restart from most recent checkpoint
		public void OnTriggerEnter2D(Collider2D col)
		{
			if (col.tag == "Player")
			{
				SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			}
		}
	}
}
