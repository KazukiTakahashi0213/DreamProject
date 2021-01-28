using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventTextEventManagerExecute {
	None
	, CharaUpdate
	, Max
}

public class EventTextEventManagerExecuteState {
	public EventTextEventManagerExecuteState(EventTextEventManagerExecute setState) {
		state_ = setState;
	}

	public EventTextEventManagerExecute state_;

	//None
	static private void NoneExecute(EventTextEventManagerExecuteState mine, EventTextEventManager eventTextEventManager, float timeRegulation, t13.TimeFluctProcess timeFluctProcess) {

	}

	//CharaUpdate
	static private void CharaUpdateExecute(EventTextEventManagerExecuteState mine, EventTextEventManager eventTextEventManager, float timeRegulation, t13.TimeFluctProcess timeFluctProcess) {
		for (int i = 0; i < eventTextEventManager.GetExecuteEventTexts().Count; ++i) {
			eventTextEventManager.GetExecuteEventTexts()[i].ProcessStateCharaUpdateExecute(
				timeRegulation
				, eventTextEventManager.GetExecuteUpdateContexts()[i]
				);
		}
	}

	private delegate void ExecuteFunc(EventTextEventManagerExecuteState mine, EventTextEventManager eventTextEventManager, float timeRegulation, t13.TimeFluctProcess timeFluctProcess);

	private ExecuteFunc[] executeFuncs_ = new ExecuteFunc[(int)EventTextEventManagerExecute.Max] {
		NoneExecute
		, CharaUpdateExecute
	};
	public void Execute(EventTextEventManager eventTextEventManager, float timeRegulation, t13.TimeFluctProcess timeFluctProcess) { executeFuncs_[(int)state_](this, eventTextEventManager, timeRegulation, timeFluctProcess); }
}
