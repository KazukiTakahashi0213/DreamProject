using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSDParts : MonoBehaviour {
	[SerializeField] private SpriteRenderer monsterSDSpriteRenderer_ = null;
	[SerializeField] private UpdateGameObject eventGameObject_ = null;

	public SpriteRenderer GetMonsterSDSpriteRenderer() { return monsterSDSpriteRenderer_; }
	public UpdateGameObject GetEventGameObject() { return eventGameObject_; }
}
