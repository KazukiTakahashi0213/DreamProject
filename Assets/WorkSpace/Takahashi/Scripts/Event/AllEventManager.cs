using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllEventManager {
	//依存性注入
	private IInputProvider inputProvider_ = new KeyBoardNormalInputProvider();

	private t13.Time_counter sceneCounter_ = new t13.Time_counter();
	private List<t13.Time_fluct> sceneFlucts_ = new List<t13.Time_fluct>();
	private t13.Event<AllEventManager> sceneEvent_;

	public t13.Time_counter GetSceneCounter() { return sceneCounter_; }
	public List<t13.Time_fluct> GetSceneFlucts() { return sceneFlucts_; }
	public bool EventUpdate() { return sceneEvent_.update(); }

	//eventの管理メンバ
	private EventSpriteRendererEventManager eventSpriteEventManager_ = new EventSpriteRendererEventManager();
	private UpdateGameObjectEventManager eventGameObjectEventManager_ = new UpdateGameObjectEventManager();
	private EventTextEventManager eventTextEventManager_ = new EventTextEventManager();
	private EventHpGaugePartsEventManager eventHpGaugePartsEventManager_ = new EventHpGaugePartsEventManager();
	private EventStatusInfoPartsEventManager eventStatusInfoPartsEventManager_ = new EventStatusInfoPartsEventManager();

	private int eventRegulationExecuteCounter_ = 0;
	private List<float> eventRegulation_ = new List<float>();

	private int eventActiveExecuteCounter_ = 0;
	private List<bool> eventActive_ = new List<bool>();

	//EventManager
	public void EventWaitSet(float timeRegulation) {
		eventRegulation_.Add(timeRegulation);

		sceneEvent_.func_add(WaitEvent);
	}
	public void EventWaitEnterSelectSet() {
		sceneEvent_.func_add(WaitEnterSelectEvent);
	}
	public void EventFinishSet() {
		sceneEvent_.func_add(EventFinishEvent);
	}
	static private bool WaitEvent(AllEventManager mgr) {
		if (mgr.sceneCounter_.measure(Time.deltaTime, mgr.eventRegulation_[mgr.eventRegulationExecuteCounter_])) {
			mgr.eventRegulationExecuteCounter_ += 1;

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
		mgr.eventRegulationExecuteCounter_ = 0;
		mgr.eventRegulation_.Clear();

		mgr.eventActiveExecuteCounter_ = 0;
		mgr.eventActive_.Clear();

		mgr.eventSpriteEventManager_.EventSpriteRenderersClear();

		mgr.eventGameObjectEventManager_.EventGameObjectsClear();

		mgr.eventTextEventManager_.EventTextsClear();

		mgr.eventHpGaugePartsEventManager_.EventHpGaugesPartsClear();

		mgr.eventStatusInfoPartsEventManager_.EventStatusInfosPartsClear();

		return mgr.sceneEvent_.event_finish();
	}

	//EventSpriteRenderer
	public void EventSpriteRendererSet(EventSpriteRenderer eventSprite, List<Sprite> sprites) {
		eventSpriteEventManager_.EventSpriteRendererSet(eventSprite, sprites);
	}
	public void EventSpriteRenderersUpdateExecute(float timeRegulation = 0) {
		eventSpriteEventManager_.EventSpriteRenderersExecuteSet();

		sceneEvent_.func_add(SpriteRenderersUpdateExecuteEvent);

		eventRegulation_.Add(timeRegulation);
	}
	public void EventSpriteRenderersSetSpriteExecute() {
		eventSpriteEventManager_.EventSpriteRenderersExecuteSet();

		sceneEvent_.func_add(SpriteRenderersSetSpriteExecuteEvent);
	}
	static private bool SpriteRenderersUpdateExecuteEvent(AllEventManager mgr) {
		mgr.eventSpriteEventManager_.EventSpriteRenderersUpdateExecute(mgr.eventRegulation_[mgr.eventRegulationExecuteCounter_]);

		mgr.eventRegulation_[mgr.eventRegulationExecuteCounter_] -= Time.deltaTime;

		mgr.sceneEvent_.func_insert(WaitEvent, mgr.sceneEvent_.funcs_num() + 1);

		return true;
	}
	static private bool SpriteRenderersSetSpriteExecuteEvent(AllEventManager mgr) {
		mgr.eventSpriteEventManager_.EventSpriteRenderersSetSpriteExecute();

		return true;
	}

	//EventGameObject
	public void EventGameObjectSet(UpdateGameObject eventGameObject, float endPos = 0) {
		eventGameObjectEventManager_.EventGameObjectSet(eventGameObject, endPos);
	}
	public void EventGameObjectsPosMoveXExecute(float timeRegulation = 0) {
		eventGameObjectEventManager_.EventGameObjectsExecuteSet();

		sceneEvent_.func_add(GameObjectsPosMoveXExecuteEvent);

		eventRegulation_.Add(timeRegulation);
	}
	public void EventGameObjectsPosMoveYExecute(float timeRegulation = 0) {
		eventGameObjectEventManager_.EventGameObjectsExecuteSet();

		sceneEvent_.func_add(GameObjectsPosMoveYExecuteEvent);

		eventRegulation_.Add(timeRegulation);
	}
	public void EventGameObjectsRotMoveExecute(float timeRegulation = 0) {
		eventGameObjectEventManager_.EventGameObjectsExecuteSet();

		sceneEvent_.func_add(GameObjectsRotMoveExecuteEvent);

		eventRegulation_.Add(timeRegulation);
	}
	public void EventGameObjectsActiveSetExecute(bool setActive) {
		eventGameObjectEventManager_.EventGameObjectsExecuteSet();

		sceneEvent_.func_add(GameObjectsActiveSetExecuteEvent);

		eventActive_.Add(setActive);
	}
	static private bool GameObjectsPosMoveXExecuteEvent(AllEventManager mgr) {
		mgr.eventGameObjectEventManager_.EventGameObjectsPosMoveXExecute(mgr.eventRegulation_[mgr.eventRegulationExecuteCounter_]);

		mgr.eventRegulation_[mgr.eventRegulationExecuteCounter_] -= Time.deltaTime;

		mgr.sceneEvent_.func_insert(WaitEvent, mgr.sceneEvent_.funcs_num() + 1);

		return true;
	}
	static private bool GameObjectsPosMoveYExecuteEvent(AllEventManager mgr) {
		mgr.eventGameObjectEventManager_.EventGameObjectsPosMoveYExecute(mgr.eventRegulation_[mgr.eventRegulationExecuteCounter_]);

		mgr.eventRegulation_[mgr.eventRegulationExecuteCounter_] -= Time.deltaTime;

		mgr.sceneEvent_.func_insert(WaitEvent, mgr.sceneEvent_.funcs_num() + 1);

		return true;
	}
	static private bool GameObjectsRotMoveExecuteEvent(AllEventManager mgr) {
		mgr.eventGameObjectEventManager_.EventGameObjectsRotMoveExecute(mgr.eventRegulation_[mgr.eventRegulationExecuteCounter_]);

		mgr.eventRegulation_[mgr.eventRegulationExecuteCounter_] -= Time.deltaTime;

		mgr.sceneEvent_.func_insert(WaitEvent, mgr.sceneEvent_.funcs_num() + 1);

		return true;
	}
	static private bool GameObjectsActiveSetExecuteEvent(AllEventManager mgr) {
		mgr.eventGameObjectEventManager_.EventGameObjectsActiveSetExecute(mgr.eventActive_[mgr.eventActiveExecuteCounter_]);

		mgr.eventActiveExecuteCounter_ += 1;

		return true;
	}

	//EventText
	public void EventTextSet(EventText eventText, string setStr) {
		eventTextEventManager_.EventTextSet(eventText, setStr);
	}
	public void EventTextsUpdateExecute(float timeRegulation = 0) {
		eventTextEventManager_.EventTextsExecuteSet();

		sceneEvent_.func_add(ContextsUpdateExecuteEvent);

		eventRegulation_.Add(timeRegulation);
	}
	static private bool ContextsUpdateExecuteEvent(AllEventManager mgr) {
		mgr.eventTextEventManager_.EventTextsUpdateExecute(mgr.eventRegulation_[mgr.eventRegulationExecuteCounter_]);

		mgr.eventRegulation_[mgr.eventRegulationExecuteCounter_] -= Time.deltaTime;

		mgr.sceneEvent_.func_insert(WaitEvent, mgr.sceneEvent_.funcs_num() + 1);

		return true;
	}

	//EventHpGaugeParts
	public void EventHpGaugeSet(EventHpGaugeParts setEventHpGauge, float endFillAmount = 0, IMonsterData setMonsterData = null) {
		eventHpGaugePartsEventManager_.EventHpGaugePartsSet(setEventHpGauge, setMonsterData, endFillAmount);
	}
	public void EventHpGaugesUpdateExecute(float timeRegulation = 0) {
		eventHpGaugePartsEventManager_.EventHpGaugesPartsExecuteSet();

		sceneEvent_.func_add(HpGaugesUpdateExecuteEvent);

		eventRegulation_.Add(timeRegulation);
	}
	static private bool HpGaugesUpdateExecuteEvent(AllEventManager mgr) {
		mgr.eventHpGaugePartsEventManager_.EventHpGaugesPartsUpdateExecute(mgr.eventRegulation_[mgr.eventRegulationExecuteCounter_]);

		mgr.eventRegulation_[mgr.eventRegulationExecuteCounter_] -= Time.deltaTime;

		mgr.sceneEvent_.func_insert(WaitEvent, mgr.sceneEvent_.funcs_num() + 1);

		return true;
	}

	//EventStatusInfoParts
	public void EventStatusInfoPartsSet(EventStatusInfoParts eventStatusInfoParts, Color32 setColor) {
		eventStatusInfoPartsEventManager_.EventStatusInfoPartsSet(eventStatusInfoParts, setColor);
	}
	public void EventStatusInfosPartsUpdateExecute(float timeRegulation = 0) {
		eventStatusInfoPartsEventManager_.EventStatusInfosPartsExecuteSet();

		sceneEvent_.func_add(ColorUpdateExecuteEvent);

		eventRegulation_.Add(timeRegulation);
	}
	static private bool ColorUpdateExecuteEvent(AllEventManager mgr) {
		mgr.eventStatusInfoPartsEventManager_.EventStatusInfosPartsUpdateExecute(mgr.eventRegulation_[mgr.eventRegulationExecuteCounter_]);

		mgr.eventRegulation_[mgr.eventRegulationExecuteCounter_] -= Time.deltaTime;

		mgr.sceneEvent_.func_insert(WaitEvent, mgr.sceneEvent_.funcs_num() + 1);

		return true;
	}

	//シングルトン
	private AllEventManager() {
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
