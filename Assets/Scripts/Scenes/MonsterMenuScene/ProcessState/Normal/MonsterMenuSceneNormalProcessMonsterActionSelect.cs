﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMenuSceneNormalProcessMonsterActionSelect : BMonsterMenuSceneProcessState {
	private MonsterNormalMenuSceneMonsterActionCommandExecuteStateProvider nowMonsterActionCommandExecuteStateProvider_ = new MonsterNormalMenuSceneMonsterActionCommandExecuteStateProvider(MonsterNormalMenuSceneMonsterActionCommandExecute.Swap);

	public override MonsterMenuSceneProcess Update(MonsterMenuManager monsterMenuManager) {
		AllSceneManager sceneMgr = AllSceneManager.GetInstance();
		AllEventManager eventMgr = AllEventManager.GetInstance();

		eventMgr.EventUpdate();

		if (sceneMgr.inputProvider_.UpSelect()) {
			if (monsterMenuManager.GetMonsterActionCommandParts().GetSelectNumber()+1 > 0) {
				//SE
				monsterMenuManager.GetInputSoundProvider().UpSelect();

				//選択肢の処理
				monsterMenuManager.GetMonsterActionCommandParts().CommandSelect(-1, new Vector3(0, 0.55f, 0));

				nowMonsterActionCommandExecuteStateProvider_.state_ = (MonsterNormalMenuSceneMonsterActionCommandExecute)monsterMenuManager.GetMonsterActionCommandParts().GetSelectNumber();
			}
		}
		else if (sceneMgr.inputProvider_.DownSelect()) {
			if (monsterMenuManager.GetMonsterActionCommandParts().GetSelectNumber() < monsterMenuManager.GetMonsterActionCommandParts().GetCommandWindowTextsCount() - 1) {
				//SE
				monsterMenuManager.GetInputSoundProvider().DownSelect();

				//選択肢の処理
				monsterMenuManager.GetMonsterActionCommandParts().CommandSelect(1, new Vector3(0, -0.55f, 0));

				nowMonsterActionCommandExecuteStateProvider_.state_ = (MonsterNormalMenuSceneMonsterActionCommandExecute)monsterMenuManager.GetMonsterActionCommandParts().GetSelectNumber();
			}
		}
		else if (sceneMgr.inputProvider_.RightSelect()) {
		}
		else if (sceneMgr.inputProvider_.LeftSelect()) {
		}
		else if (sceneMgr.inputProvider_.SelectEnter()) {
			//SE
			monsterMenuManager.GetInputSoundProvider().SelectEnter();

			nowMonsterActionCommandExecuteStateProvider_.Execute(monsterMenuManager);

			//モンスターの行動の選択肢の初期化
			nowMonsterActionCommandExecuteStateProvider_.state_ = MonsterNormalMenuSceneMonsterActionCommandExecute.Swap;
			monsterMenuManager.GetMonsterActionCommandParts().SelectReset(new Vector3(-0.6f, 0.85f, -4));
		}
		else if (sceneMgr.inputProvider_.SelectBack()) {
			monsterMenuManager.GetMonsterActionCommandParts().gameObject.SetActive(false);

			//操作の変更
			AllSceneManager.GetInstance().inputProvider_ = new KeyBoardNormalInputProvider();

			//モンスターの行動の選択肢の初期化
			nowMonsterActionCommandExecuteStateProvider_.state_ = MonsterNormalMenuSceneMonsterActionCommandExecute.Swap;
			monsterMenuManager.GetMonsterActionCommandParts().SelectReset(new Vector3(-0.6f, 0.85f, -4));

			return MonsterMenuSceneProcess.MonsterSelect;
		}

		return monsterMenuManager.GetNowProcessState().state_;
	}
}
