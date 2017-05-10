using UnityEngine;

public class HeavyObject : MonoBehaviour
{
	/// <summary>
	/// The initial position of the object when the game starts.
	/// Use this to reset the postion of the object if it gets out of bounds
	/// </summary>
	public Vector3 InitialPosition;

	[Range(0f, 1f)]
	public float PushFactor = .25f;

	private Rigidbody2D _rigidbody;

	public void Start()
	{
		InitialPosition = transform.position;
		_rigidbody = GetComponent<Rigidbody2D>();
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
			var colrb = col.gameObject.GetComponent<Player>();
			_rigidbody.velocity = colrb.velocity * PushFactor;
		}
	}

}

