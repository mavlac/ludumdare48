using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using MavLib.CommonSound;

public class Coin : MonoBehaviour
{
	[Tag] public string playerTag = "Player";

	[Space]
	public Collider2D col;

	[Header("Events")]
	public EventSO coinCollected;

	[Header("Sounds")]
	public AudioClip coinCollectedClip;



	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag(playerTag))
		{
			Collect();
		}
	}


	void Collect()
	{
		coinCollected.Raise();

		CommonSound.PlayFX(coinCollectedClip);

		col.enabled = false;

		Destroy(this.gameObject, 0.1f);
	}
}