using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMenuSceneBattleProcessMonsterActionSelect : BMonsterMenuSceneProcessState {
	private MonsterBattleMenuMonsterActionCommandExecuteStateProvider nowMonsterActionCommandExecuteStateProvider_ = new MonsterBattleMenuMonsterActionCommandExecuteStateProvider(MonsterBattleMenuMonsterActionCommandExecute.Trade);

	public override MonsterMenuSceneProcess Update(MonsterMenuManager monsterMenuManager) {
		AllSceneManager sceneMgr = AllSceneManager.GetInstance();
		AllEventManager eventMgr = AllEventManager.GetInstance();

		eventMgr.EventUpdate();

		if (sceneMgr.inputProvider_.UpSelect()) {
			if(monsterMenuManager.selectMonsterActionCommandNumber_ > 0) {
				monsterMenuManager.selectMonsterActionCommandNumber_ -= 1;

				nowMonsterActionCommandExecuteStateProvider_.state_ = (MonsterBattleMenuMonsterActionCommandExecute)monsterMenuManager.selectMonsterActionCommandNumber_+1;

				//カーソルの移動
				t13.UnityUtil.ObjectPosAdd(monsterMenuManager.GetMonsterActionCommandParts().GetCursorParts().gameObject, new Vector3(0, 0.55f, 0));
			}
		}
		else if (sceneMgr.inputProvider_.DownSelect()) {
			if (monsterMenuManager.selectMonsterActionCommandNumber_ < monsterMenuManager.GetMonsterActionCommandParts().GetCommandWindowTextsCount()-1) {
				monsterMenuManager.selectMonsterActionCommandNumber_ += 1;

				nowMonsterActionCommandExecuteStateProvider_.state_ = (MonsterBattleMenuMonsterActionCommandExecute)monsterMenuManager.selectMonsterActionCommandNumber_+1;

				//カーソルの移動
				t13.UnityUtil.ObjectPosAdd(monsterMenuManager.GetMonsterActionCommandParts().GetCursorParts().gameObject, new Vector3(0, -0.55f, 0));
			}
		}
		else if (sceneMgr.inputProvider_.RightSelect()) {
		}
		else if (sceneMgr.inputProvider_.LeftSelect()) {
		}
		else if (sceneMgr.inputProvider_.SelectEnter()) {
			nowMonsterActionCommandExecuteStateProvider_.Execute(monsterMenuManager);

			//モンスターの行動の選択肢の初期化
			nowMonsterActionCommandExecuteStateProvider_.state_ = MonsterBattleMenuMonsterActionCommandExecute.Trade;
			monsterMenuManager.selectMonsterActionCommandNumber_ = 0;
			t13.UnityUtil.ObjectPosMove(monsterMenuManager.GetMonsterActionCommandParts().GetCursorParts().gameObject, new Vector3(monsterMenuManager.GetMonsterActionCommandParts().GetCursorParts().transform.position.x, 0.85f + monsterMenuManager.GetMonsterActionCommandParts().gameObject.transform.position.y, monsterMenuManager.GetMonsterActionCommandParts().GetCursorParts().transform.position.z));
		}
		else if (sceneMgr.inputProvider_.SelectBack()) {
			monsterMenuManager.GetMonsterActionCommandParts().gameObject.SetActive(false);

			//操作の変更
			AllSceneManager.GetInstance().inputProvider_ = new KeyBoardNormalInputProvider();

			//モンスターの行動の選択肢の初期化
			nowMonsterActionCommandExecuteStateProvider_.state_ = MonsterBattleMenuMonsterActionCommandExecute.Trade;
			monsterMenuManager.selectMonsterActionCommandNumber_ = 0;
			t13.UnityUtil.ObjectPosMove(monsterMenuManager.GetMonsterActionCommandParts().GetCursorParts().gameObject, new Vector3(monsterMenuManager.GetMonsterActionCommandParts().GetCursorParts().transform.position.x, 0.85f + monsterMenuManager.GetMonsterActionCommandParts().gameObject.transform.position.y, monsterMenuManager.GetMonsterActionCommandParts().GetCursorParts().transform.position.z));

			return MonsterMenuSceneProcess.MonsterSelect;
		}

		return monsterMenuManager.GetNowProcessState().state_;
	}
}
