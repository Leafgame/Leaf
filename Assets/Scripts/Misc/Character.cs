using UnityEngine;

namespace Assets.Scripts.Misc
{
    public class Character : MonoBehaviour
    {
	    protected bool _facingRight;

        private int MovementSpeed
        {
            get;
            set;
        }

        public virtual void Move()
        {
            throw new System.NotImplementedException();
        }

        public virtual void Update()
        {
        }

        public virtual void Start()
        {
        }

		protected void Flip( )
		{
			// Switch the way the player is labelled as facing.
			_facingRight = !_facingRight;

			// Multiply the player's x local scale by -1.
			var theScale = transform.localScale;
			theScale.x *= -1;
			transform.localScale = theScale;
		}

	}
}

