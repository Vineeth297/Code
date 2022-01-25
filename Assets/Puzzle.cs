using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    [SerializeField] private int gridSize;
    
    private void Start()
    {
        CreatePuzzle();
    }

    private void CreatePuzzle()
    {
        for (var y = 0; y < gridSize; y++)
        {
            for (var x = 0; x < gridSize; x++)
            {
                var quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
                quad.transform.position = -Vector2.one * (gridSize - 1) * 0.5f + new Vector2(x, y);
                quad.transform.parent = transform;
            }
        }

        if (Camera.main != null) Camera.main.orthographicSize = gridSize * 0.55f;
    }
}
