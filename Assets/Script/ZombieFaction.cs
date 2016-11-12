using UnityEngine;
using System.Collections;

public class ZombieFaction : MonoBehaviour {
    [Range(0, 5)]
    public uint faction;
    public uint oldFaction;
    [SerializeField]
    private bool isBoss = false;
    public bool IsBoss
    {
        get
        {
            return isBoss;
        }
    }
    private ArrayList colorList = new ArrayList{Color.white, Color.red,Color.green,Color.black,Color.blue, Color.yellow};
    private Random rnd = new Random();
    MeshRenderer rendere;
    SkinnedMeshRenderer mrenderer;

    // Use this for initialization
    void Awake ()
    {
        oldFaction = faction;
        if(isBoss)
        {
            FactionManager.Instance.AddKey(gameObject);
        }
        rendere = gameObject.GetComponentInChildren<MeshRenderer>();
        mrenderer = gameObject.GetComponentInChildren<SkinnedMeshRenderer>();

        if (rendere != null)
        {
            rendere.enabled = true;
            rendere.material.color = (Color)colorList[(int)faction];
        }

        if (mrenderer != null)
        {
            mrenderer.enabled = true;
            mrenderer.material.color = (Color)colorList[(int)faction];
        }
    }

    public void SetFaction(uint factionID)
    {
        faction = factionID;
        if(rendere == null)
        {
            rendere = gameObject.GetComponentInChildren<MeshRenderer>();
        }
        if (mrenderer == null)
        {
            mrenderer = gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
        }

        if (rendere != null)
        {
            rendere.material.color = (Color)colorList[(int)faction];
        }
        if (mrenderer != null)
        {
            mrenderer.material.color = (Color)colorList[(int)faction];
        }

        
    }
}
