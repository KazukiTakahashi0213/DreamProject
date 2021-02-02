using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITrainerBattleData {
	void monsterAdd(IMonsterData addMonster);

	IMonsterData GetMonsterDatas(int num);
	int GetMonsterDatasLength();
	int GetHaveMonsterSize();
	string GetUniqueTrainerName();

	//倒れた時の処理
	void MonsterDownEventSet(BattleManager manager);

	//交換処理
	void MonsterChangeEventSet(BattleManager manager);
}
