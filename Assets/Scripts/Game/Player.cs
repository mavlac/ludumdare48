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

	//[Header("Components")]

	
	[Header("Events")]
	public EventSO jumpFromBoat;

	[Header("Sounds")]
	public AudioClip movementClip;
	
	
	
	public void InputControlInitiated()
	{
		CommonSound.PlayFX(movementClip);
	}
	public void JumpedFromBoat()
	{
		jumpFromBoat.Raise();
	}
	public void EnteredLevel(Level level)
	{
		visual.sprite = (level.underwaterPlayerVisual) ? underwaterVisual : vesselVisual;
	}
}