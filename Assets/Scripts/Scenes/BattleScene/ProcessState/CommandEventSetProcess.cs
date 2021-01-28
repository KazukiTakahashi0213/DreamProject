using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CommandEventSetProcess : IProcessState {
	public IProcessState BackProcess() {
		return this;
	}

	public IProcessState NextProcess() {
		return new CommandEventExecuteProcess();
	}

	public IProcessState Update(BattleManager mgr) {
		//TODO
		//状態異常処理の表示の実装

		//現在、場に出ているモンスターのデータの取得
		IMonsterData enemyMonsterData = EnemyBattleData.GetInstance().GetMonsterDatas(0);
		IMonsterData playerMonsterData = PlayerBattleData.GetInstance().GetMonsterDatas(0);

		//現在、場に出ているモンスターの選択技のデータの取得
		ISkillData enemySkillData = enemyMonsterData.GetSkillDatas(mgr.enemySelectSkillNumber_);
		ISkillData playerSkillData = playerMonsterData.GetSkillDatas(mgr.playerSelectSkillNumber_);

		//技の優先度で行動順を決める
		if (enemySkillData.triggerPriority_ < playerSkillData.triggerPriority_) {
			//プレイヤーの戦闘処理
			PlayerSkillResultSet(mgr, playerMonsterData, playerSkillData, enemyMonsterData);
			//エネミーの戦闘処理
			EnemySkillResultSet(mgr, enemyMonsterData, enemySkillData, playerMonsterData);
		}
		else if (enemySkillData.triggerPriority_ > playerSkillData.triggerPriority_) {
			//エネミーの戦闘処理
			EnemySkillResultSet(mgr, enemyMonsterData, enemySkillData, playerMonsterData);
			//プレイヤーの戦闘処理
			PlayerSkillResultSet(mgr, playerMonsterData, playerSkillData, enemyMonsterData);
		}
		//素早さで行動順を決める
		else if (enemyMonsterData.RealSpeed() < playerMonsterData.RealSpeed()) {
			//プレイヤーの戦闘処理
			PlayerSkillResultSet(mgr, playerMonsterData, playerSkillData, enemyMonsterData);
			//エネミーの戦闘処理
			EnemySkillResultSet(mgr, enemyMonsterData, enemySkillData, playerMonsterData);
		}
		else if (enemyMonsterData.RealSpeed() == playerMonsterData.RealSpeed()) {
			if (AllSceneManager.GetInstance().GetRandom().Next(0, 2) == 0) {
				//プレイヤーの戦闘処理
				PlayerSkillResultSet(mgr, playerMonsterData, playerSkillData, enemyMonsterData);
				//エネミーの戦闘処理
				EnemySkillResultSet(mgr, enemyMonsterData, enemySkillData, playerMonsterData);
			}
			else {
				//エネミーの戦闘処理
				EnemySkillResultSet(mgr, enemyMonsterData, enemySkillData, playerMonsterData);
				//プレイヤーの戦闘処理
				PlayerSkillResultSet(mgr, playerMonsterData, playerSkillData, enemyMonsterData);
			}
		}
		else {
			//エネミーの戦闘処理
			EnemySkillResultSet(mgr, enemyMonsterData, enemySkillData, playerMonsterData);
			//プレイヤーの戦闘処理
			PlayerSkillResultSet(mgr, playerMonsterData, playerSkillData, enemyMonsterData);
		}

		//白紙に
		AllEventManager.GetInstance().EventTextSet(mgr.GetNovelWindowParts().GetEventText(), " ");
		AllEventManager.GetInstance().EventTextsUpdateExecuteSet(EventTextEventManagerExecute.CharaUpdate);
		AllEventManager.GetInstance().AllUpdateEventExecute();
		//ウェイト
		AllEventManager.GetInstance().EventWaitSet(0.5f);

		//イベントの最後
		AllEventManager.GetInstance().EventFinishSet();

		//コマンドUIの非表示
		mgr.InactiveUiAttackCommand();

		return mgr.nowProcessState().NextProcess();
	}

	//HACK 同じ事をしている
	private void EnemySkillResultSet(BattleManager mgr, IMonsterData attackMonsterData, ISkillData attackSkillData, IMonsterData defenseMonsterData) {
		//エネミーの文字列の設定
		string skillUseContext = "あいての　" + attackMonsterData.uniqueName_ + "の\n"
			+ attackSkillData.skillNname_ + "！";

		AllEventManager.GetInstance().EventTextSet(mgr.GetNovelWindowParts().GetEventText(), skillUseContext);
		AllEventManager.GetInstance().EventTextsUpdateExecuteSet(EventTextEventManagerExecute.CharaUpdate);
		AllEventManager.GetInstance().AllUpdateEventExecute(mgr.GetEventContextUpdateTime());

		AllEventManager.GetInstance().EventWaitSet(mgr.GetEventWaitTime());

		//技のイベントの設定
		attackSkillData.effectType_.ExecuteEnemyEventSet(mgr, attackMonsterData, attackSkillData, defenseMonsterData);

		//追加効果の判定
		bool optionEffectTrigger = AllSceneManager.GetInstance().GetRandom().Next(1, 101) <= attackSkillData.optionEffectTriggerRateValue_;

		//追加効果の処理
		if (optionEffectTrigger) {
			//能力変化の処理
			ParameterRankEventSet(mgr, defenseMonsterData, attackSkillData, attackMonsterData);

			//状態異常の処理
			AbnormalEventSet(mgr, defenseMonsterData, attackSkillData, attackMonsterData);
		}
	}
	private void PlayerSkillResultSet(BattleManager mgr, IMonsterData attackMonsterData, ISkillData attackSkillData, IMonsterData defenseMonsterData) {
		//プレイヤーの文字列の設定
		string skillUseContext = attackMonsterData.uniqueName_ + "の\n"
			+ attackSkillData.skillNname_ + "！";

		AllEventManager.GetInstance().EventTextSet(mgr.GetNovelWindowParts().GetEventText(), skillUseContext);
		AllEventManager.GetInstance().EventTextsUpdateExecuteSet(EventTextEventManagerExecute.CharaUpdate);
		AllEventManager.GetInstance().AllUpdateEventExecute(mgr.GetEventContextUpdateTime());

		AllEventManager.GetInstance().EventWaitSet(mgr.GetEventWaitTime());

		//技のイベントの設定
		attackSkillData.effectType_.ExecutePlayerEventSet(mgr, attackMonsterData, attackSkillData, defenseMonsterData);

		//追加効果の判定
		bool optionEffectTrigger = AllSceneManager.GetInstance().GetRandom().Next(1, 101) <= attackSkillData.optionEffectTriggerRateValue_;

		//追加効果の処理
		if (optionEffectTrigger) {
			//能力変化の処理
			ParameterRankEventSet(mgr, attackMonsterData, attackSkillData, defenseMonsterData);

			//状態異常の処理
			AbnormalEventSet(mgr, attackMonsterData, attackSkillData, defenseMonsterData);
		}
	}

	private void ParameterRankEventSet(BattleManager mgr, IMonsterData playerMonsterData, ISkillData attackSkillData, IMonsterData enemyMonsterData) {
		if (attackSkillData.addPlayerParameterRanks_[0].state_ != AddParameterRank.None) {

			if (attackSkillData.addPlayerParameterRanks_.Count > 0) {
				//アニメーション
				AllEventManager.GetInstance().EventWaitSet(1.0f);
			}

			//プレイヤーへの能力変化の処理
			for (int i = 0; i < attackSkillData.addPlayerParameterRanks_.Count; ++i) {
				//能力変化の更新
				string parameterRankContext = attackSkillData.addPlayerParameterRanks_[i].AddParameterExecute(playerMonsterData);

				//文字列のイベント
				AllEventManager.GetInstance().EventTextSet(mgr.GetNovelWindowParts().GetEventText(), parameterRankContext);
				AllEventManager.GetInstance().EventTextsUpdateExecuteSet(EventTextEventManagerExecute.CharaUpdate);
				AllEventManager.GetInstance().AllUpdateEventExecute(mgr.GetEventContextUpdateTime());

				AllEventManager.GetInstance().EventWaitSet(mgr.GetEventWaitTime());
			}
		}

		if (attackSkillData.addEnemyParameterRanks_[0].state_ != AddParameterRank.None) {

			if (attackSkillData.addEnemyParameterRanks_.Count > 0) {
				//アニメーション
				AllEventManager.GetInstance().EventWaitSet(1.0f);
			}

			//エネミーへの能力変化の処理
			for (int i = 0; i < attackSkillData.addEnemyParameterRanks_.Count; ++i) {
				//能力変化の更新
				string parameterRankContext = attackSkillData.addEnemyParameterRanks_[i].AddParameterExecute(enemyMonsterData);

				//文字列のイベント
				AllEventManager.GetInstance().EventTextSet(mgr.GetNovelWindowParts().GetEventText(), "あいての　" + parameterRankContext);
				AllEventManager.GetInstance().EventTextsUpdateExecuteSet(EventTextEventManagerExecute.CharaUpdate);
				AllEventManager.GetInstance().AllUpdateEventExecute(mgr.GetEventContextUpdateTime());

				AllEventManager.GetInstance().EventWaitSet(mgr.GetEventWaitTime());
			}
		}
	}
	private void AbnormalEventSet(BattleManager mgr, IMonsterData playerMonsterData, ISkillData attackSkillData, IMonsterData enemyMonsterData) {
		if (attackSkillData.addPlayerAbnormalStates_[0].state_ != AddAbnormalType.None) {

			if (attackSkillData.addPlayerAbnormalStates_.Count > 0) {
				//アニメーション
				AllEventManager.GetInstance().EventWaitSet(1.0f);
			}

			//プレイヤーへの状態異常の処理
			for (int i = 0; i < attackSkillData.addPlayerAbnormalStates_.Count; ++i) {
				//能力変化の更新
				string abnormalStateContext = attackSkillData.addPlayerAbnormalStates_[i].AddAbnormalTypeExecute(playerMonsterData);

				//文字列のイベント
				AllEventManager.GetInstance().EventTextSet(mgr.GetNovelWindowParts().GetEventText(), abnormalStateContext);
				AllEventManager.GetInstance().EventTextsUpdateExecuteSet(EventTextEventManagerExecute.CharaUpdate);
				AllEventManager.GetInstance().AllUpdateEventExecute(mgr.GetEventContextUpdateTime());

				AllEventManager.GetInstance().EventWaitSet(mgr.GetEventWaitTime());
			}
		}

		if (attackSkillData.addEnemyAbnormalStates_[0].state_ != AddAbnormalType.None) {

			if (attackSkillData.addEnemyAbnormalStates_.Count > 0) {
				//アニメーション
				AllEventManager.GetInstance().EventWaitSet(1.0f);
			}

			//エネミーへの状態異常の処理
			for (int i = 0; i < attackSkillData.addEnemyAbnormalStates_.Count; ++i) {
				//能力変化の更新
				string abnormalStateContext = attackSkillData.addEnemyAbnormalStates_[i].AddAbnormalTypeExecute(enemyMonsterData);

				//文字列のイベント
				AllEventManager.GetInstance().EventTextSet(mgr.GetNovelWindowParts().GetEventText(), "あいての　" + abnormalStateContext);
				AllEventManager.GetInstance().EventTextsUpdateExecuteSet(EventTextEventManagerExecute.CharaUpdate);
				AllEventManager.GetInstance().AllUpdateEventExecute(mgr.GetEventContextUpdateTime());

				AllEventManager.GetInstance().EventWaitSet(mgr.GetEventWaitTime());
			}
		}
	}
}
