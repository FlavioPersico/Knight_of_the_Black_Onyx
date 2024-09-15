using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class UIManager : MonoBehaviour
    {
		[SerializeField] private UIBossLifeController _bossLife;
		[SerializeField] private UIPlayerController _playerUIController;
		[SerializeField] private TextMeshProUGUI _textScore;
		[SerializeField] private TextMeshProUGUI _textHiScore;

		public static UIManager _singleton;

		private void Awake()
		{
			_singleton = this;
		}

		public void UpdatePlayerUI(string lifeOrHeart, bool lost, int paramValue)
		{
			if(lifeOrHeart.ToUpper() == "LIFE")
			{
				UpdatePlayerLife(lost, paramValue);
			}
			else
			{
				UpdatePlayerHeart(lost, paramValue);
			}
		}

		public void UpdatePlayerHeart(bool damage, int currentHealth) 
		{
			if(damage)
			{
				_playerUIController.RemoveHeart(currentHealth);
			}
			else
			{
				_playerUIController.AddHeart(currentHealth);
			}
		}

		public void UpdatePlayerLife(bool lost, int currentLife)
		{
			if (lost)
			{
				_playerUIController.RemoveLife(currentLife);
			}
			else
			{
				_playerUIController.AddLife(currentLife);
			}
		}

		public void UpdateBossLife(bool newLife, int bossLife)
		{
			if(newLife)
			{
				AddBossHeart(bossLife);
			}
			else
			{
				RemoveBossHeart(bossLife);
			}
		}

		public void UpdateScore(int valueScore, string scoreFormat)
		{
			_textScore.text = valueScore.ToString(scoreFormat);
		}

		public void UpdateHiScore(int valueHiScore, string scoreFormat)
		{
			_textHiScore.text = valueHiScore.ToString(scoreFormat);
		}

		public void AddBossName(string bossName)
		{
			_bossLife.UpdateBossName(bossName);
		}

		public void AddBossHeart(int addLife)
        {
			for (int i = 0; i < addLife; i++)
			{
				_bossLife.AddHeart();
			}
		}

		public void RemoveBossHeart(int removeLife)
		{
			_bossLife.RemoveHeart();
		}
	}
}
