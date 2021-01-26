using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventStatusInfoParts : MonoBehaviour {
	//EntryPoint
	void Update() {
		//メイン処理
		processState_.state_ = processState_.Update(this);
	}

	private EventStatusInfoPartsProcessState processState_ = new EventStatusInfoPartsProcessState(EventStatusInfoPartsProcess.None);

	private t13.TimeFluct[] timeFlucts_ = new t13.TimeFluct[6] {
		new t13.TimeFluct()
		, new t13.TimeFluct()
		, new t13.TimeFluct()
		, new t13.TimeFluct()
		, new t13.TimeFluct()
		, new t13.TimeFluct()
	};
	private t13.TimeCounter timeCounter_ = new t13.TimeCounter();

	private float timeRegulation_ = 0;
	private Color32 endColor_ = new Color32();

	[SerializeField] private StatusInfoParts statusInfoParts_ = null;

	public t13.TimeFluct GetTimeFlucts(int number) { return timeFlucts_[number]; }
	public t13.TimeCounter GetTimeCounter() { return timeCounter_; }

	public float GetTimeRegulation() { return timeRegulation_; }
	public Color32 GetEndColor() { return endColor_; }

	public StatusInfoParts GetStatusInfoParts() { return statusInfoParts_; }

	public void ProcessStateColorUpdateExecute(float timeRegulation, Color32 endColor) {
		timeRegulation_ = timeRegulation;
		endColor_ = endColor;

		processState_.state_ = EventStatusInfoPartsProcess.ColorUpdate;
	}
	public void ProcessStateAllColorUpdateExecute(float timeRegulation, Color32 endColor) {
		timeRegulation_ = timeRegulation;
		endColor_ = endColor;

		processState_.state_ = EventStatusInfoPartsProcess.AllColorUpdate;
	}
}
