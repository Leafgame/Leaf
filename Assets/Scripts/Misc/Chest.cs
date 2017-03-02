using UnityEngine;

namespace Assets.Scripts.Misc
{
	public class Chest : MonoBehaviour
	{
		private Animator _animator;
		private void OnTriggerEnter2D(Collider2D col)
		{
			_animator = transform.parent.GetComponentInChildren<Animator>();
			_animator.SetBool("Open", true);
			print("Chest Opens");
		}

		private void OnTriggerExit2D(Collider2D col)
		{
			_animator.SetBool("Open", false);
			print( "Chest Closes" );
		}

	}
}
