using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventMoveMap : ObjectMoveMap {
	void Start() {
		EventInit();

		ObjectType = MapData.MAP_STATUS.EVENT;
	}

	[SerializeField] private EventMoveMapTrigger startTrigger_ = EventMoveMapTrigger.None;

	private EventMoveMapTriggerState triggerState_ = new EventMoveMapTriggerState(EventMoveMapTrigger.None);

	public bool eventActive_ = true;

	public int executeEventNum_ = 0;

	public delegate void EventSetFunc(EventMoveMap eventMoveMap, MapManager mapManager);
	private List<EventSetFunc> eventSetFuncs_ = new List<EventSetFunc>();

	public EventMoveMapTriggerState GetTriggerState() { return triggerState_; }
	public List<EventSetFunc> GetEventSetFuncs() { return eventSetFuncs_; }

	public void EventInit() {
		triggerState_.state_ = startTrigger_;
	}
}
