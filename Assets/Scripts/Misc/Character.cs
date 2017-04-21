using UnityEngine;

namespace Assets.Scripts.Misc
{
	/// <summary>
	/// Character super class contains the common logic between all characters
	/// </summary>
    public class Character : MonoBehaviour
    {
	    protected bool _facing;

        private int MovementSpeed
        {
            get;
            set;
        }

        public virtual void Move()
        {
            throw new System.NotImplementedException();
        }

		protected void Flip( )
		{
			// Switch the way the player is labelled as facing.
			_facing = !_facing;

			// Multiply the player's x local scale by -1.
			var theScale = transform.localScale;
			theScale.x *= -1;
			transform.localScale = theScale;
		}

	}
}

