using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class FactionManager : Singleton<FactionManager> {

    [SerializeField]
    private Dictionary<GameObject, List<GameObject>> factionEntity;

	// Use this for initialization
	void Awake () {
        if (factionEntity == null)
        {
            factionEntity = new Dictionary<GameObject, List<GameObject>>();
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void AddKey(GameObject key)
    {
        if(factionEntity == null)
        {
            factionEntity = new Dictionary<GameObject, List<GameObject>>();
        }
        factionEntity.Add(key, new List<GameObject>());
    }

    public void Wololo(GameObject oldFactionChief, uint newFaction)
    {
        if (factionEntity.ContainsKey(oldFactionChief))
        {
            foreach (GameObject go in factionEntity[oldFactionChief])
            {
                ZombieFaction zf = go.GetComponent<ZombieFaction>();
                zf.SetFaction(newFaction);
            }
        }
        
    }

    public void AddEntity(uint faction, GameObject entity)
    {
        GameObject factionChief = null;
        foreach(GameObject go in factionEntity.Keys)
        {
            if (go == null) continue;
            ZombieFaction zf = go.GetComponent<ZombieFaction>();
            //Debug.Log(zf);
            if (zf != null && zf.faction == faction)
            {
                factionChief = go;
                break;
            }
        }
        if(factionChief != null)
            factionEntity[factionChief].Add(entity);
    }

    public void RemoveEntity(GameObject entity)
    {
        foreach(List<GameObject> entities in factionEntity.Values)
        {
            if(entities.Contains(entity))
            {
                entities.Remove(entity);
            }
        }
    }
    

}
