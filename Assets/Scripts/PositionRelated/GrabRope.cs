using UnityEngine;

namespace Assets.Scripts
{
	/// <summary>
	/// TODO implement this
	/// </summary>
	public class GrabRope : MonoBehaviour
	{
		private bool _grabRope;
		private Transform _playerTransform;
		public void OnCollisionEnter2D(Collision2D col)
		{
			if (col.transform.tag == "Player" && Input.GetButton("Fire2"))
			{
				_grabRope = true;
				_playerTransform = col.transform;
			}
		}

		//this TODO
		public void Update()
		{
			if (Input.GetButtonUp("Fire2") && _playerTransform != null)
			{
				_grabRope = false;
				_playerTransform.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
			}
			if (_grabRope)
			{
				_playerTransform.position = transform.position;
			}
		}
	}
}

