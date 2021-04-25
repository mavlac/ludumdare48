using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using MavLib.CommonSound;

public class Octopus : MonoBehaviour
{
	[Tag] public string playerTag = "Player";

	[Header("Events")]
	public EventSO killedByOctopus;

	[Header("Sounds")]
	public AudioClip playerKilledClip;



	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag(playerTag))
		{
			KillPlayer();
		}
	}


	void KillPlayer()
	{
		killedByOctopus.Raise();
		
		CommonSound.PlayFX(playerKilledClip);
	}
}