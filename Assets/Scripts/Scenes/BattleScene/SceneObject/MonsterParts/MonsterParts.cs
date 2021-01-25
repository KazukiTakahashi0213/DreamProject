using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterParts : MonoBehaviour {
	//EntryPoint
	private void Update() {
		processState_ = processState_.Update(this);
	}

	[SerializeField] private SpriteRenderer monsterSprite_ = null;
	[SerializeField] private EventSpriteRenderer eventMonsterSprite_ = null;
	[SerializeField] private UpdateGameObject eventGameObject_ = null;
	[SerializeField] float idleTimeRegulation_ = 0.5f;

	private IMonsterPartsProcessState processState_ = new MonsterPartsProcessNone();
	private IMonsterPartsProcessIdleState processIdleState_ = new MonsterPartsProcessIdleDown();

	private t13.Time_fluct timeFluct_ = new t13.Time_fluct();
	private t13.Time_counter timeCounter_ = new t13.Time_counter();

	private Vector3 entryPos_;

	public SpriteRenderer GetMonsterSprite() { return monsterSprite_; }
	public EventSpriteRenderer GetEventMonsterSprite() { return eventMonsterSprite_; }
	public UpdateGameObject GetEventGameObject() { return eventGameObject_; }
	public float GetIdleTimeRegulation() { return idleTimeRegulation_; }

	public void SetProcessIdleState(IMonsterPartsProcessIdleState state) { processIdleState_ = state; }
	public IMonsterPartsProcessIdleState GetProcessIdleState() { return processIdleState_; }

	public t13.Time_fluct GetTimeFluct() { return timeFluct_; }
	public t13.Time_counter GetTimeCounter() { return timeCounter_; }

	public void SpriteBlinkEventSet(int times, float changeTimeRegulation) {
		//ダメージアクション（点滅）
		for (int i = 0; i < times; ++i) {
			for (int j = 0; j < 2; ++j) {
				AllEventManager.GetInstance().EventGameObjectSet(eventGameObject_);
				AllEventManager.GetInstance().EventGameObjectsActiveSetExecute(j == 1);
				AllEventManager.GetInstance().EventWaitSet(changeTimeRegulation);
			}
		}
	}

	public void ProcessIdleStart() {
		entryPos_ = transform.position;

		processState_ = new MonsterPartsProcessIdle();
	}
	public void ProcessIdleEnd() {
		t13.UnityUtil.ObjectPosMove(GetEventGameObject().GetGameObject(), entryPos_);

		processIdleState_ = new MonsterPartsProcessIdleDown();

		processState_ = new MonsterPartsProcessNone();
	}
}
