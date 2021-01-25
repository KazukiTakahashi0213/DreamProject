using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioParts : MonoBehaviour {
	[SerializeField] private AudioSource audioSource_ = null;

	public AudioSource GetAudioSource() { return audioSource_; }
}
