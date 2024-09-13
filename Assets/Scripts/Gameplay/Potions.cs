using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Player_script;

public class Potions : MonoBehaviour
{
	[SerializeField]private float _spanwProbability;
	private string _typePotion;
	private void Start()
	{
		_typePotion = this.transform.name;
	}

	public float SpanwProbability()
	{
		return _spanwProbability;
	}

	public void UsePotion(Player player)
	{
		switch(_typePotion)
		{
			case "RedPotion":
				player.HealDamage(1);
				break;
			case "GreenPotion":
				player.GainLife();
				break;
			default:
				break;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.CompareTag("Player"))
		{
			UsePotion(collision.gameObject.GetComponent<Player>());
			Destroy(this.gameObject);
		}
	}
}
