using UnityEngine;

namespace Assets.Scripts.PlayerScripts
{
    public class PlayerItems : MonoBehaviour
    {
        /*  ===================================
         *  Player active items     = true 
         *  Player deactive items   = false
         *  ==================================
         */

	    protected void Awake()
	    {
		    DontDestroyOnLoad(gameObject);
	    }

        /// <summary>
        /// The glider
        /// </summary>
	    public bool HasGliderEquipped = false;

        /// <summary>
        /// The air dash
        /// </summary>
	    public bool HasAirDashEquipped = false;

        /// <summary>
        /// The forever exsisting/unexisting umbrella
        /// </summary>
	    public bool HasUmbrellaEquipped = false;

        /// <summary>
        /// Double jump
        /// </summary>
	    public bool HasDoubleJumpEquipped = false;

        /// <summary>
        /// Wind Negator
        /// </summary>
	    public bool HasWindNegationEquipped = false;

    }
}
