using UnityEngine;

public class Tile : MonoBehaviour
{

    private SpriteRenderer _spriteRenderer;

    private TileAndTag _tileAndTag;
    
    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _tileAndTag = GameManager.Instance.GetRandomTileAndTag;
        
        _spriteRenderer.sprite = _tileAndTag.SpriteDefault;
        gameObject.tag = _tileAndTag.Tag;

        var renderingOrder = (int)transform.position.y;
        _spriteRenderer.sortingOrder = renderingOrder;
    }

    // public Sprite GetAConditionSprite()
    // {
    //     return _tileAndTag.SpriteA;
    // }
    
    public Sprite GetAConditionSprite => _tileAndTag.SpriteA;
    public Sprite GetBConditionSprite => _tileAndTag.SpriteB;
    public Sprite GetCConditionSprite => _tileAndTag.SpriteC;
    public Sprite GetDefaultConditionSprite => _tileAndTag.SpriteDefault;
}