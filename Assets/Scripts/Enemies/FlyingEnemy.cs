using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Enemies;
using Assets.Scripts.Player_script;
using UnityEngine.InputSystem.XR;

public class FlyingEnemy : Enemy
{
	[SerializeField] private Transform startingPosition;
	[SerializeField] private Player target;

	public void Chase(Vector2 direction, Player target)
	{
		if (Vector2.Distance(transform.position, target.transform.position) > _attackDistance)
		{
			transform.position = Vector2.MoveTowards(transform.position, direction, _controller.Acceleration * 3 * Time.fixedDeltaTime);
			_controller.Input.x = direction.x;
		}
		else
		{
			_controller.Input = Vector2.zero;
			if (Time.time > lastAttack + _attackRate)
			{
				Attack();
				lastAttack = Time.time;
			}
		}
	}

	public void ReturnStartPosition(Player target)
	{
		base.Move(startingPosition.position, target);
	}
}
