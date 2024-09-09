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
	[SerializeField] private bool IsIdle;

	private Enemy _enemy;
	private bool InPatrol;
	private Rigidbody2D rb_enemy;

	private Vector3 randomPosition;
	private Vector2 direction;

	private void Start()
	{
		_target = FindObjectOfType<Player>();
		_enemy = GetComponent<Enemy>();
		if(IsIdle)
		{
			rb_enemy = _enemy.GetComponent<Rigidbody2D>();
		}
		InPatrol = true;
	}

	private void FixedUpdate()
	{
		if(!InPatrol || _enemy.GetIsBoss())
		{
			if(IsIdle)
			{
				rb_enemy.constraints = RigidbodyConstraints2D.None;
			}
			direction = _target.transform.position - transform.position;
			_enemy.Move(direction.normalized, _target);
		}
		else
		{
			InPatrol = true;
			if(!IsIdle)
			{
				Roaming();
			}
			else
			{
				rb_enemy.constraints = RigidbodyConstraints2D.FreezeAll;
			}

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
}
