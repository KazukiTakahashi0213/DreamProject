using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventMoveMapTrigger {
	None
	, Trigger
	, Touch
	, Max
}

public class EventMoveMapTriggerState {
	public EventMoveMapTriggerState(EventMoveMapTrigger setState) {
		state_ = setState;
	}

	public EventMoveMapTrigger state_;

	//None
	private static bool NoneEventTrigger(EventMoveMapTriggerState mine, PlayerEntryZone playerEntryZone) {
		return false;
	}

	//Trigger
	private static bool TriggerEventTrigger(EventMoveMapTriggerState mine, PlayerEntryZone playerEntryZone) {
		if(playerEntryZone.is_collider
			&& AllSceneManager.GetInstance().inputProvider_.SelectEnter()) {
			return true;
		}

		return false;
	}

	//Touch
	private static bool TouchEventTrigger(EventMoveMapTriggerState mine, PlayerEntryZone playerEntryZone) {
		if (playerEntryZone.is_collider) {
			return true;
		}

		return false;
	}

	private delegate bool EventTriggerFunc(EventMoveMapTriggerState mine, PlayerEntryZone playerEntryZone);

	private EventTriggerFunc[] eventTriggers_ = new EventTriggerFunc[(int)EventMoveMapTrigger.Max] {
		NoneEventTrigger
		, TriggerEventTrigger
		, TouchEventTrigger
	};
	public bool EventTrigger(PlayerEntryZone playerEntryZone) { return eventTriggers_[(int)state_](this, playerEntryZone); }
}
