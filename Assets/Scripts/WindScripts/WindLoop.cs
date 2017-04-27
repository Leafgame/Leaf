using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(CircleCollider2D))]
public class WindLoop : MonoBehaviour
{
	public const float Radius = 2f;
	public const float TwoPI = Mathf.PI * 2;

	private float dS;
	private float dT;
	private float thetha;

	private void Start()
	{
		var circle = GetComponent<CircleCollider2D>();
		//circle.radius = Radius;
		//circle.offset = new Vector2(0, 0);
	}

	private void OnTriggerStay2D(Collider2D col)
	{
		// get location to player calculate the radius vector
		// take the raduis and find the perpendicular vector to that vector
		// that vector is tangent to the circle add that as a velocity to the motion
		// keep doing that for every frame

		var toTarget = col.transform.position - transform.position;
		var radiusVec = toTarget.normalized * Radius;
		var normal = new Vector2(radiusVec.y, -radiusVec.x);

		thetha += TwoPI * Time.deltaTime / Radius;
		if (thetha > TwoPI) thetha = 0.0f;

		var position = new Vector3(Mathf.Cos(thetha) * Radius, Mathf.Sin(thetha) * Radius);

		var w = new Vector2(radiusVec.x, radiusVec.y) + normal;
		var desiredVel = w - new Vector2(toTarget.x, toTarget.y);

		if(col.tag == "Player" && Mathf.Abs(Input.GetAxis("Horizontal")) < 0.1)
		{
			col.GetComponent<Player>().velocity = new Vector2(0.0f, 0.0f);
			col.transform.position = Vector3.Lerp(toTarget, position + transform.position , Time.deltaTime);
			
		}
		dS = Radius * thetha;
		dT = Time.deltaTime;
	}

}
