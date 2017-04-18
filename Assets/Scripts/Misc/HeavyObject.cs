using UnityEngine;

public class HeavyObject : MonoBehaviour
{
	/// <summary>
	/// The initial position of the object when the game starts.
	/// Use this to reset the postion of the object if it gets out of bounds
	/// </summary>
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

