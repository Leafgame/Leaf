using UnityEngine;

namespace Assets.Scripts
{
	public class InstantiatePlayer : MonoBehaviour
	{
		public Vector3 StartLocation = new Vector3(0, 2, 0);

		public GameObject Prefab;

		public void Awake ()
		{
			Instantiate( Prefab, StartLocation, Quaternion.identity );
		}
	}
}
