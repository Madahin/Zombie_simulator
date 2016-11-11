using UnityEngine;
using System.Collections;

public class ZombieFaction : MonoBehaviour {
    public uint faction;
    private static uint nbFaction;

	// Use this for initialization
	void Start ()
    {
        faction = nbFaction;
        nbFaction++;
	}
}
