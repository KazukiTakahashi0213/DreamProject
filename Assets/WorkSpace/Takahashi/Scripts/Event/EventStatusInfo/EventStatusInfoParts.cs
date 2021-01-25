﻿using System.Collections;
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

	private t13.Time_fluct[] timeFlucts_ = new t13.Time_fluct[6] {
		new t13.Time_fluct()
		, new t13.Time_fluct()
		, new t13.Time_fluct()
		, new t13.Time_fluct()
		, new t13.Time_fluct()
		, new t13.Time_fluct()
	};
	private t13.Time_counter timeCounter_ = new t13.Time_counter();

	private float timeRegulation_ = 0;
	private Color32 endColor_ = new Color32();

	[SerializeField] private StatusInfoParts statusInfoParts_ = null;

	public t13.Time_fluct GetTimeFlucts(int number) { return timeFlucts_[number]; }
	public t13.Time_counter GetTimeCounter() { return timeCounter_; }

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
