using Assets.Scripts.WindScripts;
using UnityEngine;

public class WindAnimatorController : MonoBehaviour
{
	public Animator Animator;
	private WindObject _windObject;

	private void Start()
	{
		_windObject = GetComponent<WindObject>();
	}
	private void Update ()
	{
		if (_windObject.IsActive)
		{
			Animator.speed = 1;
		}
		else
		{
			Animator.speed = 0;
		}
	}
}
