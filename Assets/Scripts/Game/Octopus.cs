using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using MavLib.CommonSound;

public class Octopus : MonoBehaviour
{
	[Tag] public string playerTag = "Player";
	[Tag] public string harpoonTag = "Harpoon";

	[Space]
	public Collider2D col;
	public SpriteSwitch deathAnimation;

	[Header("Events")]
	public EventSO killedByOctopus;

	[Header("Sounds")]
	public AudioClip playerKilledClip;
	public AudioClip enemyEliminated;



	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag(playerTag))
		{
			KillPlayer();
		}
		else if (collision.CompareTag(harpoonTag))
		{
			Die();
		}
	}



	void KillPlayer()
	{
		killedByOctopus.Raise();
		
		CommonSound.PlayFX(playerKilledClip);
	}
	void Die()
	{
		//event of killed octopus if any
		
		CommonSound.PlayFX(enemyEliminated);
		
		col.enabled = false;
		
		deathAnimation.TriggerAnimation();
		// Destroys self when animation completed
	}
}