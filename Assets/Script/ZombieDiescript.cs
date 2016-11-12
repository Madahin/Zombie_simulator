using UnityEngine;
using System.Collections;

public class ZombieDiescript : MonoBehaviour {

    public AudioClip[] clips;

    private float timer;
    private float next;

    private AudioSource source;

    System.Random rand;

    // Use this for initialization
    void Start()
    {
        rand = new System.Random();
        source = GetComponent<AudioSource>();
        source.clip = clips[rand.Next(clips.Length)];
        source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!source.isPlaying) Destroy(gameObject);
    }
}
