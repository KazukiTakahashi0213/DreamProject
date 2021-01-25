using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventGameObjectProcessPosMoveX : IEventGameObjectProcessState {
	public IEventGameObjectProcessState Update(UpdateGameObject eventObject) {
		if (eventObject.GetTimeCounter().measure(Time.deltaTime, eventObject.GetTimeRegulation())) {
			t13.UnityUtil.ObjectInFluctUpdatePosX(
				eventObject.GetGameObject(),
				eventObject.GetTimeFluct(),
				eventObject.GetEndValue(),
				eventObject.GetTimeRegulation(),
				eventObject.GetTimeRegulation()
				);

			return new EventGameObjectProcessNone();
		}
		else {
			t13.UnityUtil.ObjectInFluctUpdatePosX(
				eventObject.GetGameObject(),
				eventObject.GetTimeFluct(),
				eventObject.GetEndValue(),
				eventObject.GetTimeCounter().count(),
				eventObject.GetTimeRegulation()
				);
		}

		return this;
	}
}
