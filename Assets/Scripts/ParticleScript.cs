using Assets.Scripts.WindScripts;
using UnityEngine;

namespace Assets.Scripts
{
	public class ParticleScript : MonoBehaviour
	{
		private BoxCollider2D _windZoneBox;
		public WindObject WindObjectRef;
		private new ParticleSystem particleSystem;

		public void Start()
		{
			particleSystem = GetComponent<ParticleSystem>();
			particleSystem.Play();
			_windZoneBox = WindObjectRef.GetComponent<BoxCollider2D>();
		}

		public void Update()
		{
			if (!WindObjectRef.IsActive)
				particleSystem.enableEmission = false;
			else
				particleSystem.enableEmission = true;

			if(_windZoneBox.size.y > _windZoneBox.size.x)
				particleSystem.startLifetime = ( _windZoneBox.size.y - 2.08f) / 5.8f; 
			else
				particleSystem.startLifetime = ( _windZoneBox.size.x - 2.08f) / 5.8f;

		}

	}
}
