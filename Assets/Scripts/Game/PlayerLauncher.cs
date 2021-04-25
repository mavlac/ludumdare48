using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MavLib.CommonSound;
using UnityEngine.Assertions;

public class PlayerLauncher : MonoBehaviour
{
	[Space]
	public SpriteRenderer armedVisual;

	[Space]
	public Harpoon harpoon;

	[Header("Events")]
	public EventSO launcherArmed;
	public EventSO launcherReleasedHarpoon;

	[Header("Sounds")]
	public AudioClip launcherArmedClip;
	public AudioClip harpoonLaunchedClip;


	public bool Armed { get; private set; } = false;


	private void Awake()
	{
		armedVisual.enabled = false;
	}
	private void Start()
	{
		harpoon.gameObject.SetActive(false);
	}



	public void Arm()
	{
		Armed = true;
		
		launcherArmed.Raise();

		this.Invoke(DelayedArmIndication, 0.15f);
	}
	void DelayedArmIndication()
	{
		armedVisual.enabled = true;
		
		CommonSound.PlayFX(launcherArmedClip);
	}



	public IEnumerator ShotCoroutine(Vector2 destination)
	{
		Armed = false;
		armedVisual.enabled = false;

		harpoon.transform.position = this.transform.position;
		harpoon.gameObject.SetActive(true);
		yield return StartCoroutine(harpoon.ShotCoroutine(destination));
		
		CommonSound.PlayFX(harpoonLaunchedClip);
		
		launcherReleasedHarpoon.Raise();
	}

}