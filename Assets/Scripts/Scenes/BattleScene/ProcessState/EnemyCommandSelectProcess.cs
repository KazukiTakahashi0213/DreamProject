using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCommandSelectProcess : IProcessState {
	public IProcessState BackProcess() {
		return this;
	}

	public IProcessState NextProcess() {
		return new CommandEventSetProcess();
	}

	public IProcessState Update(BattleManager mgr) {
		//現在、場に出ているモンスターのデータの取得
		IMonsterData enemyMD = EnemyBattleData.GetInstance().GetMonsterDatas(0);
		IMonsterData playerMD = PlayerBattleData.GetInstance().GetMonsterDatas(0);

		const int EFFECT_ATTACK_SIZE = 4;
		int nowEffectAttackSize = 0;
		int[] skillDamages = new int[EFFECT_ATTACK_SIZE] {
			0,0,0,0,
		};
		int[] skillNumbers = new int[EFFECT_ATTACK_SIZE] {
			0,0,0,0,
		};

		//攻撃技の威力計算
		for (int i = 0; i < enemyMD.GetSkillSize(); ++i) {
			//サポート技だったら処理を飛ばす
			if (enemyMD.GetSkillDatas(i).effectType_.state_ == EffectType.Support) continue;

			skillDamages[nowEffectAttackSize] = MonsterData.TestDamageCalculate(enemyMD, playerMD, enemyMD.GetSkillDatas(i));
			skillNumbers[nowEffectAttackSize] = i;
			nowEffectAttackSize += 1;
		}

		//ダメージ量を大きい順にソート
		for (int i = 0; i < nowEffectAttackSize - 1; ++i) {
			for (int j = i + 1; j < nowEffectAttackSize; ++j) {
				if (skillDamages[i] < skillDamages[j]) {
					{
						int tmp = skillDamages[i];
						skillDamages[i] = skillDamages[j];
						skillDamages[j] = tmp;
					}
					{
						int tmp = skillNumbers[i];
						skillNumbers[i] = skillNumbers[j];
						skillNumbers[j] = tmp;
					}
				}
			}
		}

		//ppがあれば、一番の火力の高い技を選択
		for (int i = 0; i < skillNumbers.Length; ++i) {
			if (EnemyBattleData.GetInstance().GetMonsterDatas(0).GetSkillDatas(skillNumbers[i]).nowPlayPoint_ > 0) {
				mgr.enemySelectSkillNumber_ = skillNumbers[i];
				i = skillNumbers.Length;
			}
		}

		//ppの消費
		ISkillData enemySkillData = EnemyBattleData.GetInstance().GetMonsterDatas(0).GetSkillDatas(mgr.enemySelectSkillNumber_);
		enemySkillData.nowPlayPoint_ -= 1;

		return mgr.nowProcessState().NextProcess();
	}
}
