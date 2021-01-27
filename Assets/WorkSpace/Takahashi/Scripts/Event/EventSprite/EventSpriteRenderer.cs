using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSpriteRenderer : MonoBehaviour {
	//EntryPoint
	void Update() {
		//メイン処理
		processState_.state_ = processState_.Update(this);
	}

	private EventSpriteRendererProcessState processState_ = new EventSpriteRendererProcessState(EventSpriteRendererProcess.None);

	private t13.TimeCounter timeCounter_ = new t13.TimeCounter();

	private float timeRegulation_ = 0;
	private List<Sprite> animeSprites_ = null;
	private int nowAnimeSpriteNumber_ = 0;

	[SerializeField] private SpriteRenderer spriteRenderer_ = null;

	public t13.TimeCounter GetTimeCounter() { return timeCounter_; }

	public float GetTimeRegulation() { return timeRegulation_; }
	public List<Sprite> GetAnimeSprites() { return animeSprites_; }
	public void SetNowAnimeSpriteNumber(int number) { nowAnimeSpriteNumber_ = number; }
	public int GetNowAnimeSpriteNumber() { return nowAnimeSpriteNumber_; }

	public SpriteRenderer GetSpriteRenderer() { return spriteRenderer_; }

	public void ProcessStateAnimeExecute(float timeRegulation, List<Sprite> sprites) {
		timeRegulation_ = timeRegulation;
		animeSprites_ = sprites;

		spriteRenderer_.sprite = animeSprites_[nowAnimeSpriteNumber_];

		processState_.state_ = EventSpriteRendererProcess.Anime;
	}
}
