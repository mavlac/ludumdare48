using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MavLib.CommonSound;
using UnityEngine.Assertions;

public class Harpoon : MonoBehaviour
{
	public Rigidbody2D rb;

	[Space]
	public float movementSpeed = 1f;
	public AnimationCurve movementProgression = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

	[Header("Sounds")]
	public AudioClip harpoonHitObstacle;


	public IEnumerator ShotCoroutine(Vector2 destination)
	{
		RotateTowards(destination);
		
		yield return StartCoroutine(MovementCoroutine(rb.position, destination));
		
		CommonSound.PlayFX(harpoonHitObstacle);
	}
	void RotateTowards(Vector2 destination)
	{
		// TODO
	}
	IEnumerator MovementCoroutine(Vector2 start, Vector2 destination)
	{
		var duration = Vector2.Distance(start, destination) * movementSpeed;

		for (float t = 0f; t < duration; t += Time.deltaTime)
		{
			float p = Mathf.InverseLerp(0f, duration, t);

			rb.MovePosition(Vector2.Lerp(start, destination, movementProgression.Evaluate(p)));

			yield return null;
		}

		rb.MovePosition(destination);
	}
}