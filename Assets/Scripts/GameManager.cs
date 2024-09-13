using Assets.Scripts.UI;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	[SerializeField] private GameObject _bossZone;
	[SerializeField] private GameObject _secretZone;
	[SerializeField] private Canvas _gameCanvas;
	[SerializeField] private AudioClip _rockFloorClip;
	[SerializeField] private AudioClip _grassFloorClip;
	[SerializeField] private AudioClip _bossClip;
	[SerializeField] private LootPool _lootPool;
	private string _typeFloor;
	private string lastTypeFloor;
	private float lastStepSound;
	private bool IsSameFloor;

	[SerializeField] private float stepSoundControl;

	public static GameManager _singleton;

	private void Awake()
	{
		_singleton = this;
		_lootPool = GetComponent<LootPool>();
	}

	public void SetTypeFloor(string typeFloor)
	{
		_typeFloor = typeFloor;
	}

	public void PlayFloorSound()
	{
		if(Time.time > lastStepSound + stepSoundControl || !IsSameFloor)
		{
			switch (_typeFloor)
			{
				case "GrassFloor":
					SoundControl.audioMain.PlayOneShot(_grassFloorClip, 50f);
					lastStepSound = Time.time;
					lastTypeFloor = _typeFloor;
					IsSameFloor = (lastTypeFloor == _typeFloor);
					break;
				case "RockFloor":
					SoundControl.audioMain.PlayOneShot(_rockFloorClip, 50f);
					lastStepSound = Time.time;
					lastTypeFloor = _typeFloor;
					IsSameFloor = (lastTypeFloor == _typeFloor);
					break;
				default:
					break;
			}
		}
	}

	public void SecretZoneActivate()
	{
		_secretZone.SetActive(true);
	}

	public void BossZoneActivate()
	{
		_bossZone.SetActive(true);
		_gameCanvas.worldCamera = _bossZone.GetComponent<Camera>();
		SoundControl.audioMain.clip = _bossClip;
		SoundControl.audioMain.Play();
	}

	public void GetCoins(Collider2D coins)
	{
		int coinValue = coins.GetComponent<Coins>().GetCoinsScore();
		UIManager._singleton.UpdateScore(coinValue);
		Destroy(coins.gameObject);
	}

	public Potions ReturnLoot()
	{
		int totalPoolItemsIndex = (_lootPool.TotalPoolItems()-1);
		Potions randomLootPrefab = _lootPool.GetPoolItem(Random.Range(0, totalPoolItemsIndex));
		float randomSpawnProbability = Random.Range(0.0f, 1.0f);

		float spanwProbability = randomLootPrefab.SpanwProbability();

		Debug.Log(spanwProbability);

		if (spanwProbability >= randomSpawnProbability)
		{
			return randomLootPrefab;
		}
		return null;
	}
}
