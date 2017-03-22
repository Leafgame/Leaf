using UnityEngine;

public class HeavyObject : MonoBehaviour
{
	public Vector3 InitialPosition;

	public void Start()
	{
		InitialPosition = transform.position;
	}

	public virtual BoxCollider2D WindBlockZone
	{
		get;
		set;
	}

	public virtual void MoveObject()
	{
		throw new System.NotImplementedException();
	}

	protected virtual void OnCollisionEnter2D(Collision2D col)
	{
		if (col.transform.tag == "Player")
		{
			print(col.relativeVelocity.x);
		}
	}

}

