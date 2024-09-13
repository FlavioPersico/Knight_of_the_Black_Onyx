using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootPool : MonoBehaviour
{
    [SerializeField] private List<Potions> _itemPotionPool;
	
	public int TotalPoolItems()
	{
		return _itemPotionPool.Count;
	}

	public Potions GetPoolItem(int index)
	{
		return _itemPotionPool[index];
	}
}
