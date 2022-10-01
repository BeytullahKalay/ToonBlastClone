using UnityEngine;

public class Collaps : MonoBehaviour
{
    private Board _board;

    private Tile _tile;
    
    private int _curHeight, _curWidth;

    private SpriteRenderer _renderer;

    private BlockMatch _blockMatch;

    private IconMatch _iconMatch;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        _board = GetComponentInParent<Board>();
        _tile = GetComponent<Tile>();
        _renderer = GetComponent<SpriteRenderer>();
        _blockMatch = GetComponent<BlockMatch>();
        _iconMatch = GetComponent<IconMatch>();

        _curHeight = _tile.Height;
        _curWidth = _tile.Width;
    }

    public void MoveDownBlock(int moveDownAmount)
    {
        print("on positioning");
        var desPos = UpdatePos(moveDownAmount);
        UpdateArrayPos((int)desPos.y);
        UpdateSpriteRenderingOrder((int)desPos.y);
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
    }
}
