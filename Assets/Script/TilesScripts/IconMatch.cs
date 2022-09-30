using System;
using UnityEngine;

public class IconMatch : MonoBehaviour
{
    private Board _board;

    private GameManager _gm;

    private int _height, _width;

    private bool _isChecked;

    private void Start()
    {
        GetBoardAndGM();
        GetHeightAndWidth();
    }

    private void GetBoardAndGM()
    {
        _board = GetComponent<Board>();
        _gm = GameManager.Instance;
    }

    private void GetHeightAndWidth()
    {
        _height = _gm.Height;
        _width = _gm.Width;
    }



//  TODO: kullanilacak :)
    private int CheckIcons()
    {
        if (_isChecked) return 1;

        int sum = 1;

        GetComponent<GetSideBlock>()
            .GetSideBlocks(out var leftBlock1, out var rightBlock1, out var upBlock1, out var downBlock1);

        sum += CheckForIcon(leftBlock1, rightBlock1, upBlock1, downBlock1);

        return sum;
    }

    private int CheckForIcon(GameObject leftBlock1, GameObject rightBlock1, GameObject upBlock1, GameObject downBlock1)
    {
        int sum = 0;
        sum += CheckIconBlockMatch(leftBlock1);
        sum += CheckIconBlockMatch(rightBlock1);
        sum += CheckIconBlockMatch(upBlock1);
        sum += CheckIconBlockMatch(downBlock1);
        return sum;
    }

    private int CheckIconBlockMatch(GameObject block)
    {
        if (block == null) return 0;

        if (block.CompareTag(gameObject.tag) && !block.GetComponent<IconMatch>()._isChecked)
        {
            _isChecked = true;
            block.GetComponent<IconMatch>()._isChecked = true;
            return 1;
        }

        return 0;
    }
}