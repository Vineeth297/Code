using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    [SerializeField] private Texture2D image;
    [SerializeField] private int gridSize;

    private Block _emptyBlock;
    private Queue<Block> _inputs;
    private bool _blockIsMoving;
    
    
    private void Start()
    {
        CreatePuzzle();
    }

    private void CreatePuzzle()
    {
        Texture2D[,] getSlices = ImageSlicer.GetSlices(image, gridSize); // get sliced images
        
        for (var y = 0; y < gridSize; y++)    
        {
            for (var x = 0; x < gridSize; x++)
            {
                var quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
                quad.transform.position = -Vector2.one * (gridSize - 1) * 0.5f + new Vector2(x, y);
                quad.transform.parent = transform;

                Block block = quad.AddComponent<Block>();
                block.OnBlockPressed += BlockPressed;
                block.OnFinishedMoving += OnBlockFinishedMoving;
                //block.coord = new Vector2Int(x,y);
                block.Init(new Vector2Int(x,y), getSlices[x,y]);  // apply the slices to each block
                
                if (y == 0 && x == gridSize - 1)
                {
                    quad.SetActive(false);
                    _emptyBlock = block;
                }
            }
        }

        if (Camera.main != null) Camera.main.orthographicSize = gridSize * 0.55f;
        _inputs = new Queue<Block>();
    }

    private void BlockPressed(Block blockToMove)
    {
        _inputs.Enqueue(blockToMove);
        MakeNextPlayerMove();
    }

    private void MoveBlock(Block blockToMove)
    {
        if ((blockToMove.transform.position - _emptyBlock.transform.position).sqrMagnitude == 1)
        {
            var emptyBlockCoord = _emptyBlock.coord;
            _emptyBlock.coord = blockToMove.coord;
            blockToMove.coord = emptyBlockCoord;
            
            var emptyBlockPosition = _emptyBlock.transform.position;
            _emptyBlock.transform.position = blockToMove.transform.position;
            //blockToMove.transform.position = emptyBlockPosition;   
            blockToMove.MoveToPosition(emptyBlockPosition,0.3f);
            _blockIsMoving = true;
        }
    }

    private void OnBlockFinishedMoving()
    {
        _blockIsMoving = false;
        MakeNextPlayerMove();
    }

    private void MakeNextPlayerMove()
    {
        while(_inputs.Count > 0 && !_blockIsMoving)
            MoveBlock(_inputs.Dequeue());
    }
}
