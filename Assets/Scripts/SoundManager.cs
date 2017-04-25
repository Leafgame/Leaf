using Assets.Scripts.Settings;
using UnityEngine;

namespace Assets.Scripts
{
	public class SoundManager : Singleton<SoundManager>
	{
		public AudioClip[] SoundEffectClips;
		public AudioClip[] MusicSoundPlaylist;
		public AudioSource SoundEffectSource;
		public AudioSource MusicSoundSource;

		public SettingsManager Settings;

		private void Start()
		{
			MusicSoundSource = gameObject.AddComponent<AudioSource>();
			SoundEffectSource = gameObject.AddComponent<AudioSource>();
			Settings = GetComponentInChildren<SettingsManager>();
			PlaySong(0);
		}

		public void PlayEffect(int index)
		{
			SoundEffectSource.PlayOneShot(SoundEffectClips[index]);
		}

		public void PlaySong(int index)
		{
			MusicSoundSource.PlayOneShot(MusicSoundPlaylist[index]);
		}

		public void Update()
		{
			SoundEffectSource.volume = Settings.GameSettings.SoundEffectsVolume;
			MusicSoundSource.volume = Settings.GameSettings.MusicVolume;
		}


	}
}
