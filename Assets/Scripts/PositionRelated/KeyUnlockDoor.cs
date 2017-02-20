using CreativeSpore.SuperTilemapEditor;
using UnityEngine;

namespace Assets.Scripts
{
	public class KeyUnlockDoor : MonoBehaviour
	{

		public Tilemap DoorOpenLayer;
		public GameObject[] DoorsToUnlock;

		public void Start()
		{
			foreach (var o in DoorsToUnlock)
			{
				o.SetActive(false);
			}
		}

		public void OnCollisionEnter2D(Collision2D col)
		{
			if (col.transform.tag == "Player")
			{
				DoorOpenLayer.IsVisible = true;

				foreach (var o in DoorsToUnlock)
				{
					o.SetActive(true);
				}
				Destroy(gameObject);
			}
		}
	}
}
