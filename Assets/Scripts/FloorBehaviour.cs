using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorBehaviour : MonoBehaviour
{
	private string _typeFloor;

	private void Start()
	{
		_typeFloor = this.gameObject.tag;
		GameManager._singleton.SetTypeFloor(_typeFloor);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.CompareTag("Player"))
		{
			_typeFloor = "GrassFloor";
			GameManager._singleton.SetTypeFloor(_typeFloor);
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			_typeFloor = this.gameObject.tag;
			GameManager._singleton.SetTypeFloor(_typeFloor);
		}
	}
}
