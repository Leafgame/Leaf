using Assets.Scripts.WindScripts;
using UnityEngine;

public class ParticleScript : MonoBehaviour
{
	public WindObject WindObjectRef;
	private ParticleSystem particleSystem;

	public void Start()
	{
		particleSystem = GetComponent<ParticleSystem>();
		particleSystem.Play();
	}

	public void Update()
	{
		if (!WindObjectRef.IsActive)
			particleSystem.Pause();
	}

}
