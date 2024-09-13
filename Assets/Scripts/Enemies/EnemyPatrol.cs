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
	[SerializeField] private bool IsFlying;
	[SerializeField] private Transform pointA;
	[SerializeField] private Transform pointB;

	private Enemy _enemy;
	private FlyingEnemy _flyEnemy;
	private bool InPatrol;

	private Vector3 randomPosition;
	private Vector2 direction;

	private void Start()
	{
		_target = FindObjectOfType<Player>();
		if(IsFlying)
		{
			_flyEnemy = GetComponent<FlyingEnemy>();
		}
		else
		{
			_enemy = GetComponent<Enemy>();
		}

		InPatrol = true;
	}

	private void FixedUpdate()
	{
		if(!InPatrol || _enemy.GetIsBoss())
		{
			direction = _target.transform.position - transform.position;
			if(IsFlying)
			{
				_flyEnemy.GetComponent<EnemyAnimation>().Run();
				_flyEnemy.Chase(_target.transform.position, _target);
			}
			else
			{
				_enemy.Move(direction.normalized, _target);
			}
		}
		else
		{
			InPatrol = true;
			if(!IsFlying)
			{
				Roaming();
			}
			else
			{
				_flyEnemy.GetComponent<EnemyAnimation>().Ready();
				_flyEnemy.ReturnStartPosition(_target);
			}

		}
	}

	private void Roaming()
	{
		randomPosition = RandomPosition();
		direction = randomPosition - transform.position;

		if(Vector2.Distance(direction, pointA.position) < 0.5f) direction = pointB.position - transform.position;
		if(Vector2.Distance(direction, pointB.position) < 0.5f) direction = pointA.position - transform.position;

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
