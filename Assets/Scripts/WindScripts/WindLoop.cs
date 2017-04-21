using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(CircleCollider2D))]
public class WindLoop : MonoBehaviour
{
	public const float Radius = 5f;
	public const float T = Mathf.PI * 2 / Radius;

	private float angle;

	private void Start()
	{
		var circle = GetComponent<CircleCollider2D>();
		circle.radius = Radius;
		circle.offset = new Vector2(0, 0);
	}

	private void OnTriggerStay2D(Collider2D col)
	{
		angle += T * Time.deltaTime;
		transform.position = new Vector3(Mathf.Cos(angle) * Radius, Mathf.Sin(angle) * Radius);

		if(col.tag == "Player")
		{
			//var player = col.GetComponent<Player>(
		}
	}

}
