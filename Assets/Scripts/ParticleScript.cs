using Assets.Scripts.WindScripts;
using UnityEngine;

namespace Assets.Scripts
{
	public class ParticleScript : MonoBehaviour
	{
		private BoxCollider2D _windZoneBox;
		public WindObject WindObjectRef;
		private new ParticleSystem particleSystem;
		private Transform _playerTransform;

		public void Start()
		{
			particleSystem = GetComponent<ParticleSystem>();
			particleSystem.Play();
			_windZoneBox = WindObjectRef.GetComponent<BoxCollider2D>();
			_playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		}

		public void Update()
		{
			if (!WindObjectRef.IsActive)
				particleSystem.enableEmission = false;
			else
				particleSystem.enableEmission = true;

			if (tag != "Particles") return;

			if(_windZoneBox.size.y > _windZoneBox.size.x)
				particleSystem.startLifetime = ( _windZoneBox.size.y - 2.58f) / 6.5f; 
			else
				particleSystem.startLifetime = ( _windZoneBox.size.x - 2.58f) / 6.5f;

		}

	}
}
