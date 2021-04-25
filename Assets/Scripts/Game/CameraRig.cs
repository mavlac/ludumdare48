using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using DG.Tweening;

public class CameraRig : MonoBehaviour
{
	public float focusTranslationDuration = 2f;

	public void FocusOnLevel(Level level)
	{
		this.transform.DOMove(level.transform.position, focusTranslationDuration).SetEase(Ease.InOutCubic);
	}
}