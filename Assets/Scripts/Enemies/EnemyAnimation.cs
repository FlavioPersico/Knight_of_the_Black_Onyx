using System;
using Assets.Scripts.Enemies.StateMachine;
using Assets.Scripts.Effects;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    [RequireComponent(typeof(Enemy))]
    public class EnemyAnimation : MonoBehaviour
    {
        private Enemy _enemy;

        public void Start()
        {
			_enemy = GetComponent<Enemy>();
        }

        public void SetState(EnemyState state)
        {
            foreach (var variable in new[] { "Idle", "Ready", "Walk", "Run", "Jump", "Die", "Block" })
            {
				_enemy.Animator.SetBool(variable, false);
            }

            switch (state)
            {
                case EnemyState.Idle: _enemy.Animator.SetBool("Idle", true); break;
                case EnemyState.Ready: _enemy.Animator.SetBool("Ready", true); break;
                case EnemyState.Walk: _enemy.Animator.SetBool("Walk", true); break;
                case EnemyState.Run: _enemy.Animator.SetBool("Run", true); break;
                case EnemyState.Jump: _enemy.Animator.SetBool("Jump", true); break;
                case EnemyState.Die: _enemy.Animator.SetBool("Die", true); break;
                case EnemyState.Block: _enemy.Animator.SetBool("Block", true); break;
                default: throw new NotSupportedException();
            }

            //Debug.Log("SetState: " + state);
        }

        public EnemyState GetState()
        {
            if (_enemy.Animator.GetBool("Idle")) return EnemyState.Idle;
            if (_enemy.Animator.GetBool("Ready")) return EnemyState.Ready;
            if (_enemy.Animator.GetBool("Walk")) return EnemyState.Walk;
            if (_enemy.Animator.GetBool("Run")) return EnemyState.Run;
            if (_enemy.Animator.GetBool("Jump")) return EnemyState.Jump;
            if (_enemy.Animator.GetBool("Die")) return EnemyState.Die;
			if (_enemy.Animator.GetBool("Block")) return EnemyState.Block;

			return EnemyState.Ready;
        }

        public void Idle()
        {
            SetState(EnemyState.Idle);
        }

        public void Ready()
        {
            if (GetState() == EnemyState.Walk)
            {
                EffectManager.Instance.CreateSpriteEffect(_enemy, "Brake");
            }
            else if (GetState() == EnemyState.Idle)
            {
                return;
            }

            SetState(EnemyState.Ready);
        }

        public void Run()
        {
            if (GetState() != EnemyState.Walk)
            {
                EffectManager.Instance.CreateSpriteEffect(_enemy, "Run");
            }

            SetState(EnemyState.Walk);
        }

        public void Jump()
        {
            EffectManager.Instance.CreateSpriteEffect(_enemy, "Jump");
            SetState(EnemyState.Run);
        }

        public void Fall()
        {
            SetState(EnemyState.Run);
        }

        public void Land()
        {
            EffectManager.Instance.CreateSpriteEffect(_enemy, "Fall");
        }
        
        public void Die()
        {
            SetState(EnemyState.Die);
        }
        
        public void Attack()
        {
            _enemy.Animator.SetTrigger("Attack");
        }

        public void Fire()
        {
			_enemy.Animator.SetTrigger("Fire");
        }

        public void Hit()
        {
			_enemy.Animator.SetTrigger("Hit");
        }
		public void Block()
		{
            SetState(EnemyState.Block);
		}

	}
}