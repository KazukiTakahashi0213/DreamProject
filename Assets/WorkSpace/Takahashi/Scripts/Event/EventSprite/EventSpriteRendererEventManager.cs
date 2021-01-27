using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSpriteRendererEventManager {
	private EventSpriteRendererEventManagerExecuteState executeState_ = new EventSpriteRendererEventManagerExecuteState(EventSpriteRendererEventManagerExecute.None);

	private int eventSpriteRenderersExecuteCounter_ = 0;
	private List<EventSpriteRenderer> eventSpriteRenderers_ = new List<EventSpriteRenderer>();
	private List<List<Sprite>> animeSprites_ = new List<List<Sprite>>();
	private List<List<EventSpriteRenderer>> executeEvenetSpriteRenderers_ = new List<List<EventSpriteRenderer>>();
	private List<List<List<Sprite>>> executeAnimeSprites_ = new List<List<List<Sprite>>>();
	private List<EventSpriteRendererEventManagerExecute> eventSpriteRendererEventManagerExecutes_ = new List<EventSpriteRendererEventManagerExecute>();

	public EventSpriteRenderer GetExecuteEventSpriteRenderers(int value) { return executeEvenetSpriteRenderers_[eventSpriteRenderersExecuteCounter_][value]; }
	public int GetExecuteEventSpriteRenderersCount() { return executeEvenetSpriteRenderers_[eventSpriteRenderersExecuteCounter_].Count; }
	public List<Sprite> GetExecuteAnimeSprites(int value) { return executeAnimeSprites_[eventSpriteRenderersExecuteCounter_][value]; }

	public EventSpriteRendererEventManagerExecuteState GetExecuteState() { return executeState_; }

	public void EventSpriteRendererSet(EventSpriteRenderer eventSpriteRenderers, List<Sprite> sprites) {
		eventSpriteRenderers_.Add(eventSpriteRenderers);
		animeSprites_.Add(sprites);
	}
	public void EventSpriteRenderersExecuteSet(EventSpriteRendererEventManagerExecute setExecute = EventSpriteRendererEventManagerExecute.None) {
		List<EventSpriteRenderer> addEventSpriteRenderers = new List<EventSpriteRenderer>();
		List<List<Sprite>> addSprites = new List<List<Sprite>>();

		for (int i = 0; i < eventSpriteRenderers_.Count; ++i) {
			addEventSpriteRenderers.Add(eventSpriteRenderers_[i]);
			addSprites.Add(animeSprites_[i]);
		}

		executeEvenetSpriteRenderers_.Add(addEventSpriteRenderers);
		executeAnimeSprites_.Add(addSprites);
		eventSpriteRendererEventManagerExecutes_.Add(setExecute);

		eventSpriteRenderers_.Clear();
		animeSprites_.Clear();
	}

	public void EventSpriteRenderersUpdateExecute(float timeRegulation, t13.TimeFluctProcess timeFluctProcess) {
		executeState_.state_ = eventSpriteRendererEventManagerExecutes_[eventSpriteRenderersExecuteCounter_];

		executeState_.Execute(this, timeRegulation);

		eventSpriteRenderersExecuteCounter_ += 1;
	}
	public void EventSpriteRenderersSetSpriteExecute() {
		for (int i = 0; i < executeEvenetSpriteRenderers_[eventSpriteRenderersExecuteCounter_].Count; ++i) {
			executeEvenetSpriteRenderers_[eventSpriteRenderersExecuteCounter_][i].GetSpriteRenderer().sprite = executeAnimeSprites_[eventSpriteRenderersExecuteCounter_][i][0];
		}

		eventSpriteRenderersExecuteCounter_ += 1;
	}

	public void EventSpriteRenderersClear() {
		eventSpriteRenderers_.Clear();
		animeSprites_.Clear();
		executeEvenetSpriteRenderers_.Clear();
		executeAnimeSprites_.Clear();
		eventSpriteRendererEventManagerExecutes_.Clear();

		eventSpriteRenderersExecuteCounter_ = 0;
	}
}
