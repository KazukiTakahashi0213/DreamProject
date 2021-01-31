using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillData : ISkillData {
	//EntryPoint
	public SkillData(int number) {
		//初期化
		addPlayerParameterRanks_ = new List<AddParameterRankState>();
		addEnemyParameterRanks_ = new List<AddParameterRankState>();

		addPlayerAbnormalStates_ = new List<AddAbnormalTypeState>();
		addEnemyAbnormalStates_ = new List<AddAbnormalTypeState>();

		ResourcesSkillData data = ResourcesSkillDatasLoader.GetInstance().GetSkillDatas(number);

		skillNname_ = data.skillNname_;

		effectValue_ = data.effectValue_;

		optionEffectTriggerRateValue_ = data.optionEffectTriggerRateValue_;
		hitRateValue_ = data.hitRateValue_;
		upDpValue_ = data.upDpValue_;

		playPoint_ = data.playPoint_;
		nowPlayPoint_ = playPoint_;

		elementType_ = new ElementTypeState((ElementType)data.elementType_);
		effectType_ = new EffectTypeState((EffectType)data.effectType_[0], (EffectAttackType)data.effectType_[1]);

		triggerPriority_ = data.triggerPriority_;
		criticalParameterRank_ = data.criticalParameterRank_;

		Sprite[] sprite = Resources.LoadAll<Sprite>("Graphics/SkillEffect/" + data.effectName_);
		for (int i = 0; i < sprite.Length; ++i) {
			animeSprites_.Add(sprite[i]);
		}

		for(int i = 0;i < data.addPlayerParameterRanks_.Length; ++i) {
			addPlayerParameterRanks_.Add(
				new AddParameterRankState((AddParameterRank)data.addPlayerParameterRanks_[i].addParameterRank_
				, data.addPlayerParameterRanks_[i].value_
				));
		}
		for (int i = 0; i < data.addEnemyParameterRanks_.Length; ++i) {
			addEnemyParameterRanks_.Add(
				new AddParameterRankState((AddParameterRank)data.addEnemyParameterRanks_[i].addParameterRank_
				, data.addEnemyParameterRanks_[i].value_
				));
		}

		for (int i = 0; i < data.addPlayerAbnormals_.Length; ++i) {
			addPlayerAbnormalStates_.Add(new AddAbnormalTypeState((AddAbnormalType)data.addPlayerAbnormals_[i].addAbnormal_));
		}
		for (int i = 0; i < data.addEnemyAbnormals_.Length; ++i) {
			addEnemyAbnormalStates_.Add(new AddAbnormalTypeState((AddAbnormalType)data.addEnemyAbnormals_[i].addAbnormal_));
		}
	}

	public int skillNumber_ { get; }
	public string skillNname_ { get; }

	public float effectValue_ { get; }

	public List<AddParameterRankState> addPlayerParameterRanks_ { get; }
	public List<AddParameterRankState> addEnemyParameterRanks_ { get; }

	public List<AddAbnormalTypeState> addPlayerAbnormalStates_ { get; }
	public List<AddAbnormalTypeState> addEnemyAbnormalStates_ { get; }

	public int optionEffectTriggerRateValue_ { get; }
	public int hitRateValue_ { get; }
	public int upDpValue_ { get; }

	public int playPoint_ { get; }
	public int nowPlayPoint_ { get; set; }

	public ElementTypeState elementType_ { get; }
	public EffectTypeState effectType_ { get; }

	public int triggerPriority_ { get; }
	public int criticalParameterRank_ { get; }

	private List<Sprite> animeSprites_ = new List<Sprite>();

	public void Animetion(EffectParts targetEffectParts) {
		AllEventManager.GetInstance().EventSpriteRendererSet(targetEffectParts.GetEventSpriteRenderer(), animeSprites_, new Color32());
		AllEventManager.GetInstance().EventSpriteRenderersUpdateExecuteSet(EventSpriteRendererEventManagerExecute.Anime);
		AllEventManager.GetInstance().AllUpdateEventExecute(0.35f);
	}
}
