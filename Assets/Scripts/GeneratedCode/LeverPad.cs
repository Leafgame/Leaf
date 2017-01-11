using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class LeverPad
{
	public virtual WindPad[] connectedWindPads
	{
		get;
		set;
	}

	public virtual WindPad WindPad
	{
		get;
		set;
	}

	public virtual void ActivateWindPad()
	{
		throw new System.NotImplementedException();
	}

}

