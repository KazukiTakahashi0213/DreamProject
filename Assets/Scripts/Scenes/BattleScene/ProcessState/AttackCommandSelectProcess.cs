using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCommandSelectProcess : IProcessState {
	public IProcessState BackProcess() {
		return new CommandSelectProcess();
	}

	public IProcessState NextProcess() {
		return new EnemyCommandSelectProcess();
	}

	public IProcessState Update(BattleManager mgr) {
		//モンスターが交換されていたら
		if (PlayerBattleData.GetInstance().changeMonsterActive_ == true) {
			if (PlayerBattleData.GetInstance().changeMonsterNumber_ > 0) {
				//アイドル状態の停止
				mgr.GetPlayerStatusInfoParts().ProcessIdleEnd();
				mgr.GetPlayerMonsterParts().ProcessIdleEnd();

				//イベントの最後
				AllEventManager.GetInstance().EventFinishSet();

				return new EnemyCommandSelectProcess();
			}
			else {
				mgr.SetInputProvider(new KeyBoardInactiveInputProvider());

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
		mgr.BurnsDamageProcess();

		//こんらんの処理
		mgr.ConfusionProcess();

		if (AllEventManager.GetInstance().EventUpdate()) {
			mgr.SetInputProvider(new KeyBoardNormalInputProvider());
		}

		if (mgr.GetInputProvider().UpSelect()) {
			mgr.nowAttackCommandState_ = mgr.nowAttackCommandState_.UpSelect(mgr);
		}
		else if (mgr.GetInputProvider().DownSelect()) {
			mgr.nowAttackCommandState_ = mgr.nowAttackCommandState_.DownSelect(mgr);
		}
		else if (mgr.GetInputProvider().RightSelect()) {
			mgr.nowAttackCommandState_ = mgr.nowAttackCommandState_.RightSelect(mgr);
		}
		else if (mgr.GetInputProvider().LeftSelect()) {
			mgr.nowAttackCommandState_ = mgr.nowAttackCommandState_.LeftSelect(mgr);
		}
		else if (mgr.GetInputProvider().SelectEnter()) {
			ISkillData playerSkillData = PlayerBattleData.GetInstance().GetMonsterDatas(0).GetSkillDatas(mgr.playerSelectSkillNumber_);

			if (playerSkillData.nowPlayPoint_ > 0) {
				mgr.GetPlayerStatusInfoParts().ProcessIdleEnd();
				mgr.GetPlayerMonsterParts().ProcessIdleEnd();

				//コマンドUIの非表示
				mgr.InactiveUiAttackCommand();

				//ppの消費
				playerSkillData.nowPlayPoint_ -= 1;

				//dpが100以下だったら
				if (PlayerBattleData.GetInstance().dreamPoint_ <= 100) {
					//dpの変動
					PlayerBattleData.GetInstance().dreamPoint_ += playerSkillData.upDpValue_;
				}

				//dpの演出のイベント
				AllEventManager.GetInstance().EventWaitSet(2.0f);

				//イベントの最後
				AllEventManager.GetInstance().EventFinishSet();

				return mgr.nowAttackCommandState_.Execute(mgr);
			}
		}
		else if (mgr.GetInputProvider().SelectBack()) {
			//こんらん状態なら
			if (PlayerBattleData.GetInstance().GetMonsterDatas(0).battleData_.firstAbnormalState_.state_ == AbnormalType.Confusion
				|| PlayerBattleData.GetInstance().GetMonsterDatas(0).battleData_.secondAbnormalState_.state_ == AbnormalType.Confusion) {
				mgr.SetInputProvider(new KeyBoardNormalInputProvider());
			}

			mgr.ChangeUiCommand();

			return mgr.nowProcessState().BackProcess();
		}
		else if (mgr.GetInputProvider().SelectNovelWindowActive()) {
			mgr.GetNovelWindowPartsActiveState().state_ = mgr.GetNovelWindowPartsActiveState().Next(mgr);
		}

		return this;
	}
}
