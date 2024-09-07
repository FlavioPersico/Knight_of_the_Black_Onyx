using UnityEngine;

namespace Assets.Scripts
{
    public abstract class Character : MonoBehaviour
    {
		[SerializeField] protected Rigidbody2D rb_character;
		[SerializeField] protected float _attackRate;
		[SerializeField] public AudioSource _audioSource;
		public SpriteRenderer Body;
		public Animator Animator;
		[SerializeField] protected AudioClip _attackClip;
		[SerializeField] protected AudioClip _blockClip;
		[SerializeField] protected AudioClip _deathClip;
		protected Health health;

		public Character()
		{

		}

		public Character(int healthParam)
		{
			this.health = new Health(healthParam);
		}
		protected virtual void Start()
		{
			health = new Health(5);
		}

		public abstract void Attack();
		public abstract void Die();
		public abstract void Move();
		public abstract void Block();
		public virtual void ReceiveDamage(int damage)
		{
			health.TakeDamage(damage);
		}
	}
}