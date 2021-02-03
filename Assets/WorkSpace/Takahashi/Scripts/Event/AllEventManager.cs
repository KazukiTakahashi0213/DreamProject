﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllEventManager {
	//依存性注入
	private IInputProvider inputProvider_ = new KeyBoardNormalTriggerInputProvider();

	private t13.TimeCounter sceneCounter_ = new t13.TimeCounter();
	private List<t13.TimeFluct> sceneFlucts_ = new List<t13.TimeFluct>();
	private t13.Event<AllEventManager> sceneEvent_;

	public t13.TimeCounter GetSceneCounter() { return sceneCounter_; }
	public List<t13.TimeFluct> GetSceneFlucts() { return sceneFlucts_; }
	public bool EventUpdate() { return sceneEvent_.update(); }

	//eventの管理メンバ
	private EventSpriteRendererEventManager eventSpriteEventManager_ = new EventSpriteRendererEventManager();
	private UpdateGameObjectEventManager updateGameObjectEventManager_ = new UpdateGameObjectEventManager();
	private EventTextEventManager eventTextEventManager_ = new EventTextEventManager();
	private HpGaugePartsEventManager hpGaugePartsEventManager_ = new HpGaugePartsEventManager();
	private StatusInfoPartsEventManager statusInfoPartsEventManager_ = new StatusInfoPartsEventManager();

	private int updateEventExecuteCounter_ = 0;
	private List<float> eventTimeRegulation_ = new List<float>();
	private List<t13.TimeFluctProcess> eventTimeFluctProcesses_ = new List<t13.TimeFluctProcess>();
	private UpdateGameObjectEventManagerExecute updateGameObjectEventManagerExecute_ = UpdateGameObjectEventManagerExecute.None;
	private EventSpriteRendererEventManagerExecute eventSpriteRendererEventManagerExecute_ = EventSpriteRendererEventManagerExecute.None;
	private HpGaugePartsEventManagerExecute hpGaugePartsEventManagerExecute_ = HpGaugePartsEventManagerExecute.None;
	private EventTextEventManagerExecute eventTextEventManagerExecute_ = EventTextEventManagerExecute.None;
	private StatusInfoPartsEventManagerExecute statusInfoPartsEventManagerExecute_ = StatusInfoPartsEventManagerExecute.None;

	private int eventActiveExecuteCounter_ = 0;
	private List<bool> eventActive_ = new List<bool>();

	private int eventSceneChangeExecuteCounter_ = 0;
	private List<SceneState> sceneStates_ = new List<SceneState>();
	private List<SceneChangeMode> sceneChangeModes_ = new List<SceneChangeMode>();

	//EventManager
	public void EventWaitSet(float timeRegulation) {
		eventTimeRegulation_.Add(timeRegulation);
		eventTimeFluctProcesses_.Add(t13.TimeFluctProcess.None);

		sceneEvent_.func_add(WaitEvent);
	}
	public void EventWaitEnterSelectSet() {
		sceneEvent_.func_add(WaitEnterSelectEvent);
	}
	public void EventFinishSet() {
		sceneEvent_.func_add(EventFinishEvent);
	}
	public void AllUpdateEventExecute(float timeRegulation = 0, t13.TimeFluctProcess timeFluctProcess = t13.TimeFluctProcess.Liner) {
		eventTimeRegulation_.Add(timeRegulation);
		eventTimeFluctProcesses_.Add(timeFluctProcess);

		updateGameObjectEventManager_.UpdateGameObjectsExecuteSet(updateGameObjectEventManagerExecute_);
		eventSpriteEventManager_.EventSpriteRenderersExecuteSet(eventSpriteRendererEventManagerExecute_);
		hpGaugePartsEventManager_.HpGaugesPartsExecuteSet(hpGaugePartsEventManagerExecute_);
		eventTextEventManager_.EventTextsExecuteSet(eventTextEventManagerExecute_);
		statusInfoPartsEventManager_.EventStatusInfosPartsExecuteSet(statusInfoPartsEventManagerExecute_);

		sceneEvent_.func_add(AllUpdateEventExecuteEvent);

		updateGameObjectEventManagerExecute_ = UpdateGameObjectEventManagerExecute.None;
		eventSpriteRendererEventManagerExecute_ = EventSpriteRendererEventManagerExecute.None;
		hpGaugePartsEventManagerExecute_ = HpGaugePartsEventManagerExecute.None;
		eventTextEventManagerExecute_ = EventTextEventManagerExecute.None;
		statusInfoPartsEventManagerExecute_ = StatusInfoPartsEventManagerExecute.None;
	}
	public void SceneChangeEventSet(SceneState sceneState, SceneChangeMode sceneChangeMode) {
		sceneStates_.Add(sceneState);
		sceneChangeModes_.Add(sceneChangeMode);

		sceneEvent_.func_add(SceneChangeEvent);
	}
	static private bool WaitEvent(AllEventManager mgr) {
		if (mgr.sceneCounter_.measure(Time.deltaTime, mgr.eventTimeRegulation_[mgr.updateEventExecuteCounter_])) {
			mgr.updateEventExecuteCounter_ += 1;

			return true;
		}

		return false;
	}
	static private bool WaitEnterSelectEvent(AllEventManager mgr) {
		if (mgr.inputProvider_.SelectEnter()) {
			return true;
		}

		return false;
	}
	static private bool EventFinishEvent(AllEventManager mgr) {
		mgr.updateEventExecuteCounter_ = 0;
		mgr.eventTimeRegulation_.Clear();
		mgr.eventTimeFluctProcesses_.Clear();

		mgr.eventActiveExecuteCounter_ = 0;
		mgr.eventActive_.Clear();

		mgr.eventSceneChangeExecuteCounter_ = 0;
		mgr.sceneStates_.Clear();
		mgr.sceneChangeModes_.Clear();

		mgr.eventSpriteEventManager_.EventSpriteRenderersClear();

		mgr.updateGameObjectEventManager_.UpdateGameObjectsClear();

		mgr.eventTextEventManager_.EventTextsClear();

		mgr.hpGaugePartsEventManager_.HpGaugesPartsClear();

		mgr.statusInfoPartsEventManager_.EventStatusInfosPartsClear();

		return mgr.sceneEvent_.event_finish();
	}
	static private bool AllUpdateEventExecuteEvent(AllEventManager mgr) {
		mgr.updateGameObjectEventManager_.UpdateGameObjectsUpdateExecute(mgr.eventTimeRegulation_[mgr.updateEventExecuteCounter_], mgr.eventTimeFluctProcesses_[mgr.updateEventExecuteCounter_]);
		mgr.eventSpriteEventManager_.EventSpriteRenderersUpdateExecute(mgr.eventTimeRegulation_[mgr.updateEventExecuteCounter_], mgr.eventTimeFluctProcesses_[mgr.updateEventExecuteCounter_]);
		mgr.hpGaugePartsEventManager_.HpGaugesPartsUpdateExecute(mgr.eventTimeRegulation_[mgr.updateEventExecuteCounter_], mgr.eventTimeFluctProcesses_[mgr.updateEventExecuteCounter_]);
		mgr.eventTextEventManager_.EventTextsUpdateExecute(mgr.eventTimeRegulation_[mgr.updateEventExecuteCounter_], mgr.eventTimeFluctProcesses_[mgr.updateEventExecuteCounter_]);
		mgr.statusInfoPartsEventManager_.EventStatusInfosPartsUpdateExecute(mgr.eventTimeRegulation_[mgr.updateEventExecuteCounter_], mgr.eventTimeFluctProcesses_[mgr.updateEventExecuteCounter_]);

		mgr.eventTimeRegulation_[mgr.updateEventExecuteCounter_] -= Time.deltaTime;

		mgr.sceneEvent_.func_insert(WaitEvent, mgr.sceneEvent_.funcs_num() + 1);

		return true;
	}
	static private bool SceneChangeEvent(AllEventManager mgr) {
		AllSceneManager.GetInstance().SceneChange(mgr.sceneStates_[mgr.eventSceneChangeExecuteCounter_], mgr.sceneChangeModes_[mgr.eventSceneChangeExecuteCounter_]);

		mgr.eventSceneChangeExecuteCounter_ += 1;

		return true;
	}

	//EventSpriteRenderer
	public void EventSpriteRendererSet(EventSpriteRenderer eventSprite, List<Sprite> sprites = null, Color32 color = new Color32()) {
		eventSpriteEventManager_.EventSpriteRendererSet(eventSprite, sprites, color);
	}
	public void EventSpriteRenderersUpdateExecuteSet(EventSpriteRendererEventManagerExecute setExecute) {
		eventSpriteRendererEventManagerExecute_ = setExecute;
	}

	//UpdateGameObject
	public void UpdateGameObjectSet(UpdateGameObject updateGameObject, Vector3 endVec3 = new Vector3()) {
		updateGameObjectEventManager_.UpdateGameObjectSet(updateGameObject, endVec3);
	}
	public void UpdateGameObjectUpdateExecuteSet(UpdateGameObjectEventManagerExecute setExecute) {
		updateGameObjectEventManagerExecute_ = setExecute;
	}
	public void UpdateGameObjectsActiveSetExecute(bool setActive) {
		updateGameObjectEventManager_.UpdateGameObjectsExecuteSet();

		sceneEvent_.func_add(UpdateGameObjectsActiveSetExecuteEvent);

		eventActive_.Add(setActive);
	}
	static private bool UpdateGameObjectsActiveSetExecuteEvent(AllEventManager mgr) {
		mgr.updateGameObjectEventManager_.UpdateGameObjectsActiveSetExecute(mgr.eventActive_[mgr.eventActiveExecuteCounter_]);

		mgr.eventActiveExecuteCounter_ += 1;

		return true;
	}

	//EventText
	public void EventTextSet(EventText eventText, string setStr) {
		eventTextEventManager_.EventTextSet(eventText, setStr);
	}
	public void EventTextsUpdateExecuteSet(EventTextEventManagerExecute setExecute) {
		eventTextEventManagerExecute_ = setExecute;
	}

	//HpGaugeParts
	public void HpGaugePartsSet(HpGaugeParts setEventHpGauge, float endFillAmount = 0, IMonsterData setMonsterData = null) {
		hpGaugePartsEventManager_.HpGaugePartsSet(setEventHpGauge, setMonsterData, endFillAmount);
	}
	public void HpGaugePartsUpdateExecuteSet(HpGaugePartsEventManagerExecute setExecute) {
		hpGaugePartsEventManagerExecute_ = setExecute;
	}

	//StatusInfoParts
	public void EventStatusInfoPartsSet(StatusInfoParts eventStatusInfoParts, Color32 setColor = new Color32()) {
		statusInfoPartsEventManager_.EventStatusInfoPartsSet(eventStatusInfoParts, setColor);
	}
	public void StatusInfoPartsUpdateExecuteSet(StatusInfoPartsEventManagerExecute setExecute) {
		statusInfoPartsEventManagerExecute_ = setExecute;
	}

	//シングルトン
	public AllEventManager() {
		//イベントの初期化
		sceneEvent_ = new t13.Event<AllEventManager>(this);
	}

	static private AllEventManager instance_ = null;
	static public AllEventManager GetInstance() {
		if (instance_ != null) return instance_;

		instance_ = new AllEventManager();
		return instance_;
	}
	static public void ReleaseInstance() { instance_ = null; }
}
