using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHpGaugeParts : MonoBehaviour {
	//EntryPoint
	void Update() {
		//メイン処理
		processState_.state_ = processState_.Update(this);
	}

	private EventHpGaugePartsProcessState processState_ = new EventHpGaugePartsProcessState(EventHpGaugePartsProcess.None);

	private t13.TimeFluct timeFluct_ = new t13.TimeFluct();
	private t13.TimeCounter timeCounter_ = new t13.TimeCounter();

	private float timeRegulation_ = 0;
	private IMonsterData referMonsterData_ = null;
	private float endFillAmount_ = 0;

	[SerializeField] private HpGaugeParts hpGaugeParts_ = null;

	public t13.TimeFluct GetTimeFluct() { return timeFluct_; }
	public t13.TimeCounter GetTimeCounter() { return timeCounter_; }

	public float GetTimeRegulation() { return timeRegulation_; }
	public IMonsterData GetReferMonsterData() { return referMonsterData_; }
	public float GetEndFillAmount() { return endFillAmount_; }

	public HpGaugeParts GetHpGaugeParts() { return hpGaugeParts_; }

	public void ProcessStateGaugeUpdateExecute(float timeRegulation, IMonsterData referMonsterData, float endFillAmount) {
		timeRegulation_ = timeRegulation;
		referMonsterData_ = referMonsterData;
		endFillAmount_ = endFillAmount;

		processState_.state_ = EventHpGaugePartsProcess.GaugeUpdate;
	}
}
