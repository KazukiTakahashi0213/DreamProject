﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSceneMenuSelectCommandSelectSkill : BaseMapSceneMenuSelectCommandSelectState {
	public override void SelectEnter(MapManager mapManager) {
		AllEventManager eventMgr = AllEventManager.GetInstance();
		AllSceneManager sceneMgr = AllSceneManager.GetInstance();

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
		eventMgr.SceneChangeEventSet(SceneState.BugMenu, SceneChangeMode.Change);
	}
}