using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTextEventManager {
	private EventTextEventManagerExecuteState executeState_ = new EventTextEventManagerExecuteState(EventTextEventManagerExecute.None);

	private int eventTextsExecuteCounter_ = 0;
	private List<EventText> eventTexts_ = new List<EventText>();
	private List<string> updateContexts_ = new List<string>();
	private List<List<EventText>> executeEventTexts_ = new List<List<EventText>>();
	private List<List<string>> executeUpdateContexts_ = new List<List<string>>();
	private List<EventTextEventManagerExecute> eventTextEventManagerExecutes_ = new List<EventTextEventManagerExecute>();

	public EventTextEventManagerExecuteState GetExecuteState() { return executeState_; }

	public List<EventText> GetExecuteEventTexts() { return executeEventTexts_[eventTextsExecuteCounter_]; }
	public List<string> GetExecuteUpdateContexts() { return executeUpdateContexts_[eventTextsExecuteCounter_]; }

	public void EventTextSet(EventText eventText, string updateContext) {
		eventTexts_.Add(eventText);
		updateContexts_.Add(updateContext);
	}
	public void EventTextsExecuteSet(EventTextEventManagerExecute setState = EventTextEventManagerExecute.None) {
		List<EventText> addEventTexts = new List<EventText>();
		List<string> addUpdateContexts = new List<string>();

		for (int i = 0; i < eventTexts_.Count; ++i) {
			addEventTexts.Add(eventTexts_[i]);
			addUpdateContexts.Add(updateContexts_[i]);
		}

		executeEventTexts_.Add(addEventTexts);
		executeUpdateContexts_.Add(addUpdateContexts);
		eventTextEventManagerExecutes_.Add(setState);

		eventTexts_.Clear();
		updateContexts_.Clear();
	}

	public void EventTextsUpdateExecute(float timeRegulation, t13.TimeFluctProcess timeFluctProcess) {
		executeState_.state_ = eventTextEventManagerExecutes_[eventTextsExecuteCounter_];

		executeState_.Execute(this, timeRegulation, timeFluctProcess);

		eventTextsExecuteCounter_ += 1;
	}

	public void EventTextsClear() {
		eventTexts_.Clear();
		updateContexts_.Clear();
		executeEventTexts_.Clear();
		executeUpdateContexts_.Clear();
		eventTextEventManagerExecutes_.Clear();

		eventTextsExecuteCounter_ = 0;
	}
}
