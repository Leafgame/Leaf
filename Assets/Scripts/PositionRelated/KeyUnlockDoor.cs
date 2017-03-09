using CreativeSpore.SuperTilemapEditor;
using UnityEngine;

namespace Assets.Scripts
{
	public class KeyUnlockDoor : MonoBehaviour
	{
        /// <summary>
        /// Door open layer in a tilemap we're not gonna use tilemap so this needs to be 
        /// recreated for the same effect
        /// </summary>
		public Tilemap DoorOpenLayer;

        /// <summary>
        /// The doors the key will unlock
        /// </summary>
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
