using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts.Controllers
{
	public class OnVisibleScript : MonoBehaviour
	{
		private List<GameObject> _gameObjects = new List<GameObject>();

		private void Start()
		{
			var allObjects = FindObjectsOfType<GameObject>();
			_gameObjects.AddRange(allObjects);
		}

		private void OnBecameVisible()
		{
			gameObject.SetActive(true);
			print("Called visible");
		}

		private void OnBecameInvisible()
		{
			gameObject.SetActive(false);
			print("Called invisible");
		}
		
	}
}
