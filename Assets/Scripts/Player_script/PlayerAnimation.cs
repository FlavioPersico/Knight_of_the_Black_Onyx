using System;
using UnityEngine;
using Assets.Scripts.Player_script.StateMachine;
using Assets.Scripts.Effects;

namespace Assets.Scripts.Player_script
{
    public class PlayerAnimation : MonoBehaviour
    {
		 private Player _character;

		 public void Start()
		 {
			 _character = GetComponent<Player>();
			 Idle();
		 }

		 public void Idle()
		 {
			 SetState(PlayerState.Idle);
		 }

		 public void Ready()
		 {
			 if (GetState() == PlayerState.Run)
			 {
				 EffectManager.Instance.CreateSpriteEffect(_character, "Brake");
			 }

			 SetState(PlayerState.Ready);
		 }

		 public void Run()
		 {
			 if (GetState() != PlayerState.Run)
			 {
				 EffectManager.Instance.CreateSpriteEffect(_character, "Run");
			 }

			 SetState(PlayerState.Run);
		 }

		 public void Jump()
		 {
			 SetState(PlayerState.Jump);
			 EffectManager.Instance.CreateSpriteEffect(_character, "Jump");
		 }

		 public void Fall()
		 {
			 SetState(PlayerState.Fall);
		 }

		 public void Land(PlayerState state = PlayerState.Land)
		 {
			 SetState(state);
			 EffectManager.Instance.CreateSpriteEffect(_character, "Land");
		 }

		 public void Block()
		 {
			 SetState(PlayerState.Block);
		 }

		 public void Climb()
		 {
			 SetState(PlayerState.Climb);
		 }

		 public void Die()
		 {
			 SetState(PlayerState.Die);
		 }

		 public void Roll()
		 {
			 _character.Animator.SetTrigger("Roll");
			 EffectManager.Instance.CreateSpriteEffect(_character, "Dash");
		 }

		 public void Slash()
		 {
			 _character.Animator.SetTrigger("Slash");
		 }

		 public void Jab()
		 {
			 _character.Animator.SetTrigger("Jab");
		 }

		 public void Push()
		 {
			 _character.Animator.SetTrigger("Push");
		 }

		 public void Shot()
		 {
			 _character.Animator.SetTrigger("Shot");
		 }

		 public void Hit()
		 {
			 _character.Animator.SetTrigger("Hit");
		 }

		 public void Crawl()
		 {
			 SetState(PlayerState.Crawl);
		 }

		 public void Crouch()
		 {
			 SetState(PlayerState.Crouch);
		 }

		 public void SetState(PlayerState state)
		 {
			 foreach (var variable in new[] { "Idle", "Ready", "Walk", "Run", "Crouch", "Crawl", "Jump", "Fall", "Land", "Block", "Climb", "Die" })
			 {
				 _character.Animator.SetBool(variable, false);
			 }

			 switch (state)
			 {
				 case PlayerState.Idle: _character.Animator.SetBool("Idle", true); break;
				 case PlayerState.Ready: _character.Animator.SetBool("Ready", true); break;
				 case PlayerState.Walk: _character.Animator.SetBool("Walk", true); break;
				 case PlayerState.Run: _character.Animator.SetBool("Run", true); break;
				 case PlayerState.Crouch: _character.Animator.SetBool("Crouch", true); break;
				 case PlayerState.Crawl: _character.Animator.SetBool("Crawl", true); break;
				 case PlayerState.Jump: _character.Animator.SetBool("Jump", true); break;
				 case PlayerState.Fall: _character.Animator.SetBool("Fall", true); break;
				 case PlayerState.Land: _character.Animator.SetBool("Land", true); break;
				 case PlayerState.Block: _character.Animator.SetBool("Block", true); break;
				 case PlayerState.Climb: _character.Animator.SetBool("Climb", true); break;
				 case PlayerState.Die: _character.Animator.SetBool("Die", true); break;
				 default: throw new NotSupportedException(state.ToString());
			 }

			 //Debug.Log("SetState: " + state);
		 }

		 public PlayerState GetState()
		 {
			 if (_character.Animator.GetBool("Idle")) return PlayerState.Idle;
			 if (_character.Animator.GetBool("Ready")) return PlayerState.Ready;
			 if (_character.Animator.GetBool("Walk")) return PlayerState.Walk;
			 if (_character.Animator.GetBool("Run")) return PlayerState.Run;
			 if (_character.Animator.GetBool("Crawl")) return PlayerState.Crawl;
			 if (_character.Animator.GetBool("Crouch")) return PlayerState.Crouch;
			 if (_character.Animator.GetBool("Jump")) return PlayerState.Jump;
			 if (_character.Animator.GetBool("Fall")) return PlayerState.Fall;
			 if (_character.Animator.GetBool("Land")) return PlayerState.Land;
			 if (_character.Animator.GetBool("Block")) return PlayerState.Block;
			 if (_character.Animator.GetBool("Climb")) return PlayerState.Climb;
			 if (_character.Animator.GetBool("Die")) return PlayerState.Die;

			 return PlayerState.Ready;
		 }

		 public void SetBool(string paramName)
		 {
			 _character.Animator.SetBool(paramName, true);
		 }

		 public void UnsetBool(string paramName)
		 {
			 _character.Animator.SetBool(paramName, false);
		 }
	}
}