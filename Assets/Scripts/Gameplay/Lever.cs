using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField] private GameObject leverOpen;
	[SerializeField] private GameObject leverClose;
	[SerializeField] private GameObject doorControled;

	private void Awake()
	{
		leverOpen.SetActive(false);
		leverClose.SetActive(true);
		doorControled.GetComponent<Animator>().SetBool("Open", false);
	}

	public void LeverUpdate()
	{
		if(leverOpen.activeSelf)
		{
			leverOpen.SetActive(false);
			leverClose.SetActive(true);
			doorControled.GetComponent<Animator>().SetBool("Open", false);
		}
		else if(leverClose.activeSelf)
		{
			leverOpen.SetActive(true);
			leverClose.SetActive(false);
			doorControled.GetComponent<Animator>().SetBool("Open", true);
		}

	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.CompareTag("Player"))
		{
			LeverUpdate();
		}
	}
}
