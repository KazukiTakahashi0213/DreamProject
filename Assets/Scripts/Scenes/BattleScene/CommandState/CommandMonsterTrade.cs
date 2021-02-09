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
		AllEventManager eventMgr = AllEventManager.GetInstance();
		AllSceneManager sceneMgr = AllSceneManager.GetInstance();

		mgr.InactiveUiCommand();

		sceneMgr.inputProvider_ = new InactiveInputProvider();

		//フェードアウト
		eventMgr.EventSpriteRendererSet(
			sceneMgr.GetPublicFrontScreen().GetEventScreenSprite()
			, null
			, new Color(sceneMgr.GetPublicFrontScreen().GetEventScreenSprite().GetSpriteRenderer().color.r, sceneMgr.GetPublicFrontScreen().GetEventScreenSprite().GetSpriteRenderer().color.g, sceneMgr.GetPublicFrontScreen().GetEventScreenSprite().GetSpriteRenderer().color.b, 255)
			);
		eventMgr.EventSpriteRenderersUpdateExecuteSet(EventSpriteRendererEventManagerExecute.ChangeColor);
		eventMgr.AllUpdateEventExecute(0.4f);

		//シーンの切り替え
		eventMgr.SceneChangeEventSet(SceneState.MonsterBattleMenu, SceneChangeMode.Slide);

		return mgr.nowProcessState();
	}
}
