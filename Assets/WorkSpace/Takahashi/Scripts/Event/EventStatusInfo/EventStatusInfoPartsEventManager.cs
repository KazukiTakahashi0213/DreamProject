using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventStatusInfoPartsEventManager {
	private int eventStatusInfosPartsExecuteCounter_ = 0;
	private List<EventStatusInfoParts> eventStatusInfosParts_ = new List<EventStatusInfoParts>();
	private List<Color32> endColors_ = new List<Color32>();
	private List<List<EventStatusInfoParts>> executeEventStatusInfosParts_ = new List<List<EventStatusInfoParts>>();
	private List<List<Color32>> executeEndColors_ = new List<List<Color32>>();

	public void EventStatusInfoPartsSet(EventStatusInfoParts eventStatusInfoParts, Color32 endColor) {
		eventStatusInfosParts_.Add(eventStatusInfoParts);
		endColors_.Add(endColor);
	}
	public void EventStatusInfosPartsExecuteSet() {
		List<EventStatusInfoParts> addEventStatusInfoParts = new List<EventStatusInfoParts>();
		List<Color32> addEndColor = new List<Color32>();

		for (int i = 0; i < eventStatusInfosParts_.Count; ++i) {
			addEventStatusInfoParts.Add(eventStatusInfosParts_[i]);
			addEndColor.Add(endColors_[i]);
		}

		executeEventStatusInfosParts_.Add(addEventStatusInfoParts);
		executeEndColors_.Add(addEndColor);

		eventStatusInfosParts_.Clear();
		endColors_.Clear();
	}

	public void EventStatusInfosPartsUpdateExecute(float timeRegulation = 0) {
		for (int i = 0; i < executeEventStatusInfosParts_[eventStatusInfosPartsExecuteCounter_].Count; ++i) {
			executeEventStatusInfosParts_[eventStatusInfosPartsExecuteCounter_][i].ProcessStateColorUpdateExecute(
				timeRegulation,
				executeEndColors_[eventStatusInfosPartsExecuteCounter_][i]
				);
		}

		eventStatusInfosPartsExecuteCounter_ += 1;
	}

	public void EventStatusInfosPartsClear() {
		eventStatusInfosParts_.Clear();
		endColors_.Clear();
		executeEventStatusInfosParts_.Clear();
		executeEndColors_.Clear();

		eventStatusInfosPartsExecuteCounter_ = 0;
	}
}
