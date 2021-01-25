﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameParts : MonoBehaviour {
	[SerializeField] private SpriteRenderer frameSprite_ = null;
	[SerializeField] private HpGaugeParts hpGaugeParts_ = null;
	[SerializeField] private EventHpGaugeParts eventHpGaugeParts_ = null;

	public SpriteRenderer GetFrameSprite() { return frameSprite_; }
	public HpGaugeParts GetHpGaugeParts() { return hpGaugeParts_; }
	public EventHpGaugeParts GetEventHpGaugeParts() { return eventHpGaugeParts_; }
}
