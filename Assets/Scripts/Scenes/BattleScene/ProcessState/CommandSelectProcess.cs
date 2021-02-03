using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandSelectProcess : IProcessState {
	public IProcessState BackProcess() {
		return this;
	}

	public IProcessState NextProcess() {
		return new AttackCommandSelectProcess();
	}

	public IProcessState Update(BattleManager mgr) {
		AllSceneManager allSceneMgr = AllSceneManager.GetInstance();

		//敵の思考時間の処理
		EnemyBattleData.GetInstance().ThinkingTimeCounter();

		//モンスターが交換されていたら
		if (PlayerBattleData.GetInstance().changeMonsterActive_ == true) {
			if (PlayerBattleData.GetInstance().changeMonsterNumber_ > 0) {
				//アイドル状態の停止
				mgr.GetPlayerStatusInfoParts().ProcessIdleEnd();
				mgr.GetPlayerMonsterParts().ProcessIdleEnd();

				allSceneMgr.inputProvider_ = new KeyBoardInactiveInputProvider();

				//イベントの最後
				AllEventManager.GetInstance().EventFinishSet();

				return new EnemyCommandSelectProcess();
			}
			else {
				allSceneMgr.inputProvider_ = new KeyBoardInactiveInputProvider();

				mgr.ActiveUiCommand();
				mgr.InactiveUiCommand();

				AllEventManager.GetInstance().EventWaitSet(mgr.GetEventWaitTime());

				AllEventManager.GetInstance().UpdateGameObjectSet(mgr.GetCursorParts().GetEventGameObject());
				AllEventManager.GetInstance().UpdateGameObjectSet(mgr.GetNovelWindowParts().GetCommandParts().GetEventGameObject());
				AllEventManager.GetInstance().UpdateGameObjectsActiveSetExecute(true);

				//dpが100以上だったら
				if (PlayerBattleData.GetInstance().dreamPoint_ >= 100) {
					AllEventManager.GetInstance().EventTextSet(mgr.GetNovelWindowParts().GetEventText()
						, "ゆめたちが　\n"
						+ "きょうめいしている・・・");
					AllEventManager.GetInstance().EventTextsUpdateExecuteSet(EventTextEventManagerExecute.CharaUpdate);
					AllEventManager.GetInstance().AllUpdateEventExecute();
				}
				else {
					AllEventManager.GetInstance().EventTextSet(mgr.GetNovelWindowParts().GetEventText(), PlayerBattleData.GetInstance().GetMonsterDatas(0).uniqueName_ + "は　どうする？");
					AllEventManager.GetInstance().EventTextsUpdateExecuteSet(EventTextEventManagerExecute.CharaUpdate);
					AllEventManager.GetInstance().AllUpdateEventExecute();
				}

				AllEventManager.GetInstance().EventStatusInfoPartsSet(mgr.GetPlayerStatusInfoParts(), new Color32(0, 0, 0, 0));
				AllEventManager.GetInstance().StatusInfoPartsUpdateExecuteSet(StatusInfoPartsEventManagerExecute.IdleMoveStart);
				AllEventManager.GetInstance().AllUpdateEventExecute();

				AllEventManager.GetInstance().EventFinishSet();

				PlayerBattleData.GetInstance().changeMonsterActive_ = false;
			}
		}

		//やけどのダメージ処理
		if(mgr.BurnsDamageProcess(PlayerBattleData.GetInstance(), mgr.GetPlayerStatusInfoParts(), mgr.GetPlayerMonsterParts())) {
			return new CommandEventExecuteProcess();
		}

		if (EnemyBattleData.GetInstance().GetThinkingEnd() == false) {
			//やけどのダメージ処理
			if(mgr.BurnsDamageProcess(EnemyBattleData.GetInstance(), mgr.GetEnemyStatusInfoParts(), mgr.GetEnemyMonsterParts())) {
				return new CommandEventExecuteProcess();
			}

			if (EnemyBattleData.GetInstance().PoinsonCounter()) {
				//どくのダメージ処理
				mgr.PoisonDamageProcess(EnemyBattleData.GetInstance(), mgr.GetEnemyStatusInfoParts(), mgr.GetEnemyMonsterParts());
				if (mgr.PoisonDamageDown()) return new CommandEventExecuteProcess();
			}
		}

		if (AllEventManager.GetInstance().EventUpdate()) {
			allSceneMgr.inputProvider_ = new KeyBoardNormalTriggerInputProvider();
		}

		if (allSceneMgr.inputProvider_.UpSelect()) {
			mgr.nowCommandState_ = mgr.nowCommandState_.UpSelect(mgr);
			if (mgr.PoisonDamageDown()) return new CommandEventExecuteProcess();
		}
		else if (allSceneMgr.inputProvider_.DownSelect()) {
			mgr.nowCommandState_ = mgr.nowCommandState_.DownSelect(mgr);
			if (mgr.PoisonDamageDown()) return new CommandEventExecuteProcess();
		}
		else if (allSceneMgr.inputProvider_.RightSelect()) {
			mgr.nowCommandState_ = mgr.nowCommandState_.RightSelect(mgr);
			if (mgr.PoisonDamageDown()) return new CommandEventExecuteProcess();
		}
		else if (allSceneMgr.inputProvider_.LeftSelect()) {
			mgr.nowCommandState_ = mgr.nowCommandState_.LeftSelect(mgr);
			if (mgr.PoisonDamageDown()) return new CommandEventExecuteProcess();
		}
		else if (allSceneMgr.inputProvider_.SelectEnter()) {
			return mgr.nowCommandState_.Execute(mgr);
		}
		else if (allSceneMgr.inputProvider_.SelectNovelWindowActive()) {
			mgr.GetNovelWindowPartsActiveState().state_ = mgr.GetNovelWindowPartsActiveState().Next(mgr);
		}

		return this;
	}
}
