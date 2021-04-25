using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Dashboard : MonoBehaviour
{
	public GameObject firstCoin;
	public float nextCoinOffset;

	[Space]
	public SpriteRenderer launcherArmedVisual;
	public Sprite launcherArmedSprite;
	public Sprite launcherPendingSprite;

	[Header("Events")]
	public EventSO coinCollected;
	public EventSO launcherArmed;
	public EventSO launcherReleasedHarpoon;

	public int CollectedCoins { get; private set; } = 0;



	private void Awake()
	{
		firstCoin.gameObject.SetActive(false);
		launcherArmedVisual.enabled = false;
	}
	private void OnEnable()
	{
		coinCollected.RegisterAction(OnCoinCollected);
		launcherArmed.RegisterAction(OnLauncherArmed);
		launcherReleasedHarpoon.RegisterAction(OnLauncherReleasedHarpoon);
	}
	private void OnDisable()
	{
		coinCollected.UnregisterAction(OnCoinCollected);
		launcherArmed.UnregisterAction(OnLauncherArmed);
		launcherReleasedHarpoon.UnregisterAction(OnLauncherReleasedHarpoon);
	}



	private void OnCoinCollected()
	{
		CollectedCoins++;

		if (CollectedCoins == 1)
		{
			firstCoin.gameObject.SetActive(true);
		}
		else
		{
			var offset = nextCoinOffset * (CollectedCoins - 1);
			
			GameObject.Instantiate(
				firstCoin,
				firstCoin.transform.position + Vector3.right * offset,
				Quaternion.identity,
				this.transform);
		}
	}

	private void OnLauncherArmed()
	{
		launcherArmedVisual.enabled = true;
		launcherArmedVisual.sprite = launcherArmedSprite;
	}
	private void OnLauncherReleasedHarpoon()
	{
		launcherArmedVisual.sprite = launcherPendingSprite;
	}
}