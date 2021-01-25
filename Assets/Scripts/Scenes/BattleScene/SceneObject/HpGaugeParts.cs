using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpGaugeParts : MonoBehaviour {
	[SerializeField] private Image gauge_ = null;
	[SerializeField] private Text infoText_ = null;

	public Image GetGauge() { return gauge_; }
	public Text GetInfoText() { return infoText_; }
}
