using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParameterInfoFrameParts : MonoBehaviour {
	[SerializeField] private SpriteRenderer parameterInfoFrameSprite_ = null;
	[SerializeField] private List<MonsterParameterBarParts> monsterParameterBarsParts_ = null;

	public SpriteRenderer GetParameterInfoFrameSprite_() { return parameterInfoFrameSprite_; }
	public MonsterParameterBarParts GetMonsterParameterBarsParts(int value) { return monsterParameterBarsParts_[value]; }
	public int GetMonsterParameterBarsPartsCount() { return monsterParameterBarsParts_.Count; }

	public void MonsterDataReflect(IMonsterData referMonsterData) {
		monsterParameterBarsParts_[0].ParameterReflect(referMonsterData.RealHitPoint());
		monsterParameterBarsParts_[1].ParameterReflect(referMonsterData.RealAttack());
		monsterParameterBarsParts_[2].ParameterReflect(referMonsterData.RealDefense());
		monsterParameterBarsParts_[3].ParameterReflect(referMonsterData.RealSpecialAttack());
		monsterParameterBarsParts_[4].ParameterReflect(referMonsterData.RealSpecialDefense());
		monsterParameterBarsParts_[5].ParameterReflect(referMonsterData.RealSpeed());
	}
}
