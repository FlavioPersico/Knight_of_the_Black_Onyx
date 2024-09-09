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
	private string _typeFloor;
	private string lastTypeFloor;
	private float lastStepSound;
	private bool IsSameFloor;

	[SerializeField] private float stepSoundControl;

	public static GameManager _singleton;

	private void Awake()
	{
		_singleton = this;
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
}
