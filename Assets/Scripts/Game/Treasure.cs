using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using MavLib.CommonSound;

public class Treasure : MonoBehaviour
{
	[Tag] public string playerTag = "Player";

	[Header("Events")]
	public EventSO treasureReached;

	[Header("Sounds")]
	public AudioClip treasureReachedClip;



	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag(playerTag))
		{
			Collect();
		}
	}


	void Collect()
	{
		treasureReached.Raise();
		
		CommonSound.PlayFX(treasureReachedClip);
	}
}