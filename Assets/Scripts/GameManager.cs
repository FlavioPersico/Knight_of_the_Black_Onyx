using Assets.Scripts.UI;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class GameManager : MonoBehaviour
{
	[SerializeField] private GameObject _bossZone;
	[SerializeField] private GameObject _secretZone;
	[SerializeField] private Canvas _gameCanvas;
	[SerializeField] private AudioClip _rockFloorClip;
	[SerializeField] private AudioClip _grassFloorClip;
	[SerializeField] private AudioClip _bossClip;
	[SerializeField] private LootPool _lootPool;
	[SerializeField] private TextMeshProUGUI _textScoreGameOver;
	[SerializeField] private TextMeshProUGUI _textHiScoreGameOver;
	[SerializeField] private GameObject _recordGameOver;
	[SerializeField] private TextMeshProUGUI _taleText;
	private string _typeFloor;
	private string lastTypeFloor;
	private float lastStepSound;
	private bool IsSameFloor;
	private int _score = 0;
	private int _hiScore = 0;
	private string scoreFormat = "0000";
	private bool record;

	[SerializeField] private float stepSoundControl;

	public static GameManager _singleton;

	private void Awake()
	{
		_singleton = this;
		record = false;
		_lootPool = GetComponent<LootPool>();
		DontDestroyOnLoad(this.gameObject);
	}

	private void Start()
	{
		UpdateHiScore();
	}

	public void SetTypeFloor(string typeFloor)
	{
		_typeFloor = typeFloor;
	}

	public void UpdateScore(int valueScore)
	{
		_score += valueScore;
		PlayerPrefs.SetInt("Score",_score);
		UIManager._singleton.UpdateScore(_score, scoreFormat);
	}

	public void UpdateHiScore()
	{
		int hiScore = PlayerPrefs.GetInt("HiScore");
		_hiScore = hiScore;
		if (_score > hiScore)
		{
			PlayerPrefs.SetInt("HiScore", _score);
			_hiScore = _score;
			record = true;
		}
		UIManager._singleton.UpdateHiScore(_hiScore, scoreFormat);
	}

	public void GameOver()
	{
		_score = PlayerPrefs.GetInt("Score");
		_hiScore = PlayerPrefs.GetInt("HiScore");
		_textScoreGameOver.text = _score.ToString(scoreFormat);
		_textHiScoreGameOver.text = _hiScore.ToString(scoreFormat);
		if (record) _recordGameOver.SetActive(true);
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
		UpdateScore(coinValue);
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

	public void SetTalePart(string _talePart)
	{
		PlayerPrefs.SetString("Tale", _talePart);
	}

	public void UpdateTaleScreen()
	{
		string talePart = PlayerPrefs.GetString("Tale");
		switch (talePart)
		{
			case "Intro":
				_taleText.text = "Year XXXX\r\n\r\nThe Kingdom of Laruria was peaceful and beautiful for centuries it grows. However nothing is eternal and wars started to spread around the Kingdom. " +
					"All these wars started to put in check all peace contructed all these centuries. The responsible for this threat was the Dark Lord Monster that was in a crusade to destroy all other god's belivers  " +
					"that wa not Sigmon. The only way to defeat the Holy Kingdom was a legend. A old tale forgotten by many and know by none. The Black Onyx Knights! \r\n\r\nBeliving in this legend the squire Lino begun his " +
					"adventure to collect all Black Onyx lendary armor and weapon. So he could confront the Dark Lord Monster!";
				break;
			default:
				break;
		}
	}
}
