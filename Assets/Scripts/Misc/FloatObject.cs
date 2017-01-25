using UnityEngine;

namespace Assets.Scripts.GeneratedCode
{
	public class FloatObject : MonoBehaviour
	{
	    public float FloatIntensity = 5.0f;

		public virtual void Float()
		{
		    var cosTheta = Mathf.Cos(Time.time) * FloatIntensity;
			GetComponent<Rigidbody2D>().velocity = new Vector2(0, cosTheta);
		}


	    private void Update()
	    {
	        Float();
	    }
	}
}

