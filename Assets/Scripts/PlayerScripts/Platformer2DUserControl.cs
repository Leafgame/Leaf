using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace Assets.Scripts.PlayerScripts
{
    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        private PlatformerCharacter2D _character;
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
            // Read the inputs.
            var crouch = Input.GetKey(KeyCode.LeftControl);
            var h = CrossPlatformInputManager.GetAxis("Horizontal");
            // Pass all parameters to the character control script.
            _character.Move(h, crouch, _jump);
            _jump = false;
        }
    }
}
