using UnityEngine;

namespace Assets
{
	/// <summary>
	/// TODO implement this
	/// </summary>
	public class GrabRope : MonoBehaviour
	{
		private bool _grabRope;
		private ContactPoint2D _contactPoint2D;
		private Transform _playerTransform;
		public void OnCollisionEnter2D(Collision2D col)
		{
			print("Hey there");
			if (col.transform.tag == "Player" && Input.GetButton("Fire2"))
			{
				_grabRope = true;
				_contactPoint2D = col.contacts[0];
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
