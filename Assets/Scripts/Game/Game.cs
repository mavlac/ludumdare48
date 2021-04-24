using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Game : MonoBehaviour
{
	public const float GridSize = 0.16f;

	public enum GamePhase { Intro, Idle, Movement }


	public GameIntro intro;
	public bool skipIntroInEditor = false;

	[Space]
	public Credits credits;
	public Help help;


	public GamePhase Phase { get; private set; } = GamePhase.Intro;



	private void Start()
	{
		if (Application.isEditor && skipIntroInEditor)
		{
			IntroCompleted();
		}
		else
		{
			intro.Play(IntroCompleted);
		}
	}




	void IntroCompleted()
	{
		help.DelayedShow();

		SwitchPhase(GamePhase.Idle);
	}



	void SwitchPhase(GamePhase newPhase)
	{
		if (newPhase == Phase) return;
		
		switch(newPhase)
		{
			case GamePhase.Idle:
				break;

			case GamePhase.Movement:
				break;
		}
		
		Phase = newPhase;
	}
}