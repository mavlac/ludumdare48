using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using TMPro;

public class Credits : MonoBehaviour
{
	public TMP_Text title;
	public Color fadeColor;

	[Space]
	public TMP_Text subtitle;

	[Header("Events")]
	public EventSO jumpFromBoat;


	public bool IsDismissed { get; private set; } = false;



	private void Start()
	{
		Show();
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
		if (!IsDismissed) Dismiss();
	}



	public void Show()
	{
		StartCoroutine(ShowCoroutine());
	}
	IEnumerator ShowCoroutine()
	{
		title.overrideColorTags = true;
		
		yield return new WaitForSeconds(0.15f);

		title.overrideColorTags = false;
		yield return new WaitForSeconds(1.75f);
		title.overrideColorTags = true;
	}


	public void Dismiss()
	{
		IsDismissed = true;
		
		StartCoroutine(HideCoroutine());
	}
	IEnumerator HideCoroutine()
	{
		yield return new WaitForSeconds(0.05f);

		title.color = fadeColor;
		subtitle.gameObject.SetActive(false);

		yield return new WaitForSeconds(0.05f);

		title.gameObject.SetActive(false);
	}
}