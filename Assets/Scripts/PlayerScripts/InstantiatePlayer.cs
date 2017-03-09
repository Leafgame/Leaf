using UnityEngine;

namespace Assets.Scripts
{
	public class InstantiatePlayer : MonoBehaviour
	{
        /// <summary>
        /// Starting postion of the player in world space
        /// </summary>
		public Vector3 StartLocation = new Vector3(0, 2, 0);

        /// <summary>
        /// Player object
        /// </summary>
		public GameObject Prefab;

		public void Awake ()
		{
            var player = GameObject.FindGameObjectWithTag("Player");
            if (player == null)
                Instantiate(Prefab, StartLocation, Quaternion.identity);
            else
                player.transform.position = StartLocation;
		}
	}
}
