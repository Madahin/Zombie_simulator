using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

    public AudioClip begin;
    private AudioSource source;

    // Use this for initialization
    void Start () {
        source = GetComponent<AudioSource>();
        source.clip = begin;
        source.Play();
	}
}
