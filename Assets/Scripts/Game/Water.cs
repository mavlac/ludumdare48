using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using MavLib.CommonSound;

public class Water : MonoBehaviour
{
	public SpriteRenderer splashVisual;
	public List<Sprite> splashStepAnimation;
	public float stepLength = 0.15f;

	[Header("Sounds")]
	public AudioClip splashClip;



	private void Awake()
	{
		splashVisual.enabled = false;
	}



	public void Splash()
	{
		CommonSound.PlayFX(splashClip);

		StartCoroutine(SplashStepAnimationCoroutine());
	}
	IEnumerator SplashStepAnimationCoroutine()
	{
		splashVisual.enabled = true;

		for (int i = 0; i < splashStepAnimation.Count; i ++)
		{
			splashVisual.sprite = splashStepAnimation[i];

			yield return new WaitForSeconds(stepLength);
		}
		
		splashVisual.enabled = false;
	}
}