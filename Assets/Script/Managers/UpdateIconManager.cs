using UnityEngine;

public class UpdateIconManager : MonoBehaviour
{
    [SerializeField] private GameManager _gm;
    [SerializeField] private Board _board;

    private int _width, _height;

    private void Start()
    {
        _width = _gm.Width;
        _height = _gm.Height;
    }


    private void PrintAllBlockNames()
    {
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                print(_board.allBlocks[i, j].name);
            }
        }
    }
}
