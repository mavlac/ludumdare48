using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using MavLib.CommonSound;

public class Help : MonoBehaviour
{
	public GameObject controls;

	[Header("Events")]
	public EventSO jumpFromBoat;

	[Header("Audio")]
	public AudioClip helpPopInClip;



	private void Awake()
	{
		controls.SetActive(false);
	}
	private void OnEnable()
	{
		jumpFromBoat.RegisterAction(OnJumpedFromBoat);
	}
	private void OnDisable()
	{
		jumpFromBoat.UnregisterAction(OnJumpedFromBoat);
	}



	void OnJumpedFromBoat()
	{
		Dismiss();
	}



	public void DelayedShow()
	{
		StartCoroutine(ShowCoroutine());
	}
	IEnumerator ShowCoroutine()
	{
		yield return new WaitForSeconds(1f);

		CommonSound.PlayFX(helpPopInClip);
		controls.SetActive(true);
		controls.GetComponent<SpriteSwitch>().SetFrame(1);
	}
	public void Dismiss()
	{
		controls.SetActive(false);
	}
}