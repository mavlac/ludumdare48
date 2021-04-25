using System.Collections;
using UnityEngine;

public class LoopedAmbience : MonoBehaviour
{
	public AudioSource audioSource;
	
	public float fadeDuration = 0.5f;
	
	float defaultVolume;
	
	
	
	private void OnValidate()
	{
		if (!audioSource)
		{
			audioSource = GetComponent<AudioSource>();
			audioSource.loop = true;
		}
	}
	private void Awake()
	{
		defaultVolume = audioSource.volume;
	}



	public void PlayWithFadeIn()
	{
		if (audioSource.isPlaying) return;
		
		audioSource.volume = 0;
		audioSource.Play();
		StopAllCoroutines();
		StartCoroutine(FadeInCoroutine());
	}
	IEnumerator FadeInCoroutine()
	{
		for (float t = 0; t <= fadeDuration; t += Time.unscaledDeltaTime)
		{
			audioSource.volume = Mathf.Lerp(0f, defaultVolume, Mathf.InverseLerp(0f, fadeDuration, t));
			yield return null;
		}
		audioSource.volume = defaultVolume;
	}

	public void StopWithFadeout()
	{
		if (!audioSource.isPlaying) return;
		
		StopAllCoroutines();
		StartCoroutine(FadeOutAndStopCoroutine());
	}
	IEnumerator FadeOutAndStopCoroutine()
	{
		float initialVolume = audioSource.volume;
		for (float t = 0; t <= fadeDuration; t += Time.unscaledDeltaTime)
		{
			audioSource.volume = Mathf.Lerp(initialVolume, 0f, Mathf.InverseLerp(0f, fadeDuration, t));
			yield return null;
		}
		audioSource.Stop();
	}
}