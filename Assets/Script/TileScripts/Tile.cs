using UnityEngine;

public class Tile : MonoBehaviour
{
    [HideInInspector] public int Height,Width;

    private SpriteRenderer _spriteRenderer;

    private TileAndTag _tileAndTag;
    
    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _tileAndTag = GetComponentInParent<Board>().GetRandomTileFromSelectedTiles;
        
        _spriteRenderer.sprite = _tileAndTag.SpriteDefault;
        gameObject.tag = _tileAndTag.Tag;

        var renderingOrder = (int)transform.position.y;
        _spriteRenderer.sortingOrder = renderingOrder;
    }

    public Sprite Get_A_ConditionSprite => _tileAndTag.SpriteA;
    public Sprite Get_B_ConditionSprite => _tileAndTag.SpriteB;
    public Sprite Get_C_ConditionSprite => _tileAndTag.SpriteC;
    public Sprite GetDefaultConditionSprite => _tileAndTag.SpriteDefault;
}