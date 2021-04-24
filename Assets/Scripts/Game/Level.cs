using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Level : MonoBehaviour
{
	[Tag] public string playerTag = "Player";

	[Header("Level Config")]
	public bool anchorCameraOnEnter = true;



	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag(playerTag))
		{
			Debug.Log($"{playerTag} has entered {this.gameObject.name}");
		}
	}
}