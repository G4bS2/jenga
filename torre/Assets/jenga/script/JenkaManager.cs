using System.CodeDom.Compiler;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class JenkaManager : MonoBehaviour
{
    [Header("Tower Settings")]
    public int layers = 3;
    public int piecePerLayer = 3;
    public List<GameObject> piecePrefabs;

    [Header("Piece Size Settings")]
    public float pieceSpacing = 0f;
    public float pieceLength = 0.5f;
    public float pieceHeitgh = 0.5f;


    private void Start()
    {
        BuildTower();
    }

    void BuildTower()
    {
        for (int layerIndex = 0; layerIndex < layers; layerIndex++)
        {
            GenerateLayer(layerIndex);
        }
    }

    void GenerateLayer(int layerIndex)
    {
        Vector3 basePosition = transform.position;
        basePosition.y += layerIndex * (pieceHeitgh + pieceSpacing);

        bool isOdd = IsOddLayer(layerIndex);
        Quaternion rotation = isOdd ? Quaternion.Euler(0, 90, 0) : Quaternion.identity;
        Vector3 direction = isOdd ? Vector3.right : Vector3.forward;

        float totalWidth = (piecePerLayer - 1) * (pieceLength + pieceSpacing);
        Vector3 startOffset = -direction * (totalWidth / 2f);

    }

    bool IsOddLayer(int layerIndex)
    {
        return layerIndex % 2 != 0;
    }
}
