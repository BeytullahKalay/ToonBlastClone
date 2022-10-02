using UnityEngine;
using DG.Tweening;

public class Collaps : Initializer
{
    private Board _board;

    private Tile _tile;
    
    private int _curHeight, _curWidth;

    private SpriteRenderer _renderer;

    private GetSideBlock _sideBlock;

    private GameManager _gm;

    public override void Initialize()
    {
        _board = GetComponentInParent<Board>();
        _tile = GetComponent<Tile>();
        _renderer = GetComponent<SpriteRenderer>();
        _sideBlock = GetComponent<GetSideBlock>();
        _gm = GameManager.Instance;

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

        transform.DOLocalMoveY(desPos.y,_gm.CollapsingSpeed).SetSpeedBased().SetEase(Ease.InQuad).OnComplete(() =>
        {
            transform.DOMoveY(transform.position.y - .10f, .065f).SetLoops(2, LoopType.Yoyo);
        });
        
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
