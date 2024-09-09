using Assets.Scripts.Enemies;
using Assets.Scripts.Player_script.StateMachine;
using Assets.Scripts.UI;
using Assets.Scripts.Effects;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using static UnityEngine.GraphicsBuffer;
using System.Collections;
using System.Threading;
using TMPro.EditorUtilities;

namespace Assets.Scripts.Player_script
{
	[RequireComponent(typeof(PlayerController2D))]
	[RequireComponent(typeof(PlayerAnimation))]
	public class Player : Character
    {
		[SerializeField] public AudioClip _climbClip;
		[SerializeField] public AudioClip _landClip;
		[SerializeField] public AudioClip _jumpClip;
		[SerializeField] private GameObject _attackPoint;
		[SerializeField] private GameObject _defensePoint;
		[SerializeField] public float radius;
		[SerializeField] public LayerMask _enemyMask;
		[SerializeField] public LayerMask _destructableMask;

		private PlayerController2D _controller;
		private PlayerAnimation _animation;
		private int totalLife;
		private bool isImmortal;
		private int initialHealth = 5;

		private float lastAttack;

/*#if UNITY_EDITOR

		public void OnValidate()
				{
					if (Application.isPlaying && Time.time > 1)
					{
						//GetComponent<PlayerBuilder>().Rebuild();
					}
				}

		#endif*/

		protected override void Start()
		{
			totalLife = 5;
			health = new Health(initialHealth);
			isImmortal = false;

			UIManager._singleton.UpdatePlayerHeart(false, health.currentHealth);
			UIManager._singleton.UpdatePlayerLife(false, totalLife);


			health.OnHealthChange.AddListener(ChangeHealth);
			_controller = GetComponent<PlayerController2D>();
			_animation = GetComponent<PlayerAnimation>();
			_audioSource = GetComponent<AudioSource>();
		}

		//public Firearm Firearm;
		public override void Attack()
		{
			if (Time.time > lastAttack + _attackRate)
			{
				if (Input.GetKeyDown(KeyCode.Mouse2)) _animation.Jab();
				if (Input.GetKeyDown(KeyCode.Mouse0)) _animation.Slash();
				if (Input.GetKeyDown(KeyCode.E)) _animation.Push();
			}
		}

		public override void Block()
		{
			if (_animation.GetState() == PlayerState.Block)
			{
				if (Input.GetKeyUp(KeyCode.Mouse1))
				{
					_animation.Idle();
				}
			}
			else
			{
				if (Input.GetKey(KeyCode.Mouse1)) _animation.Block();
			}
		}

		public override void Die()
		{
			_animation.Die();
			totalLife -= 1;
			if(totalLife <= 0)
			{
				_audioSource.PlayOneShot(_deathClip, 1f);
				Destroy(gameObject, 3f);
			}
			else
			{
				StartCoroutine(SpawnPlayer());
				_animation.Idle();
			}
		}

		public override void Move()
		{
			_controller.Input = Vector2.zero;

			if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
			{
				_controller.Input.x = -1;
			}
			else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
			{
				_controller.Input.x = 1;
			}

			if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
			{
				_controller.Input.y = 1;
			}
			else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
			{
				_controller.Input.y = -1;
			}
		}

		public void Roll()
		{
			_animation.Roll();
			_audioSource.PlayOneShot(_landClip,1f);
		}

		public void ChangeHealth(int health)
		{
			Debug.Log($"Health Changed! {health}");
			UIManager._singleton.UpdatePlayerUI("Heart", true, health);
			if (health <= 0)
			{
				Die();
				UIManager._singleton.UpdatePlayerUI("Life", true, totalLife);
			}
		}

		public override void ReceiveDamage(int damage)
		{
			if (_animation.GetState() != PlayerState.Block && isImmortal == false && health.currentHealth > 0)
			{
				base.ReceiveDamage(damage);
				_animation.Hit();
			}
		}

		public void HealDamage(int heal)
		{
			health.RecoverHealth(heal);
			UIManager._singleton.UpdatePlayerUI("Heart",false, heal);
		}

		public void endAttacking()
		{
				Collider2D[] enemy = Physics2D.OverlapCircleAll(_attackPoint.transform.position, radius, _enemyMask);
				Collider2D[] destruct = Physics2D.OverlapCircleAll(_attackPoint.transform.position, radius, _destructableMask);

				_audioSource.PlayOneShot(_attackClip);
				foreach (Collider2D enemyGameObject in enemy)
				{
					Debug.Log("Hit enemy");
					enemyGameObject.GetComponent<Enemy>().ReceiveDamage(1);
				}

				foreach (Collider2D destructGameObject in destruct)
				{
					Destroy(destructGameObject.gameObject, 0.5f);
				}
				lastAttack = Time.time;
		}

		private void startBlocking()
		{
			Collider2D[] enemy = Physics2D.OverlapCircleAll(_defensePoint.transform.position, radius, _enemyMask);
			gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

			foreach (Collider2D enemyGameObject in enemy)
			{
				if (enemyGameObject.CompareTag("Enemy"))
				{
					_audioSource.PlayOneShot(_blockClip,1f);
					Debug.Log("Blocked attack");
					Rigidbody2D pushedEnemy = enemyGameObject.GetComponent<Rigidbody2D>();
					var directionPush = pushedEnemy.transform.position - transform.position;
					pushedEnemy.AddForce(directionPush.normalized * 1000, ForceMode2D.Force);
				}
			}
		}

		IEnumerator SpawnPlayer()
		{
			isImmortal = true;
			HealDamage(initialHealth);

			for(int i =0; i<=10;i++)
			{
				if(i < 10)
				{
					EffectManager.Instance.Blink(this);
					yield return new WaitForSeconds(0.5f);
				}
				else
				{
					isImmortal = false;
					yield return new WaitForSeconds(5.5f);
				}
			}
		}

		private void OnDrawGizmos()
		{
			Gizmos.DrawWireSphere(_attackPoint.transform.position, radius);
			//Gizmos.DrawWireSphere(_defensePoint.transform.position, radius);
		}

		private void OnCollisionEnter2D(Collision2D collision)
		{
			if (_animation.GetState() == PlayerState.Block)
			{
				startBlocking();
			}
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.gameObject.CompareTag("Collectable"))
			{
				Destroy(collision.gameObject);
			}

			if(collision.gameObject.CompareTag("BossZone"))
			{
				GameManager._singleton.BossZoneActivate();
			}

			if (collision.gameObject.CompareTag("SecretZone"))
			{
				GameManager._singleton.SecretZoneActivate();
			}
		}

		private void OnTriggerExit2D(Collider2D collision)
		{
			if (collision.gameObject.CompareTag("Pit"))
			{
				this.transform.position = collision.transform.position;
				base.ReceiveDamage(health.currentHealth);
			}
		}
	}
}