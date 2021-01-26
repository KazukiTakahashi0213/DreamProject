using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventSpriteRendererProcess {
	None
	, Anime
	, Max
}

public class EventSpriteRendererProcessState {
	public EventSpriteRendererProcessState(EventSpriteRendererProcess setState) {
		state_ = setState;
	}

	public EventSpriteRendererProcess state_;

	//None
	static private EventSpriteRendererProcess NoneUpdate(EventSpriteRendererProcessState mine, EventSpriteRenderer eventSpriteRenderer) {
		return mine.state_;
	}

	//Anime
	static private EventSpriteRendererProcess AnimeUpdate(EventSpriteRendererProcessState mine, EventSpriteRenderer eventSpriteRenderer) {
		if (eventSpriteRenderer.GetTimeCounter().measure(Time.deltaTime, eventSpriteRenderer.GetTimeRegulation())) {
			eventSpriteRenderer.SetNowAnimeSpriteNumber(eventSpriteRenderer.GetNowAnimeSpriteNumber() + 1);

			if (eventSpriteRenderer.GetNowAnimeSpriteNumber() >= eventSpriteRenderer.GetAnimeSprites().Count) {
				eventSpriteRenderer.SetNowAnimeSpriteNumber(0);
				eventSpriteRenderer.GetSpriteRenderer().sprite = null;

				return EventSpriteRendererProcess.None;
			}

			eventSpriteRenderer.GetSpriteRenderer().sprite = eventSpriteRenderer.GetAnimeSprites()[eventSpriteRenderer.GetNowAnimeSpriteNumber()];

			return mine.state_;
		}

		return mine.state_;
	}

	private delegate EventSpriteRendererProcess UpdateFunc(EventSpriteRendererProcessState mine, EventSpriteRenderer eventSpriteRenderer);

	private UpdateFunc[] updateFuncs_ = new UpdateFunc[(int)EventSpriteRendererProcess.Max] {
		NoneUpdate
		, AnimeUpdate
	};
	public EventSpriteRendererProcess Update(EventSpriteRenderer eventSpriteRenderer) { return updateFuncs_[(int)state_](this, eventSpriteRenderer); }
}
