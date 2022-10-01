using UnityEngine;

public class GetSideBlock : MonoBehaviour
{
    private Board _board;

    
    private int _column, _row,_height, _width;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        _board = GetComponentInParent<Board>();
        // _column = (int)transform.localPosition.x;
        // _row = (int)transform.localPosition.y;
        _column = GetComponent<Tile>().Width;
        _row = GetComponent<Tile>().Height;
        
        _height = GameManager.Instance.Height;
        _width = GameManager.Instance.Width;
    }

    public void GetSideBlocks(out GameObject leftBlock1, out GameObject rightBlock1, out GameObject upBlock1, out GameObject downBlock1)
    {
        leftBlock1 = _column > 0 ? _board.allBlocks[_column - 1, _row] : null;
        rightBlock1 = _column < _width - 1 ? _board.allBlocks[_column + 1, _row] : null;
        upBlock1 = _row < _height - 1 ? _board.allBlocks[_column, _row + 1] : null;
        downBlock1 = _row > 0 ? _board.allBlocks[_column, _row - 1] : null;
    }
}
