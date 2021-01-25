using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSpriteRendererProcessAnime : IEventSpriteRendererProcessState {
	public IEventSpriteRendererProcessState Update(EventSpriteRenderer eventSpriteRenderers) {
		if(eventSpriteRenderers.GetTimeCounter().measure(Time.deltaTime, eventSpriteRenderers.GetTimeRegulation())) {
			eventSpriteRenderers.SetNowAnimeSpriteNumber(eventSpriteRenderers.GetNowAnimeSpriteNumber() + 1);

			if (eventSpriteRenderers.GetNowAnimeSpriteNumber() >= eventSpriteRenderers.GetAnimeSprites().Count) {
				eventSpriteRenderers.SetNowAnimeSpriteNumber(0);
				eventSpriteRenderers.GetSpriteRenderer().sprite = null;

				return new EventSpriteRendererProcessNone();
			}

			eventSpriteRenderers.GetSpriteRenderer().sprite = eventSpriteRenderers.GetAnimeSprites()[eventSpriteRenderers.GetNowAnimeSpriteNumber()];

			return this;
		}

		return this;
	}
}
