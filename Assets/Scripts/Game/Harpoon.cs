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
		
		yield return StartCoroutine(MovementCoroutine(this.transform.position, destination));
		
		CommonSound.PlayFX(harpoonHitObstacle);
	}
	void RotateTowards(Vector2 destination)
	{
		if (this.transform.position.x < destination.x - 0.1f)
		{
			// Right
			this.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
		}
		else if (this.transform.position.x > destination.x + 0.1f)
		{
			// Left
			this.transform.localRotation = Quaternion.Euler(0f, 0f, 180f);
		}
		else if (this.transform.position.y < destination.y - 0.1f)
		{
			// Up
			this.transform.localRotation = Quaternion.Euler(0f, 0f, 90f);
		}
		else if (this.transform.position.y > destination.y + 0.1f)
		{
			// Down
			this.transform.localRotation = Quaternion.Euler(0f, 0f, -90f);
		}
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