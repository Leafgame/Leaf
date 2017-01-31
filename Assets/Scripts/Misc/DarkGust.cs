using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Misc
{
    public class DarkGust : Character
    {
        /*
         * The target it's currently following
         */
	    public Transform Target;

		/*
		 * The path its taking from 0-N as a collection of nodes
		 */
	    public List<Transform> Path;

		/*
		 * If it should start from beginning or go backwards again
		 */
	    public bool LoopPath = true;

		
        private float hauntPower
        {
            get;
            set;
        }

        public virtual void Haunt()
        {
            throw new System.NotImplementedException();
        }

    }
}

