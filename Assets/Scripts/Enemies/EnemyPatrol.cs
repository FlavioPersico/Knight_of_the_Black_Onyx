using Assets.Scripts.Enemies;
using Assets.Scripts.Enemies.StateMachine;
using Assets.Scripts.Player_script;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using static UnityEngine.GraphicsBuffer;
using Random = UnityEngine.Random;

public class EnemyPatrol : MonoBehaviour
{
	[SerializeField] private Player _target;
	[SerializeField] private bool IsBoss;
	private Enemy _enemy;
	private bool InPatrol;

	private Vector3 randomPosition;
	private Vector2 direction;

	private void Start()
	{
		_target = FindObjectOfType<Player>();
		_enemy = GetComponent<Enemy>();
		InPatrol = true;
	}

	private void FixedUpdate()
	{
		if(!InPatrol || IsBoss)
		{
			direction = _target.transform.position - transform.position;
			_enemy.Move(direction.normalized, _target);
		}
		else
		{
			InPatrol = true;
			Roaming();
		}
	}

	private void Roaming()
	{
		randomPosition = RandomPosition();
		direction = randomPosition - transform.position;
		_enemy.Move(direction, _target);
	}

	private Vector3 RandomPosition()
	{
		Vector3 position = Random.insideUnitCircle * 1.5f;
		position += new Vector3(transform.position.x, 0, 0);
		return position;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(_target != null)
		{
			if (collision.CompareTag("Player"))
			{
				InPatrol = false;
				direction = _target.transform.position - transform.position;
				_enemy.Move(direction, _target);
			}
		}
	}

	/*private void OnTriggerExit2D(Collider2D collision)
	{
		InPatrol = true;
	}*/
}
