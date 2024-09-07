using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	[SerializeField] private GameObject _bossZone;
	[SerializeField] private Canvas _gameCanvas;
	[SerializeField] private AudioClip _rockFloorClip;
	[SerializeField] private AudioClip _grassFloorClip;
	[SerializeField] private AudioClip _bossClip;

	public static GameManager _singleton;

	private void Awake()
	{
		_singleton = this;
	}

	public void PlayFloorSound(string typeFloor)
	{
		switch (typeFloor)
		{
			case "Grass":
				SoundControl.audioMain.PlayOneShot(_grassFloorClip);
				break;
			case "Rock":
				SoundControl.audioMain.PlayOneShot(_rockFloorClip);
				break;
			default:
				break;
		}
	}

	public void BossZoneActivate()
	{
		_bossZone.SetActive(true);
		_gameCanvas.worldCamera = _bossZone.GetComponent<Camera>();
		SoundControl.audioMain.clip = _bossClip;
		SoundControl.audioMain.Play();
	}
}
