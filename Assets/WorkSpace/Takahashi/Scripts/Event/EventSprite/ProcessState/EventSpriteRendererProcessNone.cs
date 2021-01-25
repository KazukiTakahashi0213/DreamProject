using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSpriteRendererProcessNone : IEventSpriteRendererProcessState {
	public IEventSpriteRendererProcessState Update(EventSpriteRenderer eventSpriteRenderers) {
		return this;
	}
}
