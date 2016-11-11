using UnityEngine;
using System.Collections;

public class ZombieFaction : MonoBehaviour {
    public uint faction;
    private static uint nbFaction;
    private ArrayList colorList = new ArrayList{Color.red,Color.green,Color.black,Color.blue, Color.yellow};
    private Random rnd = new Random();

    // Use this for initialization
    void Start ()
    {
        faction = nbFaction;
        nbFaction++;
        MeshRenderer m = gameObject.GetComponentInChildren<MeshRenderer>();
        m.enabled = true;

        m.material.color = (Color)colorList[(int)faction];
    }    
}
