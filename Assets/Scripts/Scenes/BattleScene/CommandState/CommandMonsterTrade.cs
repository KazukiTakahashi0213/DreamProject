using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandMonsterTrade : ICommandState {
	public ICommandState DownSelect(BattleManager mgr) {
		return this;
	}
	public ICommandState LeftSelect(BattleManager mgr) {
		return this;
	}
	public ICommandState RightSelect(BattleManager mgr) {
		//どくのダメージ処理
		mgr.PoisonDamageProcess(PlayerBattleData.GetInstance(), mgr.GetPlayerStatusInfoParts(), mgr.GetPlayerMonsterParts());

		mgr.CommandRightCursorMove();
		return new CommandEscape();
	}
	public ICommandState UpSelect(BattleManager mgr) {
		//どくのダメージ処理
		mgr.PoisonDamageProcess(PlayerBattleData.GetInstance(), mgr.GetPlayerStatusInfoParts(), mgr.GetPlayerMonsterParts());

		mgr.CommandUpCursorMove();
		return new CommandAttack();
	}

	public IProcessState Execute(BattleManager mgr) {
		mgr.InactiveUiCommand();

		AllSceneManager.GetInstance().SceneChange(SceneState.MonsterBattleMenu, SceneChangeMode.Slide);

		return mgr.nowProcessState();
	}
}
