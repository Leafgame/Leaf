using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Misc
{
	public struct SteeringOutput
	{
		public Vector3 Linear;
		public float Angular;
	}

    public class DarkGust : Character
    {
		/// <summary>
		/// The target it's currently following
		/// </summary>
		public Transform Target;

		/// <summary>
		/// The path its taking from 0-N as a collection of nodes
		/// </summary>
		public List<Transform> Path;

		/// <summary>
		/// If it should start from beginning or go backwards again
		/// </summary>
		public bool LoopPath = true;

		/// <summary>
		/// Holds the max acceleration
		/// </summary>
		public float MaxAcceleration;

	    /// <summary>
	    /// Holds the speed of the character
	    /// </summary>
	    public float MaxSpeed;

		/// <summary>
		/// Holds the radius for arriving at the target
		/// </summary>
		public float TargetRadius;

	    /// <summary>
	    /// Holds the radius for beginning to slow down
	    /// </summary>
	    public float SlowRadius;

	    /// <summary>
	    /// Holds the time over which to achieve target speed
	    /// </summary>
	    public float TimeToTarget = 0.1f;

		/// <summary>
		/// The velocity of the Agent
		/// </summary>
	    public Vector3 Velocity;

	    /// <summary>
	    /// The orientation of the Agent
	    /// </summary>
	    public float Orientation;

	    /// <summary>
	    /// The max amount of rotation per frame
	    /// </summary>
	    public float MaxRotation;

        /// <summary>
        /// The amount of Haunt Power
        /// </summary>
        private float HauntPower;


        public virtual void Haunt()
        {
            
        }

	    public SteeringOutput GetSteering(Transform target)
	    {
			// Get the direction to the target
			var steering = new SteeringOutput();
		    var direction = target.position - transform.position;
		    var distance = direction.magnitude;
		    float targetSpeed;

		    if (distance < TargetRadius)
		    {
			    return steering;
		    }
		    if (distance > SlowRadius)
		    {
			    targetSpeed = MaxSpeed;
		    }
		    // Otherwise calculate a scaled speed
		    else
		    {
			    targetSpeed = MaxSpeed*distance/SlowRadius;
		    }
			//The target velocity combines speed and direction
		    var targetVelociy = direction.normalized;
		    targetVelociy *= targetSpeed;

		    steering.Linear = targetVelociy - Velocity;
		    steering.Linear /= TimeToTarget;

			// Check if the acceleration is too fast
		    if (steering.Linear.magnitude > MaxAcceleration)
		    {
			    steering.Linear.Normalize();
			    steering.Linear *= MaxAcceleration;
		    }
		    steering.Angular = 0.0f;
		    return steering;
	    }

	    public SteeringOutput GetOrientation(Transform target)
	    {
		    var steering = new SteeringOutput();
			var rotation = target.rotation.z - transform.rotation.z;
			rotation = Mathf.Deg2Rad * rotation;

		    var rotationSize = Mathf.Abs(rotation);
		    float targetRotation;
			//Check if we are there, return no steering
			if (rotationSize < TargetRadius)
		    {
			    return steering;
		    }
		    if (rotationSize > SlowRadius)
		    {
			    targetRotation = MaxRotation;
		    }
		    //Otherwise calculate a scaled rotation
		    else
		    {
			    targetRotation = MaxRotation*rotationSize/SlowRadius;
		    }
			//The final target rotation combines speed (already in the variable) and direction
		    targetRotation *= rotation/rotationSize;

			// Acceleration tries to get to the target rotation
		    steering.Angular = targetRotation - Orientation;
		    steering.Angular /= TimeToTarget;

			// Check if the acceleration is too great
			var angularAcceleration = Mathf.Abs(steering.Angular);
		    if (angularAcceleration > MaxAcceleration)
		    {
			    steering.Angular /= angularAcceleration;
			    steering.Angular *= MaxAcceleration;
		    }
		    return steering;
	    }

		public override void Update()
	    {
		    var steering = GetSteering(Target);
			var orientation = GetOrientation(Target);
		    transform.position += Velocity * Time.deltaTime;
			transform.Rotate(new Vector3(0, 0, 1), Orientation*Time.deltaTime);

		    Velocity += steering.Linear*Time.deltaTime;
		    Orientation += orientation.Angular*Time.deltaTime;

		    var distance = Target.position - transform.position;
		    if (distance.magnitude < TargetRadius)
		    {
			    var temp = Path[0];
			    Path.Remove(temp);
			    Target = Path[0];
				Path.Add(temp);
		    }

			if (Velocity.x < 0 && _facingRight)
			{
				Flip();
			} else if (Velocity.x > 0 && !_facingRight)
			{
				Flip();
			}
	    }

	    public override void Start()
	    {
		    Target = Path[0];
	    }

	}
}

