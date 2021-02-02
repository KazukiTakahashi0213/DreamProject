﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandDream : ICommandState {
	public ICommandState DownSelect(BattleManager mgr) {
		//どくのダメージ処理
		mgr.PoisonDamageProcess();

		mgr.CommandDownCursorMove();
		return new CommandEscape();
	}
	public ICommandState LeftSelect(BattleManager mgr) {
		//どくのダメージ処理
		mgr.PoisonDamageProcess();

		mgr.CommandLeftCursorMove();
		return new CommandAttack();
	}
	public ICommandState RightSelect(BattleManager mgr) {
		return this;
	}
	public ICommandState UpSelect(BattleManager mgr) {
		return this;
	}

	public IProcessState Execute(BattleManager mgr) {
		//dpが100以上だったら
		if (PlayerBattleData.GetInstance().dreamPoint_ >= 100) {
			if (PlayerBattleData.GetInstance().dreamSyncronize_ == false) {
				//ゆめの文字色の変更
				mgr.GetNovelWindowParts().GetCommandParts().GetCommandWindowTexts(1).color = new Color32(94, 120, 255, 255);

				//パワーアップするか否かのフラグの設定
				PlayerBattleData.GetInstance().dreamSyncronize_ = true;
			}
			else {
				//ゆめの文字色の変更
				mgr.GetNovelWindowParts().GetCommandParts().GetCommandWindowTexts(1).color = new Color32(50, 50, 50, 255);

				//パワーアップするか否かのフラグの設定
				PlayerBattleData.GetInstance().dreamSyncronize_ = false;
			}
		}

		return mgr.nowProcessState();
	}
}