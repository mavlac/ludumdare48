using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using DG.Tweening;
using MavLib.CommonSound;

public class GameIntro : MonoBehaviour
{
	public Transform cameraInitialPosition;
	public Transform cameraTargetPosition;
	public Transform cameraRig;

	[Space]
	public float preDelay = 1.5f;
	public float duration = 1.5f;

	[Header("Audio")]
	public AudioClip introClip;
	public float introClipPreDelay = 1f;



	public void Play(System.Action onFinished)
	{
		cameraRig.transform.position = cameraInitialPosition.position;

		cameraRig.transform.
			DOMove(cameraTargetPosition.position, duration).
			SetDelay(preDelay).
			SetEase(Ease.InOutCubic).
			OnComplete(() => { onFinished.Invoke(); });

		this.Invoke(PlayIntroAudioClip, introClipPreDelay);
	}
	void PlayIntroAudioClip()
	{
		CommonSound.PlayFX(introClip);
	}
}