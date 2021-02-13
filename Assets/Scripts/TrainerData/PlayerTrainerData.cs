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

	//手持ちの技のデータ
	private List<SkillData> skillDatas_ = new List<SkillData>();
	public SkillData GetSkillDatas(int value) { return skillDatas_[value]; }
	public int GetSkillDatasCount() { return skillDatas_.Count; }

	//シングルトン
	private PlayerTrainerData() {
		//仮
		skillDatas_.Add(new SkillData(SkillDataNumber.Taiatari));
		skillDatas_.Add(new SkillData(SkillDataNumber.Hiitosutanpu));
		skillDatas_.Add(new SkillData(SkillDataNumber.Mizushuriken));
		skillDatas_.Add(new SkillData(SkillDataNumber.Uddohanmaa));
		skillDatas_.Add(new SkillData(SkillDataNumber.Taiatari));
		skillDatas_.Add(new SkillData(SkillDataNumber.Hiitosutanpu));
		skillDatas_.Add(new SkillData(SkillDataNumber.Mizushuriken));
		skillDatas_.Add(new SkillData(SkillDataNumber.Uddohanmaa));
	}

	static private PlayerTrainerData instance_ = null;
	static public PlayerTrainerData GetInstance() {
		if (instance_ != null) return instance_;

		instance_ = new PlayerTrainerData();
		return instance_;
	}
}
