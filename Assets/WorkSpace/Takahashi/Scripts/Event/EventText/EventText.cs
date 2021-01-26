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

	private t13.TimeFluct timeFluct_ = new t13.TimeFluct();
	private t13.TimeCounter timeCounter_ = new t13.TimeCounter();

	private float timeRegulation_ = 0;
	private string updateContext_ = "";

	[SerializeField] private Text text_ = null;

	public t13.TimeFluct GetTimeFluct() { return timeFluct_; }
	public t13.TimeCounter GetTimeCounter() { return timeCounter_; }

	public float GetTimeRegulation() { return timeRegulation_; }
	public string GetUpdateContext() { return updateContext_; }

	public Text GetText() { return text_; }

	public void ProcessStateCharaUpdateExecute(float timeRegulation, string updateContext) {
		timeRegulation_ = timeRegulation;
		updateContext_ = updateContext;

		processState_.state_ = EventTextProcess.CharaUpdate;
	}
}
