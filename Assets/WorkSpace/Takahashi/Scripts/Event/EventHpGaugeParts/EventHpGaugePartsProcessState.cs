using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EventHpGaugePartsProcess {
    None,
    GaugeUpdate,
    Max
}

public class EventHpGaugePartsProcessState {
	public EventHpGaugePartsProcessState(EventHpGaugePartsProcess setState) {
		state_ = setState;
	}

    public EventHpGaugePartsProcess state_;

	//None
	static private EventHpGaugePartsProcess NoneUpdate(EventHpGaugePartsProcessState mine, EventHpGaugeParts eventHpGaugeParts) {
		return mine.state_;
	}

	//GaugeUpdate
	static private EventHpGaugePartsProcess GaugeUpdateUpdate(EventHpGaugePartsProcessState mine, EventHpGaugeParts eventHpGaugeParts) {
		if (eventHpGaugeParts.GetTimeCounter().measure(Time.deltaTime, eventHpGaugeParts.GetTimeRegulation())) {
			//イメージの拡大縮小の処理
			t13.UnityUtil.ImageInFluctUpdate(
				eventHpGaugeParts.GetHpGaugeParts().GetGauge(),
				eventHpGaugeParts.GetTimeFluct(),
				eventHpGaugeParts.GetEndFillAmount(),
				eventHpGaugeParts.GetTimeRegulation(),
				eventHpGaugeParts.GetTimeRegulation()
				);

			//InfoTextの変更
			if (eventHpGaugeParts.GetReferMonsterData() != null) {
				Text infoText = eventHpGaugeParts.GetHpGaugeParts().GetInfoText();
				IMonsterData monsterData = eventHpGaugeParts.GetReferMonsterData();

				int result = (int)t13.Utility.ValueForPercentage(
					1,
					eventHpGaugeParts.GetHpGaugeParts().GetGauge().fillAmount,
					monsterData.RealHitPoint()
					);

				infoText.text = t13.Utility.HarfSizeForFullSize(result.ToString()) + "／" + t13.Utility.HarfSizeForFullSize(monsterData.RealHitPoint().ToString());
			}

			//緑 51,238,85,255
			//黄 238,209,51
			//赤 238,51,53
			//Gaugeの色の変更
			if (eventHpGaugeParts.GetHpGaugeParts().GetGauge().fillAmount < 0.21f) {
				eventHpGaugeParts.GetHpGaugeParts().GetGauge().color = new Color32(238, 51, 53, (byte)(eventHpGaugeParts.GetHpGaugeParts().GetGauge().color.a * 255));
			}
			else if (eventHpGaugeParts.GetHpGaugeParts().GetGauge().fillAmount < 0.52f) {
				eventHpGaugeParts.GetHpGaugeParts().GetGauge().color = new Color32(238, 209, 51, (byte)(eventHpGaugeParts.GetHpGaugeParts().GetGauge().color.a * 255));
			}
			else if (eventHpGaugeParts.GetHpGaugeParts().GetGauge().fillAmount < 1.1f) {
				eventHpGaugeParts.GetHpGaugeParts().GetGauge().color = new Color32(51, 238, 85, (byte)(eventHpGaugeParts.GetHpGaugeParts().GetGauge().color.a * 255));
			}

			return EventHpGaugePartsProcess.None;
		}
		else {
			//イメージの拡大縮小の処理
			t13.UnityUtil.ImageInFluctUpdate(
				eventHpGaugeParts.GetHpGaugeParts().GetGauge(),
				eventHpGaugeParts.GetTimeFluct(),
				eventHpGaugeParts.GetEndFillAmount(),
				eventHpGaugeParts.GetTimeCounter().count(),
				eventHpGaugeParts.GetTimeRegulation()
				) ;

			//InfoTextの変更
			if (eventHpGaugeParts.GetReferMonsterData() != null) {
				Text infoText = eventHpGaugeParts.GetHpGaugeParts().GetInfoText();
				IMonsterData monsterData = eventHpGaugeParts.GetReferMonsterData();

				int result = (int)t13.Utility.ValueForPercentage(
					1,
					eventHpGaugeParts.GetHpGaugeParts().GetGauge().fillAmount,
					monsterData.RealHitPoint()
					);

				infoText.text = t13.Utility.HarfSizeForFullSize(result.ToString()) + "／" + t13.Utility.HarfSizeForFullSize(monsterData.RealHitPoint().ToString());
			}

			//緑 51,238,85,255
			//黄 238,209,51
			//赤 238,51,53
			//Gaugeの色の変更
			if (eventHpGaugeParts.GetHpGaugeParts().GetGauge().fillAmount < 0.21f) {
				eventHpGaugeParts.GetHpGaugeParts().GetGauge().color = new Color32(238, 51, 53, (byte)(eventHpGaugeParts.GetHpGaugeParts().GetGauge().color.a * 255));
			}
			else if (eventHpGaugeParts.GetHpGaugeParts().GetGauge().fillAmount < 0.52f) {
				eventHpGaugeParts.GetHpGaugeParts().GetGauge().color = new Color32(238, 209, 51, (byte)(eventHpGaugeParts.GetHpGaugeParts().GetGauge().color.a * 255));
			}
			else if (eventHpGaugeParts.GetHpGaugeParts().GetGauge().fillAmount < 1.1f) {
				eventHpGaugeParts.GetHpGaugeParts().GetGauge().color = new Color32(51, 238, 85, (byte)(eventHpGaugeParts.GetHpGaugeParts().GetGauge().color.a * 255));
			}
		}

		return mine.state_;
	}

	private delegate EventHpGaugePartsProcess UpdateFunc(EventHpGaugePartsProcessState mine, EventHpGaugeParts eventHpGaugeParts);
	private UpdateFunc[] updateFuncs_ = new UpdateFunc[(int)EventHpGaugePartsProcess.Max] {
		NoneUpdate,
		GaugeUpdateUpdate
	};
	public EventHpGaugePartsProcess Update(EventHpGaugeParts eventHpGaugeParts) { return updateFuncs_[(int)state_](this, eventHpGaugeParts); }
}
