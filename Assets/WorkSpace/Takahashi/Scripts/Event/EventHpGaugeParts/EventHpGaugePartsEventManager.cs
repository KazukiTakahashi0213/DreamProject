using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHpGaugePartsEventManager {
	private int eventHpGaugesPartsExecuteCounter_ = 0;
	private List<EventHpGaugeParts> eventHpGaugesParts_ = new List<EventHpGaugeParts>();
	private List<IMonsterData> referMonsterDatas_ = new List<IMonsterData>();
	private List<float> endFillAmounts_ = new List<float>();
	private List<List<EventHpGaugeParts>> executeEventHpGaugesParts_ = new List<List<EventHpGaugeParts>>();
	private List<List<IMonsterData>> executeReferMonsterDatas_ = new List<List<IMonsterData>>();
	private List<List<float>> executeEndFillAmounts_ = new List<List<float>>();

	public void EventHpGaugePartsSet(EventHpGaugeParts eventHpGaugeParts, IMonsterData monsterData, float endFillAmount) {
		eventHpGaugesParts_.Add(eventHpGaugeParts);
		referMonsterDatas_.Add(monsterData);
		endFillAmounts_.Add(endFillAmount);
	}
	public void EventHpGaugesPartsExecuteSet() {
		List<EventHpGaugeParts> addEventHpGaugesParts = new List<EventHpGaugeParts>();
		List<IMonsterData> addReferMonsterDatas = new List<IMonsterData>();
		List<float> addEndFillAmounts = new List<float>();

		for (int i = 0; i < eventHpGaugesParts_.Count; ++i) {
			addEventHpGaugesParts.Add(eventHpGaugesParts_[i]);
			addReferMonsterDatas.Add(referMonsterDatas_[i]);
			addEndFillAmounts.Add(endFillAmounts_[i]);
		}

		executeEventHpGaugesParts_.Add(addEventHpGaugesParts);
		executeReferMonsterDatas_.Add(addReferMonsterDatas);
		executeEndFillAmounts_.Add(addEndFillAmounts);

		eventHpGaugesParts_.Clear();
		referMonsterDatas_.Clear();
		endFillAmounts_.Clear();
	}

	public void EventHpGaugesPartsUpdateExecute(float timeRegulation = 0) {
		for (int i = 0; i < executeEventHpGaugesParts_[eventHpGaugesPartsExecuteCounter_].Count; ++i) {
			executeEventHpGaugesParts_[eventHpGaugesPartsExecuteCounter_][i].ProcessStateGaugeUpdateExecute(
				timeRegulation,
				executeReferMonsterDatas_[eventHpGaugesPartsExecuteCounter_][i],
				executeEndFillAmounts_[eventHpGaugesPartsExecuteCounter_][i]
				);
		}

		eventHpGaugesPartsExecuteCounter_ += 1;
	}

	public void EventHpGaugesPartsClear() {
		eventHpGaugesParts_.Clear();
		referMonsterDatas_.Clear();
		endFillAmounts_.Clear();
		executeEventHpGaugesParts_.Clear();
		executeReferMonsterDatas_.Clear();
		executeEndFillAmounts_.Clear();

		eventHpGaugesPartsExecuteCounter_ = 0;
	}
}
