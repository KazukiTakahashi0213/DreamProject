using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateGameObject : MonoBehaviour {
	//EntryPoint
	void Update() {
		//メイン処理
		processState_ = processState_.Update(this);
	}

	private IEventGameObjectProcessState processState_ = new EventGameObjectProcessNone();

	private t13.Time_fluct timeFluct_ = new t13.Time_fluct();
	private t13.Time_counter timeCounter_ = new t13.Time_counter();

	private float timeRegulation_ = 0;
	private float endValue_ = 0;

	public t13.Time_fluct GetTimeFluct() { return timeFluct_; }
	public t13.Time_counter GetTimeCounter() { return timeCounter_; }

	public GameObject GetGameObject() { return gameObject; }

	public float GetTimeRegulation() { return timeRegulation_; }
	public float GetEndValue() { return endValue_; }

	public void ProcessStatePosMoveXExecute(float timeRegulation, float endValue) {
		timeRegulation_ = timeRegulation;
		endValue_ = endValue;

		processState_ = new EventGameObjectProcessPosMoveX();
	}
	public void ProcessStatePosMoveYExecute(float timeRegulation, float endValue) {
		timeRegulation_ = timeRegulation;
		endValue_ = endValue;

		processState_ = new EventGameObjectProcessPosMoveY();
	}
	public void ProcessStatePosMoveZExecute(float timeRegulation, float endValue) {
		timeRegulation_ = timeRegulation;
		endValue_ = endValue;

		processState_ = new EventGameObjectProcessPosMoveZ();
	}
	public void ProcessStateRotMoveExecute(float timeRegulation, float endValue) {
		timeRegulation_ = timeRegulation;
		endValue_ = endValue;

		processState_ = new EventGameObjectProcessRotMove();
	}
}
