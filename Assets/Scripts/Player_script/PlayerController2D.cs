using System.Linq;
using UnityEngine;
using Assets.Scripts.Player_script.StateMachine;


namespace Assets.Scripts.Player_script
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(PlayerAnimation))]
    public class PlayerController2D : MonoBehaviour
    {
		 public Vector2 Input;
		 public bool IsGrounded;
		 public bool IsLadder;

		 public float Acceleration;
		 public float MaxSpeed;
		 public float JumpForce;
		 public float Gravity;

		 private Collider2D _collider;
		 private Rigidbody2D _rigidbody;
		 private PlayerAnimation _animation;
		 private Player _player;

		 private bool _jump;
		 private bool _crouch;
		 private bool _climb;

		 public void Start()
		 {
			 _collider = GetComponent<Collider2D>();
			 _rigidbody = GetComponent<Rigidbody2D>();
			_animation = GetComponent<PlayerAnimation>();
			_player = GetComponent<Player>();
		 }

		 public void FixedUpdate()
		 {
			var state = _animation.GetState();

			if (state == PlayerState.Die || state == PlayerState.Block /*|| state == PlayerState.Climb*/) return;

			if(state == PlayerState.Climb)
			{
				_rigidbody.gravityScale = 0f;
				_rigidbody.velocity = new Vector2(_rigidbody.velocity.x, Input.y * MaxSpeed);
				_climb = true;
			}
			 else
			{
				_rigidbody.gravityScale = 1f;
				_climb = false;
			}

			var velocity = _rigidbody.velocity;

			 if (Input.x == 0)
			 {
				 if (IsGrounded)
				 {
					 velocity.x = Mathf.MoveTowards(velocity.x, 0, Acceleration * 3 * Time.fixedDeltaTime);
				 }
			 }
			 else
			 {
				 var maxSpeed = MaxSpeed;
				 var acceleration = Acceleration;

				 if (_jump)
				 {
					 acceleration /= 2;
				 }
				 else if (_crouch)
				 {
					 acceleration /= 2;
					 maxSpeed /= 4;
				 }

				velocity.x = Mathf.MoveTowards(velocity.x, Input.x * maxSpeed, acceleration * Time.fixedDeltaTime);
				GameManager._singleton.PlayFloorSound();
				Turn(velocity.x);
			 }

			 if (IsGrounded)
			 {
				 _crouch = Input.y < 0;

				 if (!_jump && !_climb)
				 {
					 if (Input.x == 0)
					 {
						if (_crouch)
						{
							_animation.Crouch();
						}
						else 
						{
							 if (state != PlayerState.Idle)
							 {
								 _animation.Ready();
							 }
						 }
					 }
					 else
					 {
						 if (_crouch)
						 {
							 _animation.Crawl();
						 }
						 else
						{
							_animation.Run();
						}
					 }
				 }

				 if(IsLadder && Mathf.Abs(Input.y) > 0f)
				 {
					_player._audioSource.PlayOneShot(_player._climbClip, 5f);
					_animation.Climb();
				 }
				 else
				 {
					if (Input.y > 0 && !_jump)
					{
						_jump = true;
						_rigidbody.AddForce(Vector2.up * JumpForce);
						_animation.Jump();
						_player._audioSource.PlayOneShot(_player._jumpClip, 5f);
					}
				 }
			 }
			 else
			 {
				 velocity.y -= Gravity * Time.fixedDeltaTime;

				 if (velocity.y < 0)
				 {
					 _jump = true;
					 _animation.Fall();
				}
			 }

			 _rigidbody.velocity = velocity;
		 }

		 private void Turn(float direction)
		 {
			 var scale = transform.localScale;

			 scale.x = Mathf.Sign(direction) * Mathf.Abs(scale.x);

			 transform.localScale = scale;
		 }

		 private Collider2D _ground;

		 public void OnCollisionEnter2D(Collision2D collision)
		 {
			ContactPoint2D[] contactPoints = new ContactPoint2D[collision.contactCount];
			collision.GetContacts(contactPoints);
			bool allPointsContacts = contactPoints.All(i => i.point.y <= _collider.bounds.min.y);

			if (allPointsContacts) //collision.gameObject.layer == LayerMask.NameToLayer("Ground")
			{
				 IsGrounded = true;
				_player._audioSource.PlayOneShot(_player._landClip, 5f);
				_ground = collision.collider;

				 if (_jump)
				 {
					 _jump = false;
					 _animation.Land(Input.y < 0 ? PlayerState.Crouch : PlayerState.Land);
				 }
			 }
		 }

		 public void OnCollisionExit2D(Collision2D collision)
		 {
			 if (IsGrounded && collision.collider == _ground)
			 {
				 IsGrounded = false;
			 }
		 }

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.gameObject.CompareTag("Ladder"))
			{
				IsLadder = true;
			}
		}

		private void OnTriggerExit2D(Collider2D collision)
		{
			if (collision.gameObject.CompareTag("Ladder"))
			{
				IsLadder = false;
				_animation.Idle();
			}
		}
	}
}