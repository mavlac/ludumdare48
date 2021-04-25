using System.Collections;
using System.Collections.Generic;
using MavLib.CommonSound;
using UnityEngine;
using UnityEngine.Assertions;

public class Player : MonoBehaviour
{
	public Game game;
	
	
	[Header("Events")]
	public EventSO jumpFromBoat;

	[Header("Sounds")]
	public AudioClip movementClip;
	public AudioClip jumpFromBoatClip;
	
	
	
	public void InputControlInitiated()
	{
		CommonSound.PlayFX(movementClip);
	}
	public void JumpedFromBoat()
	{
		CommonSound.PlayFX(jumpFromBoatClip);
		
		jumpFromBoat.Raise();
	}
}