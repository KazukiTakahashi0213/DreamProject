using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventSpriteRendererEventManagerExecute {
	None
	, Anime
	, Max
}

public class EventSpriteRendererEventManagerExecuteState {
	public EventSpriteRendererEventManagerExecuteState(EventSpriteRendererEventManagerExecute setState) {
		state_ = setState;
	}

	public EventSpriteRendererEventManagerExecute state_;

	//None
	static private void NoneExecute(EventSpriteRendererEventManagerExecuteState mine, EventSpriteRendererEventManager eventSpriteRendererEventManager, float timeRegulation) {

	}

	//Anime
	static private void AnimeExecute(EventSpriteRendererEventManagerExecuteState mine, EventSpriteRendererEventManager eventSpriteRendererEventManager, float timeRegulation) {
		for (int i = 0; i < eventSpriteRendererEventManager.GetExecuteEventSpriteRenderersCount(); ++i) {
			eventSpriteRendererEventManager.GetExecuteEventSpriteRenderers(i).ProcessStateAnimeExecute(
				timeRegulation / eventSpriteRendererEventManager.GetExecuteAnimeSprites(i).Count
				, eventSpriteRendererEventManager.GetExecuteAnimeSprites(i)
				);
		}
	}

	private delegate void ExecuteFunc(EventSpriteRendererEventManagerExecuteState mine, EventSpriteRendererEventManager eventSpriteRendererEventManager, float timeRegulation);

	private ExecuteFunc[] executeFuncs_ = new ExecuteFunc[(int)EventSpriteRendererEventManagerExecute.Max] {
		NoneExecute
		, AnimeExecute
	};
	public void Execute(EventSpriteRendererEventManager eventSpriteRendererEventManager, float timeRegulation) { executeFuncs_[(int)state_](this, eventSpriteRendererEventManager, timeRegulation); }
}
