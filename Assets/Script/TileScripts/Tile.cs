using UnityEngine;

public class Tile : Initializer
{
    public int Height,Width;

    [SerializeField] private SpriteRenderer _spriteRenderer;

    private TileAndTag _tileAndTag;

    public override void Initialize()
    {
        _tileAndTag = GetComponentInParent<Board>().GetRandomTileFromSelectedTiles;
        
        _spriteRenderer.sprite = _tileAndTag.SpriteDefault;
        gameObject.tag = _tileAndTag.Tag;

        var renderingOrder = Height;
        _spriteRenderer.sortingOrder = renderingOrder;
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