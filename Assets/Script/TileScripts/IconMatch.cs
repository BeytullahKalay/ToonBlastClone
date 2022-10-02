using System.Collections.Generic;
using UnityEngine;

public class IconMatch : Initializer
{
    [SerializeField] private Tile _tile;
    
    private List<IconMatch> _iconList = new List<IconMatch>();

    private List<GameObject> UpVisitedList = new List<GameObject>();

    private GameManager _gm;
    
    public bool _isChecked;

    public override void Initialize()
    {
        _gm = GameManager.Instance;
    }
    

    public void PrepareIconSet()
    {
        _isChecked = false;
        UpVisitedList.Clear();
        _iconList.Clear();
    }


    private int CheckIcons(List<GameObject> visitList)
    {
        if (_isChecked) return 0;

        _isChecked = true;

        if (!visitList.Contains(gameObject))
            visitList.Add(gameObject);


        int sum = 0;

        GetComponent<GetSideBlock>()
            .GetSideBlocks(out var leftBlock1, out var rightBlock1, out var upBlock1, out var downBlock1);

        sum += CheckForIcon(leftBlock1, rightBlock1, upBlock1, downBlock1, visitList);

        foreach (var block in _iconList)
        {
            sum += block.GetComponent<IconMatch>().CheckIcons(visitList);
        }

        return sum;
    }

    private int CheckForIcon(GameObject leftBlock1, GameObject rightBlock1, GameObject upBlock1, GameObject downBlock1,
        List<GameObject> visitedList)
    {
        int sum = 0;
        sum += CheckIconBlockMatch(leftBlock1, visitedList);
        sum += CheckIconBlockMatch(rightBlock1, visitedList);
        sum += CheckIconBlockMatch(upBlock1, visitedList);
        sum += CheckIconBlockMatch(downBlock1, visitedList);
        return sum;
    }

    private int CheckIconBlockMatch(GameObject block, List<GameObject> visitList)
    {
        if (block == null) return 0;

        if (block.CompareTag(gameObject.tag) && !block.GetComponent<IconMatch>()._isChecked)
        {
            _iconList.Add(block.GetComponent<IconMatch>());

            if (!visitList.Contains(block))
                visitList.Add(block);

            return 1;
        }
        else
            return 0;
    }

    public void CheckIconStuff()
    {
        var t = CheckIcons(UpVisitedList) + 1;
        var conditionA = _gm.A_Condition;
        var conditionB = _gm.B_Condition;
        var conditionC = _gm.C_Condition;

        var sprite = _tile.Get_Default_ConditionSprite;

        if (t > conditionA && t <= conditionB)
            sprite = _tile.Get_A_ConditionSprite;
        else if (t > conditionB && t <= conditionC)
            sprite = _tile.Get_B_ConditionSprite;
        else if (t > conditionC)
            sprite = _tile.Get_C_ConditionSprite;

        foreach (var block in UpVisitedList)
        {
            block.GetComponent<SpriteRenderer>().sprite = sprite;
        }
    }


}