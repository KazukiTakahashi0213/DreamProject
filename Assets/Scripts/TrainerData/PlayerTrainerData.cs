using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrainerData {
	public void monsterAdd(IMonsterData addMonster) {
		if (haveMonsterSize_ == MONSTER_MAX_SIZE) return;

		monsterDatas_[haveMonsterSize_] = addMonster;
		haveMonsterSize_ += 1;
	}

	public IMonsterData GetMonsterDatas(int num) { return monsterDatas_[num]; }
	public int GetMonsterDatasLength() { return monsterDatas_.Length; }
	public int GetHaveMonsterSize() { return haveMonsterSize_; }

	//手持ちのモンスターのデータ
	private const int MONSTER_MAX_SIZE = 6;
	private int haveMonsterSize_ = 0;
	private IMonsterData[] monsterDatas_ = new IMonsterData[MONSTER_MAX_SIZE] {
		new MonsterData(new MonsterTribesData(MonsterTribesDataNumber.None), 0, 50)
		, new MonsterData(new MonsterTribesData(MonsterTribesDataNumber.None), 0, 50)
		, new MonsterData(new MonsterTribesData(MonsterTribesDataNumber.None), 0, 50)
		, new MonsterData(new MonsterTribesData(MonsterTribesDataNumber.None), 0, 50)
		, new MonsterData(new MonsterTribesData(MonsterTribesDataNumber.None), 0, 50)
		, new MonsterData(new MonsterTribesData(MonsterTribesDataNumber.None), 0, 50)
	};

	//シングルトン
	private PlayerTrainerData() { }

	static private PlayerTrainerData instance_ = null;
	static public PlayerTrainerData GetInstance() {
		if (instance_ != null) return instance_;

		instance_ = new PlayerTrainerData();
		return instance_;
	}
}
