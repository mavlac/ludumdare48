using System.Collections;
using System.Collections.Generic;
using MavLib.CommonSound;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
	public enum Message { None, GameOver, Victory }

	[Space]
	public GameObject canvas;

	[Space]
	public Image frame;

	[Space]
	public Image picture;
	public Sprite pictureGameOver;
	public Sprite pictureVictory;

	[Space]
	public Button dismissButton;
	public AudioClip buttonClickClip;

	[Header("Audio")]
	public AudioClip gameOverClip;
	public AudioClip victoryClip;



	private Message currentMessage = Message.None;
	private System.Action<Message> onDismissed;



	private void Awake()
	{
		canvas.gameObject.SetActive(false);

		frame.enabled = false;
		picture.enabled = false;

		dismissButton.gameObject.SetActive(false);
		dismissButton.enabled = false;
		dismissButton.interactable = false;
	}
	private void OnEnable()
	{
		dismissButton.onClick.AddListener(DismissMessage);
	}
	private void Update()
	{
		if (dismissButton.enabled && dismissButton.interactable &&
			(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
		{
			DismissMessage();
		}
	}



	public void ShowMessage(Message message, System.Action<Message> onDismissed)
	{
		this.onDismissed = onDismissed;
		
		canvas.gameObject.SetActive(true);
		
		switch (message)
		{
			case Message.GameOver:
				picture.sprite = pictureGameOver;
				CommonSound.PlayFX(gameOverClip);
				break;

			case Message.Victory:
				CommonSound.PlayFX(victoryClip);
				picture.sprite = pictureVictory;
				break;
		}
		
		StartCoroutine(ShowMessageCoroutine());
	}
	IEnumerator ShowMessageCoroutine()
	{
		yield return new WaitForSeconds(0.15f);
		
		frame.enabled = true;
		
		yield return new WaitForSeconds(0.15f);
		
		picture.enabled = true;
		
		yield return new WaitForSeconds(0.15f);

		dismissButton.gameObject.SetActive(true);
		dismissButton.enabled = true;
		dismissButton.interactable = true;
	}



	public void DismissMessage()
	{
		CommonSound.StopBothChannels();
		CommonSound.PlayFX(buttonClickClip);

		Debug.Log("UI message dismissed");
		
		dismissButton.interactable = false;
		
		StartCoroutine(DismissMessageCoroutine());
	}
	IEnumerator DismissMessageCoroutine()
	{
		dismissButton.gameObject.SetActive(false);
		
		yield return new WaitForSeconds(0.5f);
		
		picture.enabled = false;
		
		yield return new WaitForSeconds(0.15f);
		
		frame.enabled = false;
		
		onDismissed.Invoke(currentMessage);
	}
}