using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour {

    public bool isHit = false;
    public uint PV { get
        {
            return m_pv;
        }
    }

    private float hurtTime;

    
    private uint m_pv = 3;

    private MeshRenderer rendere;
    private SkinnedMeshRenderer mrenderer;
    private Color c;


	// Use this for initialization
	void Start () {
        hurtTime = 0;

        rendere = GetComponentInChildren<MeshRenderer>();
        mrenderer = GetComponentInChildren<SkinnedMeshRenderer>();

        if(rendere != null)
            c = rendere.material.color;

        if (mrenderer != null)
            c = mrenderer.material.color;
    }
	
	// Update is called once per frame
	void Update () {
	    if(isHit)
        {
            hurtTime += Time.deltaTime;
            if (rendere != null)
                rendere.material.color = Color.Lerp(c, Color.red, Mathf.PingPong(hurtTime * 10, 1));
            if (mrenderer != null)
                mrenderer.material.color = Color.Lerp(c, Color.red, Mathf.PingPong(hurtTime * 10, 1));
            if (hurtTime >= 0.5f)
            {
                isHit = false;
                hurtTime = 0;
                if (rendere != null)
                    rendere.material.color = c;
                if (mrenderer != null)
                    mrenderer.material.color = c;
            }
        }
	}

    public void Hit()
    {
        if (!isHit)
        {
            if (rendere != null)
                c = rendere.material.color;

            if (mrenderer != null)
                c = mrenderer.material.color;
            isHit = true;
            m_pv -= 1;
            if(GetComponent<HurtSound>())
                GetComponent<HurtSound>().Sound();
        }
    }
}
