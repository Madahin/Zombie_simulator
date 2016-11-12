using UnityEngine;

public class RandomSound : MonoBehaviour {

    public AudioClip[] clips;

    private float timer;
    private float next;

    private AudioSource source;

    System.Random rand;

	// Use this for initialization
	void Start () {
        rand = new System.Random();
        next = rand.Next(3, 10);
        source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if(timer >= next)
        {
            next = rand.Next(3, 10);
            timer = 0;
            if (!source.isPlaying)
            {
                source.clip = clips[rand.Next(clips.Length)];
                source.Play();
            }
        }
	}
}
