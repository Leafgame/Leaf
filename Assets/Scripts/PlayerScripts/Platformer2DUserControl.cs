using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace Assets.Scripts.PlayerScripts
{
    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        /// <summary>
        /// Reference to the player controller script
        /// </summary>
        private PlatformerCharacter2D _character;

        /// <summary>
        /// If the player should jump or not
        /// </summary>
        private bool _jump;


        private void Awake()
        {
            _character = GetComponent<PlatformerCharacter2D>();
        }


        private void Update()
        {
            if (!_jump)
            {
                // Read the jump input in Update so button presses aren't missed.
                _jump = CrossPlatformInputManager.GetButtonDown("Fire1");
            }
        }


        private void FixedUpdate()
        {
            var h = CrossPlatformInputManager.GetAxis("Horizontal");
            // Pass all parameters to the character control script.
            _character.Move(h, _jump);
            _jump = false;
        }
    }
}
