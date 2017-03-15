using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    /// <summary>
    /// Sound effect manager Singleton Instance
    /// </summary>
	public static SoundEffectManager Instance;

    /// <summary>
    /// The audio effects 
    /// </summary>
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
