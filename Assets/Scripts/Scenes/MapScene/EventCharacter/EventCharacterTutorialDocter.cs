using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCharacterTutorialDocter : MonoBehaviour {
	//EntryPoint
	void Start() {
		eventMoveMap_ = GetComponent<EventMoveMap>();

		eventMoveMap_.GetEventSetFuncs().Add(BattleStart);
	}

	void Update() {
		
	}

	private EventMoveMap eventMoveMap_ = null;

	private static void BattleStart(EventMoveMap eventMoveMap, MapManager mapManager) {
		AllEventManager allEventMgr = AllEventManager.GetInstance();
		AllSceneManager allSceneMgr = AllSceneManager.GetInstance();

		//操作の変更
		allSceneMgr.inputProvider_ = new KeyBoardSelectInactiveTriggerInputProvider();

		string context = ResourcesTextsLoader.GetInstance().GetTexts("test");

		List<string> contexts = t13.Utility.ContextSlice(context, "\r\n\r\n");

		mapManager.GetNovelWindowParts().GetNovelBlinkIconParts().GetNovelBlinkIconEventSprite().blinkTimeRegulation_ = 0.5f;

		mapManager.GetNovelWindowParts().GetNovelBlinkIconParts().GetNovelBlinkIconEventSprite().GetBlinkState().state_ = UpdateSpriteRendererProcessBlink.In;

		//ウィンドウの表示
		allEventMgr.UpdateGameObjectSet(mapManager.GetNovelWindowParts().GetUpdateGameObject());
		allEventMgr.UpdateGameObjectsActiveSetExecute(true);

		for (int i = 0;i < contexts.Count; ++i) {
			//文字列の処理
			allEventMgr.EventTextSet(mapManager.GetNovelWindowParts().GetEventText(), contexts[i]);
			allEventMgr.EventTextsUpdateExecuteSet(EventTextEventManagerExecute.CharaUpdate);
			allEventMgr.AllUpdateEventExecute(0.4f);

			//Blinkの開始
			allEventMgr.EventSpriteRendererSet(mapManager.GetNovelWindowParts().GetNovelBlinkIconParts().GetNovelBlinkIconEventSprite());
			allEventMgr.EventSpriteRenderersUpdateExecuteSet(EventSpriteRendererEventManagerExecute.BlinkStart);
			allEventMgr.AllUpdateEventExecute();

			//Enterの押下待ち
			allEventMgr.EventTriggerSet();

			//Blinkの終了
			allEventMgr.EventSpriteRendererSet(mapManager.GetNovelWindowParts().GetNovelBlinkIconParts().GetNovelBlinkIconEventSprite());
			allEventMgr.EventSpriteRenderersUpdateExecuteSet(EventSpriteRendererEventManagerExecute.BlinkEnd);
			allEventMgr.AllUpdateEventExecute();
		}

		//操作の変更
		allEventMgr.InputProviderChangeEventSet(new KeyBoardNormalInputProvider());

		//ウィンドウの非表示
		allEventMgr.UpdateGameObjectSet(mapManager.GetNovelWindowParts().GetUpdateGameObject());
		allEventMgr.UpdateGameObjectsActiveSetExecute(false);

		//イベントの最後
		allEventMgr.EventFinishSet();
	}
}
