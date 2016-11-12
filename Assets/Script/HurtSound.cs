using UnityEngine;

public class HurtSound : MonoBehaviour
{

    public AudioClip[] clips;
    

    private AudioSource source;

    System.Random rand;

    // Use this for initialization
    void Start()
    {
        rand = new System.Random();
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void Sound()
    {
        if (source.isPlaying)
        {
            source.Stop();
        }
            source.clip = clips[rand.Next(clips.Length)];
            source.Play();
    }
}
