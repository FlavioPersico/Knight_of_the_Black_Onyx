using Assets.Scripts.Player_script;
using DG.Tweening.Core.Easing;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using Assets.Scripts.Effects;
using Assets.Scripts.UI;
using Assets.Scripts.Enemies.StateMachine;

namespace Assets.Scripts.Enemies
{
    /// <summary>
    /// The main monster script.
    /// </summary>
    public class Enemy : Character
    {
		[SerializeField] protected float _attackDistance;
		private EnemyController2D _controller;
		private EnemyAnimation _animation;

		[SerializeField] private GameObject _attackPoint;
		[SerializeField] private GameObject _defensePoint;
		[SerializeField] public float radius;
		[SerializeField] public LayerMask _playerMask;
		[SerializeField] protected int enemyHealth;
		[SerializeField] private int enemyPoints;
		private float lastAttack;

		protected override void Start()
		{
			_controller = GetComponent<EnemyController2D>();
			_animation = GetComponent<EnemyAnimation>();
			_audioSource = GetComponent<AudioSource>();
			SetUpEnemy(enemyHealth);
		}

		public virtual void SetUpEnemy(int healthParam)
		{
			health = new Health(healthParam);
			health.OnHealthChange.AddListener(ChangeHealth);
		}

		public virtual void ChangeHealth(int health)
		{
			Debug.Log($"Health Changed! {health}");
			if (health <= 0)
			{
				//	GameManager.singleton.scoreManager.IncreaseScore();
				Die();
			}
		}
		public override void Move()
		{

		}

		public virtual void Move(Vector2 direction, Player target)
		{
			if (Vector2.Distance(transform.position, target.transform.position) > _attackDistance)
			{
				_controller.Input = Vector2.zero;
				_controller.Input.x = direction.x;
				/*if (direction.y >= 3) //To follow on platforms
				{
					Debug.Log(direction.y);
					_controller.Input.y = direction.y;
				}*/
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
		public override void Attack()
		{
			rb_character.velocity = Vector2.zero;
			_animation.Attack();
		}
		public override void Block()
		{
			_animation.Block();
			startBlocking();
		}

		public override void ReceiveDamage(int damage)
		{
			if(_animation.GetState() != EnemyState.Block)
			{
				base.ReceiveDamage(damage);
				_animation.Hit();
			}
		}

		public override void Die()
		{
			//GenerateRandomLoot();
			_animation.Die();
			_audioSource.PlayOneShot(_deathClip, 1f);
			EffectManager.Instance.Blink(this);
			UIManager._singleton.UpdateScore(enemyPoints);
			EffectManager.Instance.Blink(this);
			Destroy(gameObject,1f);
		}

		public void endAttacking()
		{
			Collider2D player = Physics2D.OverlapCircle(_attackPoint.transform.position, radius, _playerMask);

			if(player != null)
			{
				Debug.Log("Hit Player");
				_audioSource.PlayOneShot(_attackClip, 1f);
				player.GetComponent<Player>().ReceiveDamage(1);
			}
		}

		private void startBlocking()
		{
			Collider2D[] player = Physics2D.OverlapCircleAll(_defensePoint.transform.position, radius, _playerMask);
			foreach (Collider2D playerGameObject in player)
			{
				if (playerGameObject.CompareTag("Player"))
				{
					Debug.Log("Blocked attack");
					Rigidbody2D pushedPlayer = playerGameObject.GetComponent<Rigidbody2D>();
					var directionPush = pushedPlayer.transform.position - transform.position;
					pushedPlayer.AddForce(directionPush.normalized * 500, ForceMode2D.Force);
					_audioSource.PlayOneShot(_blockClip, 1f);
				}
			}
		}

		private void GenerateRandomLoot()
		{
			/*GameObject randomLootPrefab = pickUpPrefab[Random.Range(0, pickUpPrefab.Length)];
			float randomSpawnProbability = Random.Range(0.0f, 1.0f);

			float spanwProbability = randomLootPrefab.GetComponent<PickUp>().SpawnProbability();

			Debug.Log(spanwProbability);
			if (spanwProbability >= randomSpawnProbability)
			{
				Instantiate(randomLootPrefab, transform.position, Quaternion.identity);
			}*/
		}
		private void OnDrawGizmos()
		{
			//Gizmos.DrawWireSphere(_attackPoint.transform.position, radius);
			//Gizmos.DrawWireSphere(_defensePoint.transform.position, radius);
		}
	}
}