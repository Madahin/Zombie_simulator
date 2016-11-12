using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;

public class ProceduralMapGeneration : MonoBehaviour {

    public uint nbSkyscrapper = 50;
    public uint nbHouse = 50;
    public uint nbCampagneHouse = 50;

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

    public void BuildObject()
    {
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

        renderer1.material = new Material(road);
        Vector2 size = new Vector2(position, position);
        renderer1.material.mainTextureScale = size;

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

                
                
                renderer.material = new Material(immeuble);
                Vector2 size1 = new Vector2(building.width*0.5f, building.height*0.5f);
                renderer.material.mainTextureScale = size1;
            }
            else // Banlieu
            {
                building.width = houseWidth + m_randomEngine.Next(-houseErrorBase, houseErrorBase);
                building.height = houseDepth + m_randomEngine.Next(-houseErrorBase, houseErrorBase);

                buildingObject.transform.localScale = new Vector3(building.width,
                                                                  houseHeight + m_randomEngine.Next(-houseErrorHeight, houseErrorHeight),
                                                                  building.height);

                

                int leRandom = m_randomEngine.Next(0,2);
                if (leRandom == 0){ renderer.material = new Material(mur);}
                else{renderer.material = new Material(mur1);}

                Vector2 size2 = new Vector2(building.width*1.25f, building.height);
                renderer.material.mainTextureScale = size2;
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
