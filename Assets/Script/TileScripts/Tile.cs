using DG.Tweening;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Tile : Initializer
{
    public int Height, Width;

    [SerializeField] private SpriteRenderer _spriteRenderer;

    private TileAndTag _tileAndTag;

    private GameManager _gm;

    private Board _board;

    public override void Initialize()
    {
        _gm = GameManager.Instance;

        _board = GetComponentInParent<Board>();

        _tileAndTag = GetComponentInParent<Board>().GetRandomTileFromSelectedTiles;

        _spriteRenderer.sprite = _tileAndTag.SpriteDefault;
        gameObject.tag = _tileAndTag.Tag;

        var renderingOrder = Height;
        _spriteRenderer.sortingOrder = renderingOrder;

        SlideInAnimation();
    }

    private void SlideInAnimation()
    {
        transform.DOLocalMoveY(Height + _gm.SpawnOffset, _gm.FallSpeed).SetSpeedBased().From().SetEase(Ease.InQuad)
            .OnStart(() => { _board.CanGetInput = false; }).OnComplete(() =>
            {
                transform.DOMoveY(transform.position.y - .10f, .065f).SetLoops(2, LoopType.Yoyo)
                    .OnComplete(() => _board.CanGetInput = true);
            });
    }

    public void UpdateValues(int height, int width)
    {
        Height = height;
        Width = width;
    }

    public Sprite Get_A_ConditionSprite => _tileAndTag.SpriteA;
    public Sprite Get_B_ConditionSprite => _tileAndTag.SpriteB;
    public Sprite Get_C_ConditionSprite => _tileAndTag.SpriteC;
    public Sprite Get_Default_ConditionSprite => _tileAndTag.SpriteDefault;
}