using UnityEngine;

public class Collaps : Initializer
{
    private Board _board;

    private Tile _tile;
    
    private int _curHeight, _curWidth;

    private SpriteRenderer _renderer;

    private GetSideBlock _sideBlock;
    
    private void Start()
    {
        Initialize();
    }

    public override void Initialize()
    {
        _board = GetComponentInParent<Board>();
        _tile = GetComponent<Tile>();
        _renderer = GetComponent<SpriteRenderer>();
        _sideBlock = GetComponent<GetSideBlock>();

        _curHeight = _tile.Height;
        _curWidth = _tile.Width;
    }

    public void MoveDownBlock(int moveDownAmount)
    {
        var desPos = UpdatePos(moveDownAmount);
        UpdateArrayPos((int)desPos.y);
        UpdateSpriteRenderingOrder((int)desPos.y);
        _tile.UpdateValues(_curHeight,_curWidth);
        _sideBlock.UpdateValues(_curHeight,_curWidth);
    }

    private Vector3 UpdatePos(int moveDownAmount)
    {
        var desPos = transform.position;
        desPos.y -= moveDownAmount;
        transform.position = desPos;
        return desPos;
    }

    private void UpdateSpriteRenderingOrder(int newOrder)
    {
        _renderer.sortingOrder = newOrder;
    }

    private void UpdateArrayPos(int newPosY)
    {
        _board.allBlocks[_curWidth, newPosY] = _board.allBlocks[_curWidth, _curHeight];
        _board.allBlocks[_curWidth, _curHeight] = null;
        _curHeight = newPosY;
        gameObject.name = "( " + _curWidth + ", " + _curHeight + " )";
    }
}
