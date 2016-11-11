using UnityEngine;
using System;

public class ProceduralMapGeneration : MonoBehaviour {

    public uint worldWidth = 500;
    public uint worldHeight = 500;

    public uint cityCenterPerimeter = 50;

    public uint roadSize = 1;

    public uint houseWidth;
    public uint houseDepth;
    public uint houseHeight;

    public uint skyscrapperWidth;
    public uint skyscrapperDepth;
    public uint skyscrapperHeight;

    private GameObject m_father;

    private uint[,] m_map;

    private System.Random m_randomEngine;

    public void BuildObject()
    {
        Reset();
        m_father = new GameObject("City");

        //m_map = new uint[worldWidth, worldHeight];

        uint centerX = (uint)Mathf.Floor(worldWidth * 0.5f);
        uint centerY = (uint)Mathf.Floor(worldHeight * 0.5f);

        uint currentX = centerX;
        uint currentY = centerY;

        Debug.Log(m_randomEngine.Next(0, 42));

        //while (true)
        {

        }
    }

    public void Reset()
    {
        if (m_father != null)
        {
            DestroyImmediate(m_father);
            m_father = null;
        }
        if(m_randomEngine != null)
        {
            m_randomEngine = new System.Random();
        }
    }
}
