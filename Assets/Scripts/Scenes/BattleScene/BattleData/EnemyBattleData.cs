using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBattleData {
	public void monsterAdd(IMonsterData addMonster) {
		if (haveMonsterSize_ == MONSTER_MAX_SIZE) return;

		monsterDatas_[haveMonsterSize_] = addMonster;
		haveMonsterSize_ += 1;
	}

	//get
	public IMonsterData GetMonsterDatas(int num) { return monsterDatas_[num]; }
	public int GetMonsterDatasLength() { return monsterDatas_.Length; }
	public int GetHaveMonsterSize() { return haveMonsterSize_; }

	//手持ちのモンスターのデータ
	private const int MONSTER_MAX_SIZE = 3;
	private int haveMonsterSize_ = 0;
	private IMonsterData[] monsterDatas_ = new IMonsterData[MONSTER_MAX_SIZE];

	//シングルトン
	private EnemyBattleData() { }

	static private EnemyBattleData instance_ = null;
	static public EnemyBattleData GetInstance() {
		if (instance_ != null) return instance_;

		instance_ = new EnemyBattleData();
		return instance_;
	}
}
