using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventText : MonoBehaviour {
	//EntryPoint
	void Update() {
		//メイン処理
		processState_.state_ = processState_.Update(this);
	}

	private EventTextProcessState processState_ = new EventTextProcessState(EventTextProcess.None);

	private t13.Time_fluct timeFluct_ = new t13.Time_fluct();
	private t13.Time_counter timeCounter_ = new t13.Time_counter();

	private float timeRegulation_ = 0;
	private string updateContext_ = "";

	[SerializeField] private Text text_ = null;

	public t13.Time_fluct GetTimeFluct() { return timeFluct_; }
	public t13.Time_counter GetTimeCounter() { return timeCounter_; }

	public float GetTimeRegulation() { return timeRegulation_; }
	public string GetUpdateContext() { return updateContext_; }

	public Text GetText() { return text_; }

	public void ProcessStateCharaUpdateExecute(float timeRegulation, string updateContext) {
		timeRegulation_ = timeRegulation;
		updateContext_ = updateContext;

		processState_.state_ = EventTextProcess.CharaUpdate;
	}
}
