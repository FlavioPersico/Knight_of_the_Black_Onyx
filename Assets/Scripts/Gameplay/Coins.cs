using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
	[SerializeField] private int _coinValue;

	public int GetCoinsScore()
	{
		return _coinValue;
	}
}
