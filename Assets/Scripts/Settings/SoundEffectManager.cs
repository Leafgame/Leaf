using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
	public static SoundEffectManager Instance;

	public AudioClip[] AudioClips;

	protected void Awake()
	{
		if (Instance != null)
		{
			Debug.LogError("Multiple instances of SoundEffectsManager");
		}
	}

	public void MakeSound(int index)
	{
		AudioSource.PlayClipAtPoint(AudioClips[index], transform.position);
	}
}
