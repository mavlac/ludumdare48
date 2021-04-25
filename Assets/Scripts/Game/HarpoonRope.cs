using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class HarpoonRope : MonoBehaviour
{
	public SpriteRenderer spriteRenderer;
	public Transform player;



	private void Update()
	{
		var length = Vector2.Distance(this.transform.position, player.position);

		// Evil hack
		length *= 1.6f;

		this.transform.localPosition = new Vector3(-length * 0.5f, 0f, 0f);

		spriteRenderer.size = new Vector2(length, spriteRenderer.size.y);
	}
}