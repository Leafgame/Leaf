using UnityEngine;

namespace Assets.Scripts.Misc
{
	/// <summary>
	/// This class represent the logic for opening and closing a chest.
	/// </summary>
	public class Chest : MonoBehaviour
	{
		/// <summary>
		/// Tge animator refence to open the chest
		/// </summary>
		private Animator _animator;

		/// <summary>
		/// The chest will open once the player enters the on trigger zone
		/// </summary>
		/// <param name="col"></param>
		private void OnTriggerEnter2D(Collider2D col)
		{
			_animator = transform.parent.GetComponentInChildren<Animator>();
			_animator.SetBool("Open", true);
			print("Chest Opens");
		}

		/// <summary>
		/// The chest closes once the player leaves the zone
		/// </summary>
		/// <param name="col"></param>
		private void OnTriggerExit2D(Collider2D col)
		{
			_animator.SetBool("Open", false);
			print( "Chest Closes" );
		}

	}
}
