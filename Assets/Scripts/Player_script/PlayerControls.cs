using System;
using UnityEngine;

namespace Assets.Scripts.Player_script
{
    [RequireComponent(typeof(Player))]
    public class PlayerControls : MonoBehaviour
    {
        private Player _player;

		public void Start()
        {
            _player = GetComponent<Player>();
        }

        public void Update()
        {
			//Climb();
            Move();
            Attack();
			Block();

			if (Input.GetKeyDown(KeyCode.LeftShift)) _player.Roll();
			//if (Input.GetKeyDown(KeyCode.C)) _animation.Climb();
			//if (Input.GetKeyUp(KeyCode.L)) EffectManager.Instance.Blink(_player);
		}

		/*private void Climb()
		{
			_player.Climb();
		}*/

		private void Move()
        {
            _player.Move();
        }

        private void Attack()
        {
			_player.Attack();
		}

		private void Block()
		{
			_player.Block();
		}

		/*private float _fireTime;

		 public void Fire(bool power = false)
		 {
			 if (Time.time - _fireTime < 0.15f) return;

			 if (_animation.GetState() == CharacterState.Idle)
			 {
				 _animation.Ready();
			 }

			 _fireTime = Time.time;

			 if (_player.Firearm.Detached)
			 {
				 _player.Firearm.Transform.gameObject.SetActive(true);
				 _player.Firearm.Animator.SetTrigger(power ? "PowerFire" : "Fire");
			 }
			 else
			 {
				 _player.Animator.SetTrigger("Fire");
			 }

			 _player.AudioSource.pitch = Random.Range(0.9f, 1.1f);
			 _player.AudioSource.PlayOneShot(EffectManager.Instance.FireAudioClip);
			 EffectManager.Instance.CreateSpriteEffect(_player, power ? "FireMuzzleM" : "FireMuzzleS", direction: 1, parent: _player.Firearm.FireMuzzle);
		 }*/
	}
}