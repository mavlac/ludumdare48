﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Dashboard : MonoBehaviour
{
	public GameObject firstCoin;
	public float nextCoinOffset;

	[Header("Events")]
	public EventSO coinCollected;


	public int CollectedCoins { get; private set; } = 0;



	private void Awake()
	{
		firstCoin.gameObject.SetActive(false);
	}
	private void OnEnable()
	{
		coinCollected.RegisterAction(OnCoinCollected);
	}
	private void OnDisable()
	{
		coinCollected.UnregisterAction(OnCoinCollected);
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
}