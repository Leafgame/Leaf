using UnityEngine;

namespace Assets.Scripts
{
    public class AirLift : MonoBehaviour
    {
	    public Vector2 WindForce = new Vector2( 0, 20 );

		protected void OnTriggerStay2D( Collider2D other )
        {
            if(other.tag == "Player")
            {
                other.GetComponent<Rigidbody2D>().AddForce( WindForce );
            }
        }
    }
}
