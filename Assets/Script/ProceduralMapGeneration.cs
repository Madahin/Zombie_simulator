using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;

public class ProceduralMapGeneration : MonoBehaviour {

    public uint nbSkyscrapper = 50;
    public uint nbHouse = 50;
    public uint nbCampagneHouse = 50;
    public uint nbLighTorch = 300;
    public uint nbHuman = 200;

    public uint cityCenterPerimeter = 50;
    public uint cityBanlieurPerimeter = 100;
    public uint cityCampagnePerimeter = 150;

    public uint houseWidth = 5;
    public uint houseDepth = 5;
    public uint houseHeight = 5;

    public int houseErrorBase = 3;
    public int houseErrorHeight = 4;

    public uint skyscrapperWidth = 8;
    public uint skyscrapperDepth = 8;
    public uint skyscrapperHeight = 15;

    public int skyscrapperErrorBase = 4;
    public int skyscrapperErrorHeight = 6;

    private GameObject m_father;

    private List<GameObject> m_buildings;

    private System.Random m_randomEngine = null;

    public Material mur;
    public Material mur1;
    public Material immeuble;
    public Material road;
    public GameObject Fire;
    public GameObject humanPrefab;

    public void BuildObject()
    {
        var watch = System.Diagnostics.Stopwatch.StartNew();
        Reset();
        m_father = new GameObject("City");

        immeuble.mainTexture.wrapMode = TextureWrapMode.Repeat;
        mur.mainTexture.wrapMode = TextureWrapMode.Repeat;
        mur1.mainTexture.wrapMode = TextureWrapMode.Repeat;
        road.mainTexture.wrapMode = TextureWrapMode.Repeat;

        uint centerX = 0;
        uint centerY = 0;

        float currentX = centerX;
        float currentY = centerY;

        uint i, j, k;
        i = j = k = 0;

        float position = (int)(cityCenterPerimeter + cityBanlieurPerimeter + cityCampagnePerimeter);
        float demiPosition = position * 0.5f;

        GameObject floor = GameObject.CreatePrimitive(PrimitiveType.Cube);
        floor.name = "Floor";
        floor.transform.parent = m_father.transform;
        floor.transform.localScale = new Vector3(position, 1, position);
        floor.transform.position = new Vector3(0, -0.5f, 0);

        MeshRenderer renderer1 = floor.GetComponent<MeshRenderer>();

        renderer1.sharedMaterial = new Material(road);
        Vector2 size = new Vector2(position, position);
        renderer1.sharedMaterial.mainTextureScale = size;

        while ((i < nbSkyscrapper) || (j < nbHouse) || (k < nbCampagneHouse))
        {
            currentX = (float)(m_randomEngine.NextDouble() * position - demiPosition);
            currentY = (float)(m_randomEngine.NextDouble() * position - demiPosition);

            float dist = Vector2.Distance(Vector2.zero, new Vector2(currentX, currentY));
            if (dist > position) continue;

            GameObject buildingObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            buildingObject.name = "batiment_" + (i + j + k);
            buildingObject.transform.parent = m_father.transform;
            MeshRenderer renderer = buildingObject.GetComponent<MeshRenderer>();

            Rect building = new Rect();
            if (dist < cityCenterPerimeter) // City center
            {
                building.width = skyscrapperWidth + m_randomEngine.Next(-skyscrapperErrorBase, skyscrapperErrorBase);
                building.height = skyscrapperDepth + m_randomEngine.Next(-skyscrapperErrorBase, skyscrapperErrorBase);

                buildingObject.transform.localScale = new Vector3(building.width,
                                                                  skyscrapperHeight + m_randomEngine.Next(-skyscrapperErrorHeight, skyscrapperErrorHeight),
                                                                  building.height);

                
                
                renderer.sharedMaterial = new Material(immeuble);
                Vector2 size1 = new Vector2(building.width*0.5f, building.height*0.5f);
                renderer.sharedMaterial.mainTextureScale = size1;
            }
            else // Banlieu
            {
                building.width = houseWidth + m_randomEngine.Next(-houseErrorBase, houseErrorBase);
                building.height = houseDepth + m_randomEngine.Next(-houseErrorBase, houseErrorBase);

                buildingObject.transform.localScale = new Vector3(building.width,
                                                                  houseHeight + m_randomEngine.Next(-houseErrorHeight, houseErrorHeight),
                                                                  building.height);

                

                int leRandom = m_randomEngine.Next(0,2);
                if (leRandom == 0){ renderer.sharedMaterial = new Material(mur);}
                else{renderer.sharedMaterial = new Material(mur1);}

                Vector2 size2 = new Vector2(building.width*1.25f, building.height);
                renderer.sharedMaterial.mainTextureScale = size2;
            }
            buildingObject.transform.position = new Vector3(currentX, buildingObject.transform.localScale.y / 2, currentY);
            
            if (dist < cityCenterPerimeter)
            {
                if (i < nbSkyscrapper)
                    i += 1;
                else
                    DestroyImmediate(buildingObject);
            }
            else if (dist < cityBanlieurPerimeter)
            {
                if (j < nbHouse)
                    j += 1;
                else
                    DestroyImmediate(buildingObject);
            }
            else
            {
                if (k < nbCampagneHouse)
                    k += 1;
                else
                    DestroyImmediate(buildingObject);
            }
        }

        for(uint n=0; n < nbLighTorch; n++)
        {
            uint proba = (uint)m_randomEngine.Next(100);

            float x, y;

            if(proba < 60)
            {
                x = (float)((m_randomEngine.NextDouble() - 0.5) * cityCenterPerimeter);
                y = (float)((m_randomEngine.NextDouble() - 0.5) * cityCenterPerimeter);
            }
            else if(proba < 85)
            {
                x = cityCenterPerimeter + (float)((m_randomEngine.NextDouble() - 0.5) * cityBanlieurPerimeter);
                y = cityCenterPerimeter + (float)((m_randomEngine.NextDouble() - 0.5) * cityBanlieurPerimeter);
            }
            else
            {
                x = cityCenterPerimeter + cityBanlieurPerimeter + (float)((m_randomEngine.NextDouble() - 0.5) * cityCampagnePerimeter);
                y = cityCenterPerimeter + cityBanlieurPerimeter + (float)((m_randomEngine.NextDouble() - 0.5) * cityCampagnePerimeter);
            }

            RaycastHit[] hits = Physics.BoxCastAll(new Vector3(x, 0, y), new Vector3(2, 2, 2), Vector3.down);
            hits = Array.FindAll(hits, (o) => (o.transform.gameObject.name.Contains("batiment_")));

            if (hits.Length == 0)
            {
                GameObject l = Instantiate<GameObject>(Fire);
                l.transform.position = new Vector3(x, 0.44f, y);
                l.transform.localScale = new Vector3(0.05736747f, 0.05736747f, 0.05736747f);
                l.transform.parent = m_father.transform;
            }
            else
            {
                n--;
            }
        }

        for (uint n = 0; n < nbHuman; n++)
        {
            uint proba = (uint)m_randomEngine.Next(100);

            float x, y;

            if (proba < 80)
            {
                x = (float)((m_randomEngine.NextDouble() * cityCenterPerimeter) - cityCenterPerimeter * 0.5f);
                y = (float)((m_randomEngine.NextDouble() * cityCenterPerimeter) - cityCenterPerimeter * 0.5f);
            }
            else
            {
                x = (float)(m_randomEngine.NextDouble() * position - demiPosition);
                y = (float)(m_randomEngine.NextDouble() * position - demiPosition);
            }

            RaycastHit[] hits = Physics.BoxCastAll(new Vector3(x, 0, y), new Vector3(2, 2, 2), Vector3.down);
            hits = Array.FindAll(hits, (o) => (o.transform.gameObject.name.Contains("batiment_")));

            if (hits.Length == 0)
            {
                GameObject l = Instantiate<GameObject>(humanPrefab);
                l.name = "Human_" + n;
                l.transform.position = new Vector3(x, 0f, y);
                l.transform.localScale = new Vector3(0.09536505f, 0.09536505f, 0.09536505f);
            }
            else
            {
                n--;
            }
        }
        watch.Stop();
        var elapsedMs = watch.ElapsedMilliseconds;
        Debug.Log("Generation time : " + elapsedMs / 1000 + "s");
    }

    public void Reset()
    {
        if (m_father != null)
        {
            DestroyImmediate(m_father);
            m_father = null;
        }

        if(m_randomEngine == null)
        {
            m_randomEngine = new System.Random();
        }

        if(m_buildings == null)
        {
            m_buildings = new List<GameObject>();
        }
        else
        {
            m_buildings.Clear();
        }
    }
}
