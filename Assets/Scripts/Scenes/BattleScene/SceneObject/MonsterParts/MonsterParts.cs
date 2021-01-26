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

	private t13.TimeFluct timeFluct_ = new t13.TimeFluct();
	private t13.TimeCounter timeCounter_ = new t13.TimeCounter();

	private Vector3 entryPos_;

	public SpriteRenderer GetMonsterSprite() { return monsterSprite_; }
	public EventSpriteRenderer GetEventMonsterSprite() { return eventMonsterSprite_; }
	public UpdateGameObject GetEventGameObject() { return eventGameObject_; }
	public float GetIdleTimeRegulation() { return idleTimeRegulation_; }

	public void SetProcessIdleState(IMonsterPartsProcessIdleState state) { processIdleState_ = state; }
	public IMonsterPartsProcessIdleState GetProcessIdleState() { return processIdleState_; }

	public t13.TimeFluct GetTimeFluct() { return timeFluct_; }
	public t13.TimeCounter GetTimeCounter() { return timeCounter_; }

	public void SpriteBlinkEventSet(int times, float changeTimeRegulation) {
		//ダメージアクション（点滅）
		for (int i = 0; i < times; ++i) {
			for (int j = 0; j < 2; ++j) {
				AllEventManager.GetInstance().UpdateGameObjectSet(eventGameObject_);
				AllEventManager.GetInstance().UpdateGameObjectsActiveSetExecute(j == 1);
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
