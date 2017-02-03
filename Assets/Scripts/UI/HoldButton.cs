using UnityEngine;

public class HoldButton : MonoBehaviour
{
	public float JumpHoldTime = 0.5f;

	private void Update ()
    {
		if(Input.GetButton( "Fire1" ) && JumpHoldTime > 0)
		{
			Debug.Log( "Fire" );
			JumpHoldTime -= Time.deltaTime;
		}
	}
}
