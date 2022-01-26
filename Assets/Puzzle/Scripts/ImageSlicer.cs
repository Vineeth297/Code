using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ImageSlicer 
{
    public static Texture2D[,] GetSlices(Texture2D image, int gridSize)
    {
        int imageSize = Mathf.Min(image.width, image.height);
        int blockSize = imageSize / gridSize;
        
        Texture2D[,] blocks = new Texture2D[gridSize, gridSize];

        for (int y = 0; y < gridSize; y++)
        {
            for (int x = 0; x < gridSize; x++)
            {
                Texture2D block = new Texture2D(blockSize,blockSize);
                block.wrapMode = TextureWrapMode.Clamp;
                block.SetPixels(image.GetPixels(x * blockSize, y * blockSize,blockSize,blockSize));
                block.Apply();
                blocks[x, y] = block;
            }
        }

        return blocks;
    }
}
