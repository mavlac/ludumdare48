using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using DG.Tweening;

public class GameIntro : MonoBehaviour
{
	public Transform cameraInitialPosition;
	public Transform cameraTargetPosition;
	public Transform cameraRig;

	[Space]
	public float preDelay = 1.5f;
	public float duration = 1.5f;


	public void Play(System.Action onFinished)
	{
		cameraRig.transform.position = cameraInitialPosition.position;

		cameraRig.transform.
			DOMove(cameraTargetPosition.position, duration).
			SetDelay(preDelay).
			SetEase(Ease.InOutCubic).
			OnComplete(() => { onFinished.Invoke(); });
	}
}