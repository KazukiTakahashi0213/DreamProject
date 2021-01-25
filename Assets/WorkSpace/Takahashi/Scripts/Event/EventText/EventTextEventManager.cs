using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTextEventManager {
	private int eventTextsExecuteCounter_ = 0;
	private List<EventText> eventTexts_ = new List<EventText>();
	private List<string> updateContexts_ = new List<string>();
	private List<List<EventText>> executeEventTexts_ = new List<List<EventText>>();
	private List<List<string>> executeUpdateContexts_ = new List<List<string>>();

	public void EventTextSet(EventText eventText, string updateContext) {
		eventTexts_.Add(eventText);
		updateContexts_.Add(updateContext);
	}
	public void EventTextsExecuteSet() {
		List<EventText> addEventTexts = new List<EventText>();
		List<string> addUpdateContexts = new List<string>();

		for (int i = 0; i < eventTexts_.Count; ++i) {
			addEventTexts.Add(eventTexts_[i]);
			addUpdateContexts.Add(updateContexts_[i]);
		}

		executeEventTexts_.Add(addEventTexts);
		executeUpdateContexts_.Add(addUpdateContexts);

		eventTexts_.Clear();
		updateContexts_.Clear();
	}

	public void EventTextsUpdateExecute(float timeRegulation = 0) {
		for (int i = 0; i < executeEventTexts_[eventTextsExecuteCounter_].Count; ++i) {
			executeEventTexts_[eventTextsExecuteCounter_][i].ProcessStateCharaUpdateExecute(
				timeRegulation,
				executeUpdateContexts_[eventTextsExecuteCounter_][i]
				);
		}

		eventTextsExecuteCounter_ += 1;
	}

	public void EventTextsClear() {
		eventTexts_.Clear();
		updateContexts_.Clear();
		executeEventTexts_.Clear();
		executeUpdateContexts_.Clear();

		eventTextsExecuteCounter_ = 0;
	}
}
