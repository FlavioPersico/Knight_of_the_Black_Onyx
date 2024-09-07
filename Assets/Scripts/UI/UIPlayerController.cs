using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class UIPlayerController : MonoBehaviour
    {
		[SerializeField] private Image[] _playerHeart;
		[SerializeField] private TextMeshProUGUI _totalHeartsTxt;
		[SerializeField] private TextMeshProUGUI _totalLivesTxt;
		private int _totalHearts;
		private int _totalLives = 5;

		private void Start()
		{
			_totalHearts = _playerHeart.Length;
			_totalLivesTxt.text = $"X{_totalLives}"; 
		}
		
		public void AddHeart(int currentHealth)
        {
			if (_totalHearts <= 0) PlayerRespawn(currentHealth);

			for (int hearts = _totalHearts; hearts < currentHealth; hearts++)
			{
				if (HeartOverLimit(currentHealth))
				{
					_totalHearts += 1;
					_totalHeartsTxt.text = $"X{_totalHearts.ToString()}";
				}
				else
				{
					int currentIndex = hearts;
					if (!_playerHeart[currentIndex].IsActive())
					{
						_playerHeart[currentIndex].gameObject.SetActive(true);
						_totalHearts += 1;
					}
				}
			}
		}
		public void RemoveHeart(int currentHealth)
		{
			for(int hearts = _totalHearts; hearts > currentHealth; hearts--)
			{
				_totalHearts -= 1;
				if (HeartOverLimit(currentHealth))
				{
					_totalHeartsTxt.text = $"X{_totalHearts.ToString()}";
				}
				else 
				{
					int currentIndex = _totalHearts;
					if (currentHealth < _playerHeart.Length)
					{
						_playerHeart[currentIndex].gameObject.SetActive(false);
					}
				}
			}
		}
		public bool HeartOverLimit(int currentHealth)
		{
			int lastIndex = _playerHeart.Length - 1;
			int secdondIndex = 1;
			if (currentHealth > _playerHeart.Length)
			{
				_totalHeartsTxt.gameObject.SetActive(true);
				if (_playerHeart[secdondIndex].IsActive())
				{
					for (int i = (_playerHeart.Length - 1); i > 0; i--)
					{
						_playerHeart[i].gameObject.SetActive(false);
					}
				}
				return true;
			}
			else if(currentHealth == _playerHeart.Length)
			{
				_totalHeartsTxt.gameObject.SetActive(false);
				if(_playerHeart[lastIndex].IsActive() == false)
				{
					for (int i = 0; i < _playerHeart.Length; i++)
					{
						_playerHeart[i].gameObject.SetActive(true);
					}
				}
				return false;
			}
			else
			{
				return false;
			}
		}
		public void AddLife(int currentLife)
		{

			_totalLives = currentLife;
			_totalLivesTxt.text = $"X{_totalLives.ToString()}";
		}
		public void RemoveLife(int currentLife)
		{
			_totalLives = currentLife;
			_totalLivesTxt.text = $"X{_totalLives.ToString()}";
		}

		public void PlayerRespawn(int currentHealth)
		{
			_totalHearts = currentHealth;
			if (HeartOverLimit(currentHealth))
			{
				int firstIndex = 0;
				_totalHeartsTxt.text = $"X{_totalHearts.ToString()}";
				_playerHeart[firstIndex].gameObject.SetActive(true);
			}
		}
	}
}
