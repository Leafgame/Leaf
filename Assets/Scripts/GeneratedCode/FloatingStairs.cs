﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class FloatingStairs : FloatObject
{
	public virtual float Height
	{
		get;
		set;
	}

	public virtual float Width
	{
		get;
		set;
	}

	public virtual float StepHeight
	{
		get;
		set;
	}

	public virtual void CreateStairs()
	{
		throw new System.NotImplementedException();
	}

}

