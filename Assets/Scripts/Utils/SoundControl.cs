using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControl : MonoBehaviour
{
	private AudioSource audioSource;
	public static AudioSource audioMain;

	private void Awake()
	{
		audioSource = GetComponent<AudioSource>();
		audioMain = audioSource;
	}
}
