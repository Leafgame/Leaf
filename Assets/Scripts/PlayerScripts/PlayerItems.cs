using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.PlayerScripts
{
    public class PlayerItems : MonoBehaviour
    {
        /*  
         *  Player items active items = true 
         *  deactive items = false
         */

	    protected void Awake()
	    {
		    DontDestroyOnLoad(gameObject);
	    }

	    public bool HasGliderEquipped = false;
	    public bool HasAirDashEquipped = false;
	    public bool HasUmbrellaEquipped = false;
	    public bool HasDoubleJumpEquipped = false;
    }
}
