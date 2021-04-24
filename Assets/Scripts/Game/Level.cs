using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Level : MonoBehaviour
{
	[Tag] public string playerTag = "Player";

	[Header("Level Config")]
	public bool anchorCameraOnEnter = true;

	[Space]
	public Collider2D entranceCollider;



	private void Awake()
	{
		if (entranceCollider)
		{
			entranceCollider.enabled = false;
		}
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag(playerTag))
		{
			Debug.Log($"{playerTag} has entered {this.gameObject.name}");
		}
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag(playerTag))
		{
			Debug.Log($"{playerTag} has left {this.gameObject.name}");
			
			if (entranceCollider)
			{
				Debug.Log("Closing the entrance");
				entranceCollider.enabled = true;
			}
		}
	}
}