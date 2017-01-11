using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[RequireComponent(typeof(BoxCollider2D))]
public class WindObject : MonoBehaviour
{
	private float windForce
	{
		get;
		set;
	}

	private float windSpeed
	{
		get;
		set;
	}

	private Vector3 windDirection
	{
		get;
		set;
	}

    private BoxCollider2D windTrigger
    {
        get;
        set;
    }


	public virtual void ApplyWindPhysics()
	{
		throw new System.NotImplementedException();
	}

    public void Start()
    {
        windTrigger = GetComponent<BoxCollider2D>();
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        print(col.transform.tag);
    }
}

