using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Super-simple audio playback functionality on top of singleton MonoBehaviour AudioSource holder
/// </summary>
namespace MavLib.CommonSound
{
	public class CommonSound : MonoBehaviour
	{
		public static CommonSound Instance { get; private set; }
		
		
		public bool dontDestroyOnLoad = false;
		
		[Space, SerializeField]
		private AudioSource audioSourceFX = default;
		[SerializeField]
		private AudioSource audioSourceVO = default;
		
		
		
		private void Awake()
		{
			// Singleton pattern management
			if (Instance == null) Instance = this;
			else if (Instance != this) Destroy(this.gameObject);
			
			if (dontDestroyOnLoad) DontDestroyOnLoad(this.gameObject);
		}
		
		
		
		private void PlayExclusive(AudioSource audioSource, AudioClip clip)
		{
			if (audioSource.isPlaying) audioSource.Stop();
			audioSource.clip = clip;
			audioSource.Play();
		}
		private void PlayOneShot(AudioSource audioSource, AudioClip clip)
		{
			audioSource.PlayOneShot(clip);
		}
		private void PlayOneShot(AudioSource audioSource, AudioClip clip, float volume)
		{
			audioSource.PlayOneShot(clip, volume);
		}
		
		
		
		public static void PlayFX(AudioClip clip)
		{
			PlayFX(clip, 1f, false);
		}
		public static void PlayFX(AudioClip clip, float volume)
		{
			PlayFX(clip, volume, false);
		}
		public static void PlayFX(AudioClip clip, bool exclusive)
		{
			PlayFX(clip, 1f, exclusive);
		}
		public static void PlayFX(AudioClip clip, float volume, bool exclusive)
		{
			if (Instance == null) return;
			if (clip == null) return;

			if (exclusive)
			{
				if (volume != 1f) Debug.LogWarning($"Playing exclusive clip {clip.name} with non-1 volume not supported.");
				
				Instance.PlayExclusive(Instance.audioSourceFX, clip);
			}
			else
			{
				Instance.PlayOneShot(Instance.audioSourceFX, clip, volume);
			}
		}
		public static void PlayVO(AudioClip clip)
		{
			PlayVO(clip, true);
		}
		public static void PlayVO(AudioClip clip, bool exclusive)
		{
			Assert.IsNotNull(Instance, $"{nameof(CommonSound)} instance not present.");
			if (clip == null) return;
			
			if (exclusive)
			{
				Instance.PlayExclusive(Instance.audioSourceVO, clip);
			}
			else
			{
				Instance.PlayOneShot(Instance.audioSourceVO, clip);
			}
		}
		
		public static void StopBothChannels()
		{
			Instance.audioSourceFX.Stop();
			Instance.audioSourceVO.Stop();
		}
		
		
		
		public static bool FXChannelIsPlaying => Instance.audioSourceVO.isPlaying;
		public static bool VOChannelIsPlaying => Instance.audioSourceVO.isPlaying;
	}
}
