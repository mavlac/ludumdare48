using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Level : MonoBehaviour
{
	public Game game;
	[Tag] public string playerTag = "Player";

	[Header("Level Config")]
	public bool anchorCameraOnEnter = true;
	public bool splashWaterOnEnter = false;
	public bool underwaterPlayerVisual = true;

	[Space]
	public Collider2D entranceCollider;



	private void OnValidate()
	{
		if (!game) game = FindObjectOfType<Game>();
	}
	private void Awake()
	{
		Assert.IsNotNull(game);

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
			
			game.LevelEntered(this);
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