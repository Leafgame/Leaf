﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool
//     Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using UnityEngine;

public class HeavyObject : MonoBehaviour
{
	private Rigidbody2D _rigidbody2D;
	public float PushForce = 100f;

	public void Start( )
	{
		_rigidbody2D = GetComponent<Rigidbody2D>();
	}

	public virtual BoxCollider2D windBlockZone
	{
		get;
		set;
	}

	public virtual void MoveObject( )
	{
		throw new System.NotImplementedException();
	}

	protected virtual void OnCollisionEnter2D( Collision2D col )
	{
		if (col.transform.tag == "Player")
		{
			
			print( col.relativeVelocity.x );
		}
	}

}

