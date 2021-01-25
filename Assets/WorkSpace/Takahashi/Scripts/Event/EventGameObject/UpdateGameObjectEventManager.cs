using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateGameObjectEventManager {
	private int eventGameObjectsExecuteCounter_ = 0;
	private List<UpdateGameObject> eventGameObjects_ = new List<UpdateGameObject>();
	private List<float> eventGameObjectsEndValue_ = new List<float>();
	private List<List<UpdateGameObject>> executeEvenetGameObjects_ = new List<List<UpdateGameObject>>();
	private List<List<float>> executeEventGameObjectsEndValue_ = new List<List<float>>();

	public void EventGameObjectSet(UpdateGameObject eventGameObject, float endValue = 0) {
		eventGameObjects_.Add(eventGameObject);
		eventGameObjectsEndValue_.Add(endValue);
	}
	public void EventGameObjectsExecuteSet() {
		List<UpdateGameObject> addGameObjects = new List<UpdateGameObject>();
		List<float> addFloats = new List<float>();

		for (int i = 0; i < eventGameObjects_.Count; ++i) {
			addGameObjects.Add(eventGameObjects_[i]);
			addFloats.Add(eventGameObjectsEndValue_[i]);
		}

		executeEvenetGameObjects_.Add(addGameObjects);
		executeEventGameObjectsEndValue_.Add(addFloats);

		eventGameObjects_.Clear();
		eventGameObjectsEndValue_.Clear();
	}

	public void EventGameObjectsActiveSetExecute(bool setActive) {
		for (int i = 0; i < executeEvenetGameObjects_[eventGameObjectsExecuteCounter_].Count; ++i) {
			executeEvenetGameObjects_[eventGameObjectsExecuteCounter_][i].GetGameObject().SetActive(setActive);
		}

		eventGameObjectsExecuteCounter_ += 1;
	}
	public void EventGameObjectsPosMoveXExecute(float timeRegulation = 0) {
		for(int i = 0;i < executeEvenetGameObjects_[eventGameObjectsExecuteCounter_].Count; ++i) {
			executeEvenetGameObjects_[eventGameObjectsExecuteCounter_][i].ProcessStatePosMoveXExecute(
				timeRegulation, 
				executeEventGameObjectsEndValue_[eventGameObjectsExecuteCounter_][i]
				);
		}

		eventGameObjectsExecuteCounter_ += 1;
	}
	public void EventGameObjectsPosMoveYExecute(float timeRegulation = 0) {
		for (int i = 0; i < executeEvenetGameObjects_[eventGameObjectsExecuteCounter_].Count; ++i) {
			executeEvenetGameObjects_[eventGameObjectsExecuteCounter_][i].ProcessStatePosMoveYExecute(
				timeRegulation,
				executeEventGameObjectsEndValue_[eventGameObjectsExecuteCounter_][i]
				);
		}

		eventGameObjectsExecuteCounter_ += 1;
	}
	public void EventGameObjectsRotMoveExecute(float timeRegulation = 0) {
		for (int i = 0; i < executeEvenetGameObjects_[eventGameObjectsExecuteCounter_].Count; ++i) {
			executeEvenetGameObjects_[eventGameObjectsExecuteCounter_][i].ProcessStateRotMoveExecute(
				timeRegulation,
				executeEventGameObjectsEndValue_[eventGameObjectsExecuteCounter_][i]
				);
		}

		eventGameObjectsExecuteCounter_ += 1;
	}

	public void EventGameObjectsClear() {
		eventGameObjects_.Clear();
		eventGameObjectsEndValue_.Clear();
		executeEvenetGameObjects_.Clear();
		executeEventGameObjectsEndValue_.Clear();

		eventGameObjectsExecuteCounter_ = 0;
	}
}
