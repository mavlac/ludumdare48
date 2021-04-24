using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Transition : MonoBehaviour
{
	public SpriteRenderer sr;
	public List<Sprite> openingSequence;

	public float stepDuration = 0.15f;



	public void FadeIn()
	{
		StartCoroutine(FadeInCoroutine());
	}
	IEnumerator FadeInCoroutine()
	{
		sr.enabled = true;
		
		for (int i = 0; i < openingSequence.Count; i++)
		{
			sr.sprite = openingSequence[i];

			yield return new WaitForSeconds(stepDuration);
		}

		sr.enabled = false;
	}
	public void FadeOut()
	{
		StartCoroutine(FadeOutCoroutine());
	}
	IEnumerator FadeOutCoroutine()
	{
		sr.enabled = true;
		
		for (int i = openingSequence.Count - 1; i >= 0; i--)
		{
			sr.sprite = openingSequence[i];
			
			yield return new WaitForSeconds(stepDuration);
		}
	}
}