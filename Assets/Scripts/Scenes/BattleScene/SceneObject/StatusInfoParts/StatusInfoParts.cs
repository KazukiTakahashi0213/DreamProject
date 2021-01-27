using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusInfoParts : MonoBehaviour {
	//EntryPoint
	private void Update() {
		processState_ = processState_.Update(this);
	}

	[SerializeField] FrameParts frameParts_ = null;
	[SerializeField] BaseParts baseParts_ = null;
	[SerializeField] AbnormalStateInfoParts firstAbnormalStateInfoParts_ = null;
	[SerializeField] AbnormalStateInfoParts secondAbnormalStateInfoParts_ = null;
	[SerializeField] UpdateGameObject eventGameObject_ = null;
	[SerializeField] float idleTimeRegulation_ = 0.5f;

	private IStatusInfoPartsProcessState processState_ = new StatusInfoPartsProcessNone();
	private IStatusInfoPartsProcessIdleState processIdleState_ = new StatusInfoPartsProcessIdleDown();

	private t13.TimeFluct timeFluct_ = new t13.TimeFluct();
	private t13.TimeCounter timeCounter_ = new t13.TimeCounter();

	private Vector3 entryPos_;

	public FrameParts GetFrameParts() { return frameParts_; }
	public BaseParts GetBaseParts() { return baseParts_; }
	public AbnormalStateInfoParts GetFirstAbnormalStateInfoParts() { return firstAbnormalStateInfoParts_; }
	public AbnormalStateInfoParts GetSecondAbnormalStateInfoParts() { return secondAbnormalStateInfoParts_; }
	public UpdateGameObject GetEventGameObject() { return eventGameObject_; }
	public float GetIdleTimeRegulation() { return idleTimeRegulation_; }

	public void SetProcessIdleState(IStatusInfoPartsProcessIdleState state) { processIdleState_ = state; }
	public IStatusInfoPartsProcessIdleState GetProcessIdleState() { return processIdleState_; }

	public t13.TimeFluct GetTimeFluct() { return timeFluct_; }
	public t13.TimeCounter GetTimeCounter() { return timeCounter_; }

	public void ProcessIdleStart() {
		entryPos_ = transform.position;

		processState_ = new StatusInfoPartsProcessIdle();
	}
	public void ProcessIdleEnd() {
		t13.UnityUtil.ObjectPosMove(GetEventGameObject().GetGameObject(), entryPos_);

		processIdleState_ = new StatusInfoPartsProcessIdleDown();

		processState_ = new StatusInfoPartsProcessNone();
	}

	public void SetAllAlphaValue(int value) {
		frameParts_.GetFrameSprite().color = new Color(frameParts_.GetFrameSprite().color.r, frameParts_.GetFrameSprite().color.g, frameParts_.GetFrameSprite().color.b, value / 255);
		if (frameParts_.GetHpGaugeParts().GetInfoText() != null) {
			frameParts_.GetHpGaugeParts().GetInfoText().color = new Color(frameParts_.GetHpGaugeParts().GetInfoText().color.r, frameParts_.GetHpGaugeParts().GetInfoText().color.g, frameParts_.GetHpGaugeParts().GetInfoText().color.b, value / 255);
		}
		frameParts_.GetHpGaugeParts().GetGauge().color = new Color(frameParts_.GetHpGaugeParts().GetGauge().color.r, frameParts_.GetHpGaugeParts().GetGauge().color.g, frameParts_.GetHpGaugeParts().GetGauge().color.b, value / 255);

		baseParts_.GetBaseSprite().color = new Color(baseParts_.GetBaseSprite().color.r, baseParts_.GetBaseSprite().color.g, baseParts_.GetBaseSprite().color.b, value / 255);
		baseParts_.GetHpLogoText().color = new Color(baseParts_.GetHpLogoText().color.r, baseParts_.GetHpLogoText().color.g, baseParts_.GetHpLogoText().color.b, value / 255);
		baseParts_.GetInfoEventText().GetText().color = new Color(baseParts_.GetInfoEventText().GetText().color.r, baseParts_.GetInfoEventText().GetText().color.g, baseParts_.GetInfoEventText().GetText().color.b, value / 255);
	}

	public void MonsterStatusInfoSet(IMonsterData monsterData) {
		//名前とレベルをTextに反映
		string monsterViewName = t13.Utility.StringFullSpaceBackTamp(monsterData.uniqueName_, 6);
		baseParts_.GetInfoEventText().GetText().text = monsterViewName + "　　Lｖ" + t13.Utility.HarfSizeForFullSize(monsterData.level_.ToString());

		//HPをTextに反映
		//HPゲージの調整
		float hpGaugeFillAmount = t13.Utility.ValueForPercentage(monsterData.RealHitPoint(), monsterData.nowHitPoint_, 1);
		frameParts_.GetHpGaugeParts().ProcessStateGaugeUpdateExecute(0, t13.TimeFluctProcess.Liner, monsterData, hpGaugeFillAmount);
	}
}
