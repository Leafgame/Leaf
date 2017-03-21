using UnityEngine;

namespace Assets.Scripts.PlayerScripts
{
	[RequireComponent(typeof (PlayerItemsController))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        /// <summary>
        /// If the player should jump or not
        /// </summary>
        private bool _jump;

		/// <summary>
		/// Dash command button
		/// </summary>
		private Command DashLeftButton;

		/// <summary>
		/// Dash command button
		/// </summary>
		private Command DashRightButton;

		/// <summary>
		/// Jump command button
		/// </summary>
		private Command JumpButton;

		/// <summary>
		/// Horizontal Axis command
		/// </summary>
		private Command HorizontalAxis;

		/// <summary>
		/// Vertical axis command
		/// </summary>
		private Command VerticalAxis;

		/// <summary>
		/// Interaction button command
		/// </summary>
		private Command InteractButton;


		private void Awake()
        {
			DashLeftButton = new DashCommand(DashDirection.Left);
			DashRightButton = new DashCommand(DashDirection.Right);
			InteractButton = new InteractionCommand();
        }


        private void Update()
        {
			if (Input.GetButton("Fire2"))
			{
				InteractButton.Execute(gameObject);
			}
			else if (Input.GetButtonDown("DashLeft"))
			{
				DashLeftButton.Execute(gameObject);
			}
			else if (Input.GetButtonDown("DashRight"))
			{
				DashRightButton.Execute(gameObject);
			}
		}


        private void FixedUpdate()
        {

        }
    }
}
