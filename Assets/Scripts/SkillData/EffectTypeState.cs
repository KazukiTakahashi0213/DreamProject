using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectType {
	None,
	Attack,
	Support,
	Max
}

public class EffectTypeState {
	public EffectTypeState(EffectType setState, EffectAttackType effectAttackType = EffectAttackType.Normal) {
		state_ = setState;
		effectAttackTypeState_.state_ = effectAttackType;
	}

	public EffectType state_;

	//None
	static private void NoneExecutePlayerEventSet(EffectTypeState mine, BattleManager mgr, IMonsterData attackMonsterData, ISkillData attackSkillData, IMonsterData defenseMonsterData) {

	}
	static private void NoneExecuteEnemyEventSet(EffectTypeState mine, BattleManager mgr, IMonsterData attackMonsterData, ISkillData attackSkillData, IMonsterData defenseMonsterData) {

	}

	//Attack
	static private void AttackExecutePlayerEventSet(EffectTypeState mine, BattleManager mgr, IMonsterData attackMonsterData, ISkillData attackSkillData, IMonsterData defenseMonsterData) {
		//ランク補正の計算
		float monsterHitRateValue = 0;
		{
			//回避率と命中率のランク
			int monsterHitRank = attackMonsterData.battleData_.GetHitRateParameterRank() - defenseMonsterData.battleData_.GetAvoidRateParameterRank();

			//分子,分母
			float numerator = 3, denominator = 3;

			if (monsterHitRank < 0) denominator -= monsterHitRank;
			else numerator += monsterHitRank;

			//回避率と命中率のランクの倍率
			monsterHitRateValue = numerator / denominator;
		}

		//攻撃のヒット判定
		//技の命中率×命中補正値M×ランク補正
		bool skillHit = AllSceneManager.GetInstance().GetRandom().Next(0, 100) < (int)(attackSkillData.hitRateValue_ * (4096 / 4096) * monsterHitRateValue);

		//攻撃がはずれた時の説明
		if (!skillHit) {
			AllEventManager.GetInstance().EventTextSet(mgr.GetNovelWindowParts().GetEventText(), "しかし　" + attackMonsterData.uniqueName_ + "の\nこうげきは　はずれた！");
			AllEventManager.GetInstance().EventTextsUpdateExecute(mgr.GetEventContextUpdateTime());
			AllEventManager.GetInstance().EventWaitSet(1.0f);

			return;
		}

		//技のアニメーション
		attackSkillData.Animetion(mgr.GetEnemyEffectParts());
		AllEventManager.GetInstance().EventWaitSet(mgr.GetEventWaitTime());

		//ダメージアクション（点滅）
		if (attackSkillData.effectValue_ != 0) {
			mgr.GetEnemyMonsterParts().SpriteBlinkEventSet(4, 0.06f);
		}

		//急所の判定
		float critical = 1;
		if (attackSkillData.criticalParameterRank_ <= 0) {
			if (AllSceneManager.GetInstance().GetRandom().Next(0, 24) == 13) critical = 1.5f;
		}
		else if (attackSkillData.criticalParameterRank_ == 1) {
			if (AllSceneManager.GetInstance().GetRandom().Next(0, 8) == 4) critical = 1.5f;
		}
		else if (attackSkillData.criticalParameterRank_ == 2) {
			if (AllSceneManager.GetInstance().GetRandom().Next(0, 2) == 0) critical = 1.5f;
		}
		else critical = 1.5f;

		//ヒットポイントの変動
		int realDamage = (int)(MonsterData.BattleDamageCalculate(attackMonsterData, defenseMonsterData, attackSkillData) * critical);
		defenseMonsterData.nowHitPoint_ -= realDamage;

		//ヒットポイントのゲージの変動イベントの設定
		float hpGaugeFillAmount = t13.Utility.ValueForPercentage(defenseMonsterData.RealHitPoint(), defenseMonsterData.nowHitPoint_, 1);
		AllEventManager.GetInstance().HpGaugePartsSet(mgr.GetEnemyStatusInfoParts().GetFrameParts().GetHpGaugeParts(), hpGaugeFillAmount);
		AllEventManager.GetInstance().HpGaugePartsUpdateExecuteSet(HpGaugePartsEventManagerExecute.GaugeUpdate);
		AllEventManager.GetInstance().AllUpdateEventExecute(0.5f);
		//ウェイト
		AllEventManager.GetInstance().EventWaitSet(mgr.GetEventWaitTime());

		if (critical > 1.0f) {
			//急所の説明
			AllEventManager.GetInstance().EventTextSet(mgr.GetNovelWindowParts().GetEventText(), "きゅうしょに　あたった！");
			AllEventManager.GetInstance().EventTextsUpdateExecute(mgr.GetEventContextUpdateTime());
			AllEventManager.GetInstance().EventWaitSet(mgr.GetEventWaitTime());
		}

		//効果の説明
		if (defenseMonsterData.ElementSimillarChecker(attackSkillData.elementType_) > 1.0f) {
			AllEventManager.GetInstance().EventTextSet(mgr.GetNovelWindowParts().GetEventText(), "こうかは　ばつぐんだ！");
			AllEventManager.GetInstance().EventTextsUpdateExecute(mgr.GetEventContextUpdateTime());
			AllEventManager.GetInstance().EventWaitSet(mgr.GetEventWaitTime());
		}
		else if (defenseMonsterData.ElementSimillarChecker(attackSkillData.elementType_) < 1.0f
			&& defenseMonsterData.ElementSimillarChecker(attackSkillData.elementType_) > 0) {
			AllEventManager.GetInstance().EventTextSet(mgr.GetNovelWindowParts().GetEventText(), "こうかは　いまひとつの　ようだ");
			AllEventManager.GetInstance().EventTextsUpdateExecute(mgr.GetEventContextUpdateTime());
			AllEventManager.GetInstance().EventWaitSet(mgr.GetEventWaitTime());
		}
		else if (defenseMonsterData.ElementSimillarChecker(attackSkillData.elementType_) < 0.1f) {
			AllEventManager.GetInstance().EventTextSet(mgr.GetNovelWindowParts().GetEventText(), "こうかは　ないようだ・・・");
			AllEventManager.GetInstance().EventTextsUpdateExecute(mgr.GetEventContextUpdateTime());
			AllEventManager.GetInstance().EventWaitSet(mgr.GetEventWaitTime());
		}
	}
	static private void AttackExecuteEnemyEventSet(EffectTypeState mine, BattleManager mgr, IMonsterData attackMonsterData, ISkillData attackSkillData, IMonsterData defenseMonsterData) {
		//ランク補正の計算
		float monsterHitRateValue = 0;
		{
			//回避率と命中率のランク
			int monsterHitRank = attackMonsterData.battleData_.GetHitRateParameterRank() - defenseMonsterData.battleData_.GetAvoidRateParameterRank();

			//分子,分母
			float numerator = 3, denominator = 3;

			if (monsterHitRank < 0) denominator -= monsterHitRank;
			else numerator += monsterHitRank;

			//回避率と命中率のランクの倍率
			monsterHitRateValue = numerator / denominator;
		}

		//攻撃のヒット判定
		//技の命中率×命中補正値M×ランク補正
		bool skillHit = AllSceneManager.GetInstance().GetRandom().Next(0, 100) < (int)(attackSkillData.hitRateValue_ * (4096 / 4096) * monsterHitRateValue);

		//攻撃がはずれた時の説明
		if (!skillHit) {
			AllEventManager.GetInstance().EventTextSet(mgr.GetNovelWindowParts().GetEventText(), "しかし　あいての　" + attackMonsterData.uniqueName_ + "の\nこうげきは　はずれた！");
			AllEventManager.GetInstance().EventTextsUpdateExecute(mgr.GetEventContextUpdateTime());
			AllEventManager.GetInstance().EventWaitSet(1.0f);

			return;
		}

		//技のアニメーション
		attackSkillData.Animetion(mgr.GetPlayerEffectParts());
		AllEventManager.GetInstance().EventWaitSet(mgr.GetEventWaitTime());

		//急所の判定
		float critical = 1;
		if (attackSkillData.criticalParameterRank_ <= 0) {
			if (AllSceneManager.GetInstance().GetRandom().Next(0, 24) == 13) critical = 1.5f;
		}
		else if (attackSkillData.criticalParameterRank_ == 1) {
			if (AllSceneManager.GetInstance().GetRandom().Next(0, 8) == 4) critical = 1.5f;
		}
		else if (attackSkillData.criticalParameterRank_ == 2) {
			if (AllSceneManager.GetInstance().GetRandom().Next(0, 2) == 0) critical = 1.5f;
		}
		else critical = 1.5f;

		//ヒットポイントの変動
		int realDamage = (int)(MonsterData.BattleDamageCalculate(attackMonsterData, defenseMonsterData, attackSkillData) * critical);
		defenseMonsterData.nowHitPoint_ -= realDamage;

		//ダメージアクション（点滅）
		if (attackSkillData.effectValue_ != 0) {
			mgr.GetPlayerMonsterParts().SpriteBlinkEventSet(4, 0.06f);
		}

		//ヒットポイントのゲージの変動イベントの設定
		float hpGaugeFillAmount = t13.Utility.ValueForPercentage(defenseMonsterData.RealHitPoint(), defenseMonsterData.nowHitPoint_, 1);
		AllEventManager.GetInstance().HpGaugePartsSet(mgr.GetPlayerStatusInfoParts().GetFrameParts().GetHpGaugeParts(), hpGaugeFillAmount, defenseMonsterData);
		AllEventManager.GetInstance().HpGaugePartsUpdateExecuteSet(HpGaugePartsEventManagerExecute.GaugeUpdate);
		AllEventManager.GetInstance().AllUpdateEventExecute(0.5f);
		//ウェイト
		AllEventManager.GetInstance().EventWaitSet(mgr.GetEventWaitTime());

		if (critical > 1.0f) {
			//急所の説明
			AllEventManager.GetInstance().EventTextSet(mgr.GetNovelWindowParts().GetEventText(), "きゅうしょに　あたった！");
			AllEventManager.GetInstance().EventTextsUpdateExecute(mgr.GetEventContextUpdateTime());
			AllEventManager.GetInstance().EventWaitSet(mgr.GetEventWaitTime());
		}

		//効果の説明
		if (defenseMonsterData.ElementSimillarChecker(attackSkillData.elementType_) > 1.0f) {
			AllEventManager.GetInstance().EventTextSet(mgr.GetNovelWindowParts().GetEventText(), "こうかは　ばつぐんだ！");
			AllEventManager.GetInstance().EventTextsUpdateExecute(mgr.GetEventContextUpdateTime());
			AllEventManager.GetInstance().EventWaitSet(mgr.GetEventWaitTime());
		}
		else if (defenseMonsterData.ElementSimillarChecker(attackSkillData.elementType_) < 1.0f
			&& defenseMonsterData.ElementSimillarChecker(attackSkillData.elementType_) > 0) {
			AllEventManager.GetInstance().EventTextSet(mgr.GetNovelWindowParts().GetEventText(), "こうかは　いまひとつの　ようだ");
			AllEventManager.GetInstance().EventTextsUpdateExecute(mgr.GetEventContextUpdateTime());
			AllEventManager.GetInstance().EventWaitSet(mgr.GetEventWaitTime());
		}
		else if (defenseMonsterData.ElementSimillarChecker(attackSkillData.elementType_) < 0.1f) {
			AllEventManager.GetInstance().EventTextSet(mgr.GetNovelWindowParts().GetEventText(), "こうかが　ないようだ・・・");
			AllEventManager.GetInstance().EventTextsUpdateExecute(mgr.GetEventContextUpdateTime());
			AllEventManager.GetInstance().EventWaitSet(mgr.GetEventWaitTime());
		}
	}

	//Support
	static private void SupportExecutePlayerEventSet(EffectTypeState mine, BattleManager mgr, IMonsterData attackMonsterData, ISkillData attackSkillData, IMonsterData defenseMonsterData) {
		//技のアニメーション
		attackSkillData.Animetion(mgr.GetEnemyEffectParts());
		AllEventManager.GetInstance().EventWaitSet(mgr.GetEventWaitTime());
	}
	static private void SupportExecuteEnemyEventSet(EffectTypeState mine, BattleManager mgr, IMonsterData attackMonsterData, ISkillData attackSkillData, IMonsterData defenseMonsterData) {
		//技のアニメーション
		attackSkillData.Animetion(mgr.GetPlayerEffectParts());
		AllEventManager.GetInstance().EventWaitSet(mgr.GetEventWaitTime());
	}

	private delegate void ExecutePlayerEventSetFunc(EffectTypeState mine, BattleManager mgr, IMonsterData attackMonsterData, ISkillData attackSkillData, IMonsterData defenseMonsterData);
	private ExecutePlayerEventSetFunc[] executePlayerEventSets_ = new ExecutePlayerEventSetFunc[(int)EffectType.Max] {
		NoneExecutePlayerEventSet,
		AttackExecutePlayerEventSet,
		SupportExecutePlayerEventSet
	};
	public void ExecutePlayerEventSet(BattleManager battleManager, IMonsterData attackMonsterData, ISkillData attackSkillData, IMonsterData defenseMonsterData) { executePlayerEventSets_[(int)state_](this, battleManager, attackMonsterData, attackSkillData, defenseMonsterData); }

	private delegate void ExecuteEnemyEventSetFunc(EffectTypeState mine, BattleManager mgr, IMonsterData attackMonsterData, ISkillData attackSkillData, IMonsterData defenseMonsterData);
	private ExecuteEnemyEventSetFunc[] executeEnemyEventSets_ = new ExecuteEnemyEventSetFunc[(int)EffectType.Max] {
		NoneExecuteEnemyEventSet,
		AttackExecuteEnemyEventSet,
		SupportExecuteEnemyEventSet
	};
	public void ExecuteEnemyEventSet(BattleManager battleManager, IMonsterData attackMonsterData, ISkillData attackSkillData, IMonsterData defenseMonsterData) { executeEnemyEventSets_[(int)state_](this, battleManager, attackMonsterData, attackSkillData, defenseMonsterData); }

	private EffectAttackTypeState effectAttackTypeState_ = new EffectAttackTypeState(EffectAttackType.Normal);
	public EffectAttackTypeState GetEffectAttackTypeState() { return effectAttackTypeState_; }
}
