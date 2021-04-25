using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
	public const float GridSize = 0.16f;


	public enum GamePhase { Intro, Idle, Movement, UIEnding }


	public GameIntro intro;
	public bool skipIntroInEditor = false;

	[Space]
	public CameraRig cameraRig;
	public Transition transition;
	public Credits credits;
	public Help help;
	public UI ui;

	[Space]
	public Water water;
	public Player player;

	[Header("Events")]
	public EventSO preSceneRestart;
	public EventSO killedByOctopus;
	public EventSO treasureReached;
	
	
	public GamePhase Phase { get; private set; } = GamePhase.Intro;



	private void Start()
	{
		transition.FadeIn();
		
		if (Application.isEditor && skipIntroInEditor)
		{
			IntroCompleted();
		}
		else
		{
			intro.Play(IntroCompleted);
		}
	}
	private void OnEnable()
	{
		killedByOctopus.RegisterAction(OnKilledByOctopus);
		treasureReached.RegisterAction(OnTreasureReached);
	}
	private void OnDisable()
	{
		killedByOctopus.UnregisterAction(OnKilledByOctopus);
		treasureReached.UnregisterAction(OnTreasureReached);
	}



	private void OnKilledByOctopus()
	{
		UpdatePhase(GamePhase.UIEnding);
		
		GameOver();
	}
	private void OnTreasureReached()
	{
		UpdatePhase(GamePhase.UIEnding);
		
		Victory();
	}




	public void IntroCompleted()
	{
		help.DelayedShow();

		UpdatePhase(GamePhase.Idle);
	}
	public void LevelEntered(Level level)
	{
		player.EnteredLevel(level);
		
		if (level.splashWaterOnEnter)
		{
			water.Splash();
		}

		if (level.anchorCameraOnEnter)
		{
			cameraRig.FocusOnLevel(level);
		}
	}
	public void GameOver()
	{
		transition.FadeOut();
		ui.ShowMessage(UI.Message.GameOver, GameOverOrVictoryDismissed);
	}
	public void Victory()
	{
		transition.FadeOut();
		ui.ShowMessage(UI.Message.Victory, GameOverOrVictoryDismissed);
	}
	void GameOverOrVictoryDismissed(UI.Message dismissedMessage)
	{
		switch(dismissedMessage)
		{
			case UI.Message.GameOver:
				// TODO Restart Scene without Intro?
				break;
			
			case UI.Message.Victory:
				// TODO Restart Scene like completely new launch
				break;
		}
		
		preSceneRestart.Raise();
		
		this.Invoke(RestartScene, 1f);
	}
	void RestartScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}



	public void UpdatePhase(GamePhase newPhase)
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