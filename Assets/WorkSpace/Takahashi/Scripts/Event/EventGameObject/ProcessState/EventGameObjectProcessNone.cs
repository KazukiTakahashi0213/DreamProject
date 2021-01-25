using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventGameObjectProcessNone : IEventGameObjectProcessState {
	public IEventGameObjectProcessState Update(UpdateGameObject eventObject) {
		return this;
	}
}
