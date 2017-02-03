using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
	public class SettingsManager : MonoBehaviour
	{
		public Toggle FullscreenToggle;
		public Dropdown ResolutionDropdown;
		public Dropdown TextureQualityDropdown;
		public Dropdown AntiAliasingDropdown;
		public Dropdown OverallQuality;
		public Slider MusicVolumeSlider;
		public Slider SoundEffectSlider;
		public Button ApplyButton;

		public AudioSource MusicAudioSource;
		public AudioSource SoundEffectsAudioSource;
		public Resolution[] Resolutions;
		public GameSettings GameSettings;

		public void OnEnable()
		{
			GameSettings = new GameSettings();
			FullscreenToggle.onValueChanged.AddListener(delegate { OnFullscreenToggle(); });
			ResolutionDropdown.onValueChanged.AddListener(delegate { OnResolutionChanged(); });
			TextureQualityDropdown.onValueChanged.AddListener(delegate { OnTextureQualityChanged(); });
			AntiAliasingDropdown.onValueChanged.AddListener(delegate { OnAntiAliasingChanged(); });
			OverallQuality.onValueChanged.AddListener(delegate { OnOverallQualityChanged(); });
			MusicVolumeSlider.onValueChanged.AddListener(delegate { OnMusicVolumeChanged(); });
			SoundEffectSlider.onValueChanged.AddListener(delegate { OnSoundEffectsVolumeChanged(); });
			ApplyButton.onClick.AddListener(OnApplyButtonClicked);

			Resolutions = Screen.resolutions;

			foreach (var resolution in Resolutions)
			{
				ResolutionDropdown.options.Add(new Dropdown.OptionData(resolution.ToString()));
			}
			ResolutionDropdown.options.Reverse();
			LoadSettings();
		}

		public void OnFullscreenToggle()
		{
			GameSettings.Fullscreen = Screen.fullScreen = FullscreenToggle.isOn;
		}

		public void OnResolutionChanged()
		{
			Screen.SetResolution(Resolutions[Resolutions.Length-ResolutionDropdown.value-1].width, Resolutions[Resolutions.Length-ResolutionDropdown.value-1].height, Screen.fullScreen);
			GameSettings.ResolutionIndex = Resolutions.Length - ResolutionDropdown.value - 1;
		}

		public void OnTextureQualityChanged()
		{
			GameSettings.TextureQuality = QualitySettings.masterTextureLimit = TextureQualityDropdown.value;
		}

		public void OnAntiAliasingChanged()
		{
			GameSettings.AntiAliasing = QualitySettings.antiAliasing = (int)Mathf.Pow(2f, AntiAliasingDropdown.value);
		}

		public void OnOverallQualityChanged()
		{
			QualitySettings.SetQualityLevel(OverallQuality.value);
			GameSettings.OverallQuality = QualitySettings.GetQualityLevel();
		}

		public void OnMusicVolumeChanged()
		{
			GameSettings.MusicVolume = MusicAudioSource.volume = MusicVolumeSlider.value;
		}

		public void OnSoundEffectsVolumeChanged()
		{
			GameSettings.SoundEffectsVolume = SoundEffectsAudioSource.volume = SoundEffectSlider.value;
		}

		public void OnApplyButtonClicked()
		{
			var jsonData = JsonUtility.ToJson(GameSettings, true);
			File.WriteAllText(Application.persistentDataPath + "/gamesettings.json", jsonData);
		}

		public void LoadSettings()
		{
			GameSettings = JsonUtility.FromJson<GameSettings>(File.ReadAllText(Application.persistentDataPath + "/gamesettings.json"));
			MusicVolumeSlider.value = GameSettings.MusicVolume;
			SoundEffectSlider.value = GameSettings.SoundEffectsVolume;
			ResolutionDropdown.value = GameSettings.ResolutionIndex;
			OverallQuality.value = GameSettings.OverallQuality;
			AntiAliasingDropdown.value = GameSettings.AntiAliasing;
			TextureQualityDropdown.value = GameSettings.TextureQuality;
			FullscreenToggle.isOn = GameSettings.Fullscreen;

			ResolutionDropdown.RefreshShownValue();
			OverallQuality.RefreshShownValue();
			AntiAliasingDropdown.RefreshShownValue();
			TextureQualityDropdown.RefreshShownValue();
		}
	}

}
