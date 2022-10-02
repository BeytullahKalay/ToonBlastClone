using System;
using System.Collections.Generic;
using UnityEngine;

public class BlockMatch : MonoBehaviour
{
    [HideInInspector] public List<GameObject> matchBlocks = new List<GameObject>();
    [HideInInspector] public bool isMatched;

    private Board _board;

    private bool _mouseInput;

    private void Start()
    {
        _board = GetComponentInParent<Board>();
    }

    private void OnMouseDown()
    {
        if (_board.CanGetInput)
            FindMatches(true);
    }

    private void FindMatches(bool mouseInput)
    {
        matchBlocks.Clear();
        _mouseInput = mouseInput;

        GetComponent<GetSideBlock>()
            .GetSideBlocks(out var leftBlock1, out var rightBlock1, out var upBlock1, out var downBlock1);

        CheckBlocksMatch(leftBlock1, rightBlock1, upBlock1, downBlock1);

        foreach (var matchedBlock in matchBlocks)
            matchedBlock.GetComponent<BlockMatch>().FindMatches(false);

        if (matchBlocks.Count >= 1 && mouseInput)
        {
            KillMatch(gameObject);
        }
    }


    private void CheckBlocksMatch(GameObject leftBlock1, GameObject rightBlock1, GameObject upBlock1,
        GameObject downBlock1)
    {
        CheckTheBlockMatch(leftBlock1);
        CheckTheBlockMatch(rightBlock1);
        CheckTheBlockMatch(upBlock1);
        CheckTheBlockMatch(downBlock1);
    }

    private void CheckTheBlockMatch(GameObject block)
    {
        if (block == null) return;

        if (block.CompareTag(gameObject.tag) && !block.GetComponent<BlockMatch>().isMatched)
            Toggle(block);
    }

    private void Toggle(GameObject block)
    {
        isMatched = true;
        block.GetComponent<BlockMatch>().isMatched = true;
        matchBlocks.Add(block);
        KillMatch(block);
    }

    private void KillMatch(GameObject matchObj)
    {
        var tileScript = GetComponent<Tile>();
        matchObj.GetComponentInParent<Board>().allBlocks[tileScript.Width, tileScript.Height] = null;
        Destroy(matchObj);
    }

    private void OnDestroy()
    {
        if (_mouseInput)
            EventManager.OnBlockDestroyingActions?.Invoke();
    }
}