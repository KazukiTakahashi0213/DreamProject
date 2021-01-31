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
		if (!AllEventManager.GetInstance().EventUpdate()) {
			return this;
		}
		else {
			//タイプ相性の測定
			int[] typeSimillarResult = new int[3] { 0, 0, 0 };
			int[] monsterNumbers = new int[3] { 0, 1, 2 };

			for (int i = 0; i < EnemyBattleData.GetInstance().GetMonsterDatasLength(); ++i) {
				//戦えたら、None以外だったら
				if (EnemyBattleData.GetInstance().GetMonsterDatas(i).battleActive_
					&& EnemyBattleData.GetInstance().GetMonsterDatas(i).tribesData_.monsterNumber_ != 0) {
					{
						int simillarResult = PlayerBattleData.GetInstance().GetMonsterDatas(0).ElementSimillarCheckerForValue(EnemyBattleData.GetInstance().GetMonsterDatas(i).tribesData_.firstElement_);

						if (simillarResult == 0) typeSimillarResult[i] += 3;
						else if (simillarResult == 1) typeSimillarResult[i] += 1;
						else if (simillarResult == 2) typeSimillarResult[i] += 0;
						else if (simillarResult == 3) typeSimillarResult[i] += 2;
					}
					{
						int simillarResult = PlayerBattleData.GetInstance().GetMonsterDatas(0).ElementSimillarCheckerForValue(EnemyBattleData.GetInstance().GetMonsterDatas(i).tribesData_.secondElement_);

						if (simillarResult == 0) typeSimillarResult[i] += 3;
						else if (simillarResult == 1) typeSimillarResult[i] += 1;
						else if (simillarResult == 2) typeSimillarResult[i] += 0;
						else if (simillarResult == 3) typeSimillarResult[i] += 2;
					}
				}
			}

			//先頭のモンスターの相性が悪かったら
			if (typeSimillarResult[0] < 4) {
				t13.Utility.SimpleHiSort2Index(typeSimillarResult, monsterNumbers);

				EnemyBattleData.GetInstance().changeMonsterNumber_ = monsterNumbers[0];

				if (EnemyBattleData.GetInstance().changeMonsterNumber_ > 0) {
					EnemyBattleData.GetInstance().changeMonsterActive_ = true;
				}
			}

			if (EnemyBattleData.GetInstance().changeMonsterActive_ == false) {

				//現在、場に出ているモンスターのデータの取得
				IMonsterData enemyMD = EnemyBattleData.GetInstance().GetMonsterDatas(EnemyBattleData.GetInstance().changeMonsterNumber_);
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

				//dpが100以下だったら
				if (EnemyBattleData.GetInstance().dreamPoint_ <= 100) {
					//dpの変動
					EnemyBattleData.GetInstance().dreamPoint_ += enemySkillData.upDpValue_;
				}

				//dpの演出のイベント
				AllEventManager.GetInstance().EventWaitSet(2.0f);
			}

			return mgr.nowProcessState().NextProcess();
		}
	}
}
