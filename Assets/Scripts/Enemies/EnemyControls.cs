using Assets.Scripts.Player_script;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts.Enemies
{
    [RequireComponent(typeof(Enemy))]
    [RequireComponent(typeof(EnemyController2D))]
    [RequireComponent(typeof(EnemyAnimation))]
    public class EnemyControls : MonoBehaviour
	{
        private Enemy _enemy;
        private EnemyController2D _controller;
        private EnemyAnimation _animation;

		public void Start()
        {
            _enemy = GetComponent<Enemy>();
            _controller = GetComponent<EnemyController2D>();
            _animation = GetComponent<EnemyAnimation>();
        }

		public void Update()
        {
            //Move();
            //Attack();

            // Play other animations, just for example.
            //if (Input.GetKeyDown(KeyCode.I)) { _animation.SetState(EnemyState.Idle); }
            //if (Input.GetKeyDown(KeyCode.R)) { _animation.SetState(EnemyState.Ready); }
            //if (Input.GetKeyDown(KeyCode.D)) _animation.SetState(EnemyState.Die);
            //if (Input.GetKeyUp(KeyCode.H)) _animation.Hit();
            //if (Input.GetKeyUp(KeyCode.L)) EffectManager.Instance.Blink(_monster);
        }

		private void Move()
        {

            /*if (Input.GetKey(KeyCode.LeftArrow))
            {
                _controller.Input.x = -1;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                _controller.Input.x = 1;
            }

            if (Input.GetKey(KeyCode.UpArrow))
            {
                _controller.Input.y = 1;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                _controller.Input.y = -1;
            }*/
        }

        private void Attack()
        {
            //if (Input.GetKeyDown(KeyCode.A)) _animation.Attack();
            //if (Input.GetKeyDown(KeyCode.F)) _animation.Fire();
		}
	}
}