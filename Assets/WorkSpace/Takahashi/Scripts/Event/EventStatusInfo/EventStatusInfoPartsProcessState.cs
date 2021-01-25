using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventStatusInfoPartsProcess {
	None
	, ColorUpdate
	, AllColorUpdate
	, Max
}

public class EventStatusInfoPartsProcessState {
	public EventStatusInfoPartsProcessState(EventStatusInfoPartsProcess setState) {
		state_ = setState;
	}

	public EventStatusInfoPartsProcess state_;

	//None
	static private EventStatusInfoPartsProcess NoneUpdate(EventStatusInfoPartsProcessState mine, EventStatusInfoParts eventStatusInfoParts) {
		return mine.state_;
	}

	//ColorUpdate
	static private EventStatusInfoPartsProcess ColorUpdateUpdate(EventStatusInfoPartsProcessState mine, EventStatusInfoParts eventStatusInfoParts) {
		if(eventStatusInfoParts.GetTimeCounter().measure(Time.deltaTime, eventStatusInfoParts.GetTimeRegulation())) {
			BaseParts baseParts = eventStatusInfoParts.GetStatusInfoParts().GetBaseParts();
			FrameParts frameParts = eventStatusInfoParts.GetStatusInfoParts().GetFrameParts();

			baseParts.GetBaseSprite().color = t13.UnityUtil.Color32InFluctUpdateAlpha(
				t13.UnityUtil.ColorForColor32(baseParts.GetBaseSprite().color)
				, eventStatusInfoParts.GetTimeFlucts(5)
				, 255
				, eventStatusInfoParts.GetTimeRegulation()
				, eventStatusInfoParts.GetTimeRegulation()
				);
			baseParts.GetHpLogoText().color = t13.UnityUtil.Color32InFluctUpdateAlpha(
				t13.UnityUtil.ColorForColor32(baseParts.GetHpLogoText().color)
				, eventStatusInfoParts.GetTimeFlucts(0)
				, eventStatusInfoParts.GetEndColor().a
				, eventStatusInfoParts.GetTimeRegulation()
				, eventStatusInfoParts.GetTimeRegulation()
				);
			baseParts.GetInfoEventText().GetText().color = t13.UnityUtil.Color32InFluctUpdateAlpha(
				t13.UnityUtil.ColorForColor32(baseParts.GetInfoEventText().GetText().color)
				, eventStatusInfoParts.GetTimeFlucts(1)
				, eventStatusInfoParts.GetEndColor().a
				, eventStatusInfoParts.GetTimeRegulation()
				, eventStatusInfoParts.GetTimeRegulation()
				);

			frameParts.GetFrameSprite().color = t13.UnityUtil.Color32InFluctUpdateAlpha(
				t13.UnityUtil.ColorForColor32(frameParts.GetFrameSprite().color)
				, eventStatusInfoParts.GetTimeFlucts(2)
				, eventStatusInfoParts.GetEndColor().a
				, eventStatusInfoParts.GetTimeRegulation()
				, eventStatusInfoParts.GetTimeRegulation()
				);
			if (frameParts.GetHpGaugeParts().GetInfoText() != null) {
				frameParts.GetHpGaugeParts().GetInfoText().color = t13.UnityUtil.Color32InFluctUpdateAlpha(
					t13.UnityUtil.ColorForColor32(frameParts.GetHpGaugeParts().GetInfoText().color)
					, eventStatusInfoParts.GetTimeFlucts(3)
					, eventStatusInfoParts.GetEndColor().a
					, eventStatusInfoParts.GetTimeRegulation()
					, eventStatusInfoParts.GetTimeRegulation()
					);
			}
			frameParts.GetHpGaugeParts().GetGauge().color = t13.UnityUtil.Color32InFluctUpdateAlpha(
				t13.UnityUtil.ColorForColor32(frameParts.GetHpGaugeParts().GetGauge().color)
				, eventStatusInfoParts.GetTimeFlucts(4)
				, eventStatusInfoParts.GetEndColor().a
				, eventStatusInfoParts.GetTimeRegulation()
				, eventStatusInfoParts.GetTimeRegulation()
				);

			return EventStatusInfoPartsProcess.None;
		}
		else {
			BaseParts baseParts = eventStatusInfoParts.GetStatusInfoParts().GetBaseParts();
			FrameParts frameParts = eventStatusInfoParts.GetStatusInfoParts().GetFrameParts();

			baseParts.GetBaseSprite().color = t13.UnityUtil.Color32InFluctUpdateAlpha(
				t13.UnityUtil.ColorForColor32(baseParts.GetBaseSprite().color)
				, eventStatusInfoParts.GetTimeFlucts(5)
				, 255
				, eventStatusInfoParts.GetTimeCounter().count()
				, eventStatusInfoParts.GetTimeRegulation()
				);
			baseParts.GetHpLogoText().color = t13.UnityUtil.Color32InFluctUpdateAlpha(
				t13.UnityUtil.ColorForColor32(baseParts.GetHpLogoText().color)
				, eventStatusInfoParts.GetTimeFlucts(0)
				, eventStatusInfoParts.GetEndColor().a
				, eventStatusInfoParts.GetTimeCounter().count()
				, eventStatusInfoParts.GetTimeRegulation()
				);
			baseParts.GetInfoEventText().GetText().color = t13.UnityUtil.Color32InFluctUpdateAlpha(
				t13.UnityUtil.ColorForColor32(baseParts.GetInfoEventText().GetText().color)
				, eventStatusInfoParts.GetTimeFlucts(1)
				, eventStatusInfoParts.GetEndColor().a
				, eventStatusInfoParts.GetTimeCounter().count()
				, eventStatusInfoParts.GetTimeRegulation()
				);

			frameParts.GetFrameSprite().color = t13.UnityUtil.Color32InFluctUpdateAlpha(
				t13.UnityUtil.ColorForColor32(frameParts.GetFrameSprite().color)
				, eventStatusInfoParts.GetTimeFlucts(2)
				, eventStatusInfoParts.GetEndColor().a
				, eventStatusInfoParts.GetTimeCounter().count()
				, eventStatusInfoParts.GetTimeRegulation()
				);
			if (frameParts.GetHpGaugeParts().GetInfoText() != null) {
				frameParts.GetHpGaugeParts().GetInfoText().color = t13.UnityUtil.Color32InFluctUpdateAlpha(
					t13.UnityUtil.ColorForColor32(frameParts.GetHpGaugeParts().GetInfoText().color)
					, eventStatusInfoParts.GetTimeFlucts(3)
					, eventStatusInfoParts.GetEndColor().a
					, eventStatusInfoParts.GetTimeCounter().count()
					, eventStatusInfoParts.GetTimeRegulation()
					);
			}
			frameParts.GetHpGaugeParts().GetGauge().color = t13.UnityUtil.Color32InFluctUpdateAlpha(
				t13.UnityUtil.ColorForColor32(frameParts.GetHpGaugeParts().GetGauge().color)
				, eventStatusInfoParts.GetTimeFlucts(4)
				, eventStatusInfoParts.GetEndColor().a
				, eventStatusInfoParts.GetTimeCounter().count()
				, eventStatusInfoParts.GetTimeRegulation()
				);
		}

		return mine.state_;
	}

	//AllColorUpdate
	static private EventStatusInfoPartsProcess AllColorUpdateUpdate(EventStatusInfoPartsProcessState mine, EventStatusInfoParts eventStatusInfoParts) {
		if (eventStatusInfoParts.GetTimeCounter().measure(Time.deltaTime, eventStatusInfoParts.GetTimeRegulation())) {
			BaseParts baseParts = eventStatusInfoParts.GetStatusInfoParts().GetBaseParts();
			FrameParts frameParts = eventStatusInfoParts.GetStatusInfoParts().GetFrameParts();

			baseParts.GetBaseSprite().color = t13.UnityUtil.Color32InFluctUpdateAlpha(
				t13.UnityUtil.ColorForColor32(baseParts.GetBaseSprite().color)
				, eventStatusInfoParts.GetTimeFlucts(5)
				, eventStatusInfoParts.GetEndColor().a
				, eventStatusInfoParts.GetTimeRegulation()
				, eventStatusInfoParts.GetTimeRegulation()
				);
			baseParts.GetHpLogoText().color = t13.UnityUtil.Color32InFluctUpdateAlpha(
				t13.UnityUtil.ColorForColor32(baseParts.GetHpLogoText().color)
				, eventStatusInfoParts.GetTimeFlucts(0)
				, eventStatusInfoParts.GetEndColor().a
				, eventStatusInfoParts.GetTimeRegulation()
				, eventStatusInfoParts.GetTimeRegulation()
				);
			baseParts.GetInfoEventText().GetText().color = t13.UnityUtil.Color32InFluctUpdateAlpha(
				t13.UnityUtil.ColorForColor32(baseParts.GetInfoEventText().GetText().color)
				, eventStatusInfoParts.GetTimeFlucts(1)
				, eventStatusInfoParts.GetEndColor().a
				, eventStatusInfoParts.GetTimeRegulation()
				, eventStatusInfoParts.GetTimeRegulation()
				);

			frameParts.GetFrameSprite().color = t13.UnityUtil.Color32InFluctUpdateAlpha(
				t13.UnityUtil.ColorForColor32(frameParts.GetFrameSprite().color)
				, eventStatusInfoParts.GetTimeFlucts(2)
				, eventStatusInfoParts.GetEndColor().a
				, eventStatusInfoParts.GetTimeRegulation()
				, eventStatusInfoParts.GetTimeRegulation()
				);
			if (frameParts.GetHpGaugeParts().GetInfoText() != null) {
				frameParts.GetHpGaugeParts().GetInfoText().color = t13.UnityUtil.Color32InFluctUpdateAlpha(
					t13.UnityUtil.ColorForColor32(frameParts.GetHpGaugeParts().GetInfoText().color)
					, eventStatusInfoParts.GetTimeFlucts(3)
					, eventStatusInfoParts.GetEndColor().a
					, eventStatusInfoParts.GetTimeRegulation()
					, eventStatusInfoParts.GetTimeRegulation()
					);
			}
			frameParts.GetHpGaugeParts().GetGauge().color = t13.UnityUtil.Color32InFluctUpdateAlpha(
				t13.UnityUtil.ColorForColor32(frameParts.GetHpGaugeParts().GetGauge().color)
				, eventStatusInfoParts.GetTimeFlucts(4)
				, eventStatusInfoParts.GetEndColor().a
				, eventStatusInfoParts.GetTimeRegulation()
				, eventStatusInfoParts.GetTimeRegulation()
				);

			return EventStatusInfoPartsProcess.None;
		}
		else {
			BaseParts baseParts = eventStatusInfoParts.GetStatusInfoParts().GetBaseParts();
			FrameParts frameParts = eventStatusInfoParts.GetStatusInfoParts().GetFrameParts();

			baseParts.GetBaseSprite().color = t13.UnityUtil.Color32InFluctUpdateAlpha(
				t13.UnityUtil.ColorForColor32(baseParts.GetBaseSprite().color)
				, eventStatusInfoParts.GetTimeFlucts(5)
				, eventStatusInfoParts.GetEndColor().a
				, eventStatusInfoParts.GetTimeCounter().count()
				, eventStatusInfoParts.GetTimeRegulation()
				);
			baseParts.GetHpLogoText().color = t13.UnityUtil.Color32InFluctUpdateAlpha(
				t13.UnityUtil.ColorForColor32(baseParts.GetHpLogoText().color)
				, eventStatusInfoParts.GetTimeFlucts(0)
				, eventStatusInfoParts.GetEndColor().a
				, eventStatusInfoParts.GetTimeCounter().count()
				, eventStatusInfoParts.GetTimeRegulation()
				);
			baseParts.GetInfoEventText().GetText().color = t13.UnityUtil.Color32InFluctUpdateAlpha(
				t13.UnityUtil.ColorForColor32(baseParts.GetInfoEventText().GetText().color)
				, eventStatusInfoParts.GetTimeFlucts(1)
				, eventStatusInfoParts.GetEndColor().a
				, eventStatusInfoParts.GetTimeCounter().count()
				, eventStatusInfoParts.GetTimeRegulation()
				);

			frameParts.GetFrameSprite().color = t13.UnityUtil.Color32InFluctUpdateAlpha(
				t13.UnityUtil.ColorForColor32(frameParts.GetFrameSprite().color)
				, eventStatusInfoParts.GetTimeFlucts(2)
				, eventStatusInfoParts.GetEndColor().a
				, eventStatusInfoParts.GetTimeCounter().count()
				, eventStatusInfoParts.GetTimeRegulation()
				);
			if (frameParts.GetHpGaugeParts().GetInfoText() != null) {
				frameParts.GetHpGaugeParts().GetInfoText().color = t13.UnityUtil.Color32InFluctUpdateAlpha(
					t13.UnityUtil.ColorForColor32(frameParts.GetHpGaugeParts().GetInfoText().color)
					, eventStatusInfoParts.GetTimeFlucts(3)
					, eventStatusInfoParts.GetEndColor().a
					, eventStatusInfoParts.GetTimeCounter().count()
					, eventStatusInfoParts.GetTimeRegulation()
					);
			}
			frameParts.GetHpGaugeParts().GetGauge().color = t13.UnityUtil.Color32InFluctUpdateAlpha(
				t13.UnityUtil.ColorForColor32(frameParts.GetHpGaugeParts().GetGauge().color)
				, eventStatusInfoParts.GetTimeFlucts(4)
				, eventStatusInfoParts.GetEndColor().a
				, eventStatusInfoParts.GetTimeCounter().count()
				, eventStatusInfoParts.GetTimeRegulation()
				);
		}

		return mine.state_;
	}

	private delegate EventStatusInfoPartsProcess UpdateFunc(EventStatusInfoPartsProcessState mine, EventStatusInfoParts eventStatusInfoParts);
	private UpdateFunc[] updateFuncs_ = new UpdateFunc[(int)EventStatusInfoPartsProcess.Max] {
		NoneUpdate
		, ColorUpdateUpdate
		, AllColorUpdateUpdate
	};
	public EventStatusInfoPartsProcess Update(EventStatusInfoParts eventStatusInfoParts) { return updateFuncs_[(int)state_](this, eventStatusInfoParts); }
}
