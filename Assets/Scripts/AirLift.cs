using UnityEngine;

public class AirLift : MonoBehaviour
{
	protected void OnTriggerStay2D( Collider2D other )
	{
		if(other.tag == "Player")
		{
			other.GetComponent<Rigidbody2D>().AddForce(new Vector2( 0, 20 ));
		}
		
	}
}
