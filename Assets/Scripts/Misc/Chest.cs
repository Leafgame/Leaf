using UnityEngine;

namespace Assets.Scripts.Misc
{
	public class Chest : MonoBehaviour
	{
		public Animator Animator;
		private void OnTriggerEnter2D(Collider2D col)
		{
			Animator.SetBool("Open", true);
			print("Chest Opens");
		}

		private void OnTriggerExit2D(Collider2D col)
		{
			Animator.SetBool("Open", false);
			print( "Chest Closes" );
		}

	}
}
