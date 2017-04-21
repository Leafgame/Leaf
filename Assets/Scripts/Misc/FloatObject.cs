using UnityEngine;

namespace Assets.Scripts.Misc
{
	/// <summary>
	/// An object that will simulate a floating object with Sin/Cos waves.
	/// </summary>
	public class FloatObject : MonoBehaviour
	{
        /// <summary>
        /// The amount which the object will float based on cos/sin curves
        /// </summary>
	    public float FloatIntensity = 5.0f;

        /// <summary>
        /// The offset of the given air/wind model underneath for so the landing will function properly
        /// </summary>
		public float PlatformOffset = 1f;

        /// <summary>
        /// The inital startingposition height of the object
        /// </summary>
		protected Vector3 InitalPosition;

        /// <summary>
        /// The rigidbody of the floating object prefferbly its kinematic
        /// </summary>
		protected Rigidbody2D Rigidbody2D;

		/// <summary>
		/// If the wind is currently active
		/// </summary>
		public bool IsWindActive = true;


		protected virtual void Start()
		{
			InitalPosition = transform.position;
			Rigidbody2D = GetComponent<Rigidbody2D>();
		}

		/// <summary>
		/// Floating the object by changing the velocity of the rigidbody.
		/// </summary>
		public virtual void Float()
		{
		    var cosTheta = Mathf.Cos(Time.time) * FloatIntensity;
			Rigidbody2D.velocity = new Vector2(0, cosTheta);
		}


	    protected virtual void FixedUpdate()
	    {
			if (!IsWindActive)
		    {
		    }
		    else
		    {
				Float();
			}
		}


	}
}

