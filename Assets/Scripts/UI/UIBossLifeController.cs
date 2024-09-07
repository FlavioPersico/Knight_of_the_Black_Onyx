using Assets.Scripts.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
	public class UIBossLifeController : MonoBehaviour
	{
		public GameObject _heartPrefab;
		public TextMeshProUGUI _bossNameText;
		private Transform parentTransform;

		private void Awake()
		{
			parentTransform = this.gameObject.transform;
		}

		public void UpdateBossName(string bossName)
		{
			_bossNameText.text = $"{bossName}: ";
		}

		public void AddHeart()
		{
			Instantiate(_heartPrefab,parentTransform);
		}
		public void RemoveHeart()
		{
			if (parentTransform.childCount > 0)
			{
				int lastChildIndex = parentTransform.childCount - 1;
				Transform lastChildTransform = parentTransform.GetChild(lastChildIndex);
				if (lastChildTransform != null)
				{
					Destroy(lastChildTransform.gameObject);
				}
			}
		}
	}
}
