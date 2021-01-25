using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSpriteRendererEventManager {
	private int eventSpriteRenderersExecuteCounter_ = 0;
	private List<EventSpriteRenderer> eventSpriteRenderers_ = new List<EventSpriteRenderer>();
	private List<List<Sprite>> eventSpriteRenderersAnimeSprites_ = new List<List<Sprite>>();
	private List<List<EventSpriteRenderer>> executeEvenetSpriteRenderers_ = new List<List<EventSpriteRenderer>>();
	private List<List<List<Sprite>>> executeEventSpriteRenderersAnimeSprites_ = new List<List<List<Sprite>>>();

	public void EventSpriteRendererSet(EventSpriteRenderer eventSpriteRenderers, List<Sprite> sprites) {
		eventSpriteRenderers_.Add(eventSpriteRenderers);
		eventSpriteRenderersAnimeSprites_.Add(sprites);
	}
	public void EventSpriteRenderersExecuteSet() {
		List<EventSpriteRenderer> addEventSpriteRenderers = new List<EventSpriteRenderer>();
		List<List<Sprite>> addSprites = new List<List<Sprite>>();

		for (int i = 0; i < eventSpriteRenderers_.Count; ++i) {
			addEventSpriteRenderers.Add(eventSpriteRenderers_[i]);
			addSprites.Add(eventSpriteRenderersAnimeSprites_[i]);
		}

		executeEvenetSpriteRenderers_.Add(addEventSpriteRenderers);
		executeEventSpriteRenderersAnimeSprites_.Add(addSprites);

		eventSpriteRenderers_.Clear();
		eventSpriteRenderersAnimeSprites_.Clear();
	}

	public void EventSpriteRenderersUpdateExecute(float timeRegulation = 0) {
		for (int i = 0; i < executeEvenetSpriteRenderers_[eventSpriteRenderersExecuteCounter_].Count; ++i) {
			executeEvenetSpriteRenderers_[eventSpriteRenderersExecuteCounter_][i].ProcessStateAnimeExecute(
				timeRegulation / executeEventSpriteRenderersAnimeSprites_[eventSpriteRenderersExecuteCounter_][i].Count,
				executeEventSpriteRenderersAnimeSprites_[eventSpriteRenderersExecuteCounter_][i]
				);
		}

		eventSpriteRenderersExecuteCounter_ += 1;
	}

	public void EventSpriteRenderersSetSpriteExecute() {
		for (int i = 0; i < executeEvenetSpriteRenderers_[eventSpriteRenderersExecuteCounter_].Count; ++i) {
			executeEvenetSpriteRenderers_[eventSpriteRenderersExecuteCounter_][i].SetSpriteExecute(
				executeEventSpriteRenderersAnimeSprites_[eventSpriteRenderersExecuteCounter_][i][0]
				);
		}

		eventSpriteRenderersExecuteCounter_ += 1;
	}

	public void EventSpriteRenderersClear() {
		eventSpriteRenderers_.Clear();
		eventSpriteRenderersAnimeSprites_.Clear();
		executeEvenetSpriteRenderers_.Clear();
		executeEventSpriteRenderersAnimeSprites_.Clear();

		eventSpriteRenderersExecuteCounter_ = 0;
	}
}
