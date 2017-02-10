using UnityEngine;

namespace Assets.Scripts
{
	public class ParallaxOption : MonoBehaviour
	{
		public bool MoveParallax;

		[SerializeField]
		[HideInInspector]
		private Vector3 _storedPosition;

		public void SavePosition() {
			_storedPosition = transform.position;
		}

		public void RestorePosition() {
			transform.position = _storedPosition;
		}
	}
}