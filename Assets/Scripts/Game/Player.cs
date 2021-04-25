using System.Collections;
using System.Collections.Generic;
using MavLib.CommonSound;
using UnityEngine;
using UnityEngine.Assertions;

public class Player : MonoBehaviour
{
	public Game game;

	[Space]
	public SpriteRenderer visual;
	public Sprite vesselVisual;
	public Sprite underwaterVisual;

	[Header("Components")]
	public PlayerLauncher launcher;

	
	[Header("Events")]
	public EventSO jumpFromBoat;
	public EventSO movementInitiated;
	public EventSO movementFinished;

	[Header("Sounds")]
	public AudioClip movementClip;
	public AudioClip movementFinishedClip;



	public Level CurrentLevel { get; private set; } = null;



	public void InputControlTriggered()
	{
		CommonSound.PlayFX(movementClip);
	}
	public void MovementInitiated()
	{
		movementInitiated.Raise();
		
		game.UpdatePhase(Game.GamePhase.Movement);
	}
	public void MovementFinished()
	{
		CommonSound.PlayFX(movementFinishedClip);
		
		movementFinished.Raise();
		
		game.UpdatePhase(Game.GamePhase.Idle);
	}




	public void JumpedFromBoat()
	{
		jumpFromBoat.Raise();
	}
	public void EnteredLevel(Level level)
	{
		CurrentLevel = level;
		
		visual.sprite = (CurrentLevel.isUnderwater) ? underwaterVisual : vesselVisual;
	}
}