using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public event System.Action<Block> OnBlockPressed;
    public event System.Action OnFinishedMoving;

    public Vector2Int coord;
    
    public void Init(Vector2Int startingCoord, Texture2D image) // apply slice to the this block
    {
        coord = startingCoord;
        GetComponent<MeshRenderer>().material.shader = Shader.Find("Unlit/Texture");
        GetComponent<MeshRenderer>().material.mainTexture = image;
    }

    public void MoveToPosition(Vector2 target, float duration)
    {
        StartCoroutine(AnimateMove(target, duration));
    }
    
    private void OnMouseDown()
    {
        OnBlockPressed?.Invoke(this);
    }

    IEnumerator AnimateMove(Vector2 target, float duration)
    {
        Vector2 initialPosition = transform.position;
        float percent = 0;

        while (percent < 1f)
        {
            percent += Time.deltaTime / duration;
            transform.position = Vector2.Lerp(transform.position, target, percent);
            yield return null;
        }
        
        OnFinishedMoving?.Invoke();
    }
}
