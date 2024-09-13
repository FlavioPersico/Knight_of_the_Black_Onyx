using Assets.Scripts.Player_script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] private Transform teleportTarget;
	[SerializeField] private Player _player;
	[SerializeField] private AudioClip teleportAudio;

	private void Awake()
	{
		_player = FindObjectOfType<Player>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.CompareTag("Player"))
		{
			SoundControl.audioMain.PlayOneShot(teleportAudio);
			_player.transform.position = teleportTarget.position;
		}
	}

}
