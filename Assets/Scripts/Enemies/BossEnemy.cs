using Assets.Scripts.Enemies;
using Assets.Scripts.Player_script;
using Assets.Scripts.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : Enemy
{
	[SerializeField] private string _bossName;
	// Start is called before the first frame update
	protected override void Start()
	{
		base.Start();
		BossLife();
	}

	public override void ChangeHealth(int health)
	{
		UIManager._singleton.UpdateBossLife(false, health);
		base.ChangeHealth(health);
	}

	private void BossLife()
	{
		UIManager._singleton.AddBossName(_bossName);
		UIManager._singleton.UpdateBossLife(true, enemyHealth);
	}
}
