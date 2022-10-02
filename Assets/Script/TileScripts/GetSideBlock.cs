using UnityEngine;

public class GetSideBlock : Initializer
{
    private Board _board;


    public int _row, _column;
    private int _boardHeight, _boardWidth;

    // private void Start()
    // {
    //     Initialize();
    // }

    public override void Initialize()
    {
        _board = GetComponentInParent<Board>();
        _column = GetComponent<Tile>().Width;
        _row = GetComponent<Tile>().Height;
        
        _boardHeight = GameManager.Instance.Height;
        _boardWidth = GameManager.Instance.Width;
    }

    public void UpdateValues(int height, int width)
    {
        _column = width;
        _row = height;
    }

    public void GetSideBlocks(out GameObject leftBlock1, out GameObject rightBlock1, out GameObject upBlock1, out GameObject downBlock1)
    {
        leftBlock1 = _column > 0 ? _board.allBlocks[_column - 1, _row] : null;
        rightBlock1 = _column < _boardWidth - 1 ? _board.allBlocks[_column + 1, _row] : null;
        upBlock1 = _row < _boardHeight - 1 ? _board.allBlocks[_column, _row + 1] : null;
        downBlock1 = _row > 0 ? _board.allBlocks[_column, _row - 1] : null;
    }
}
