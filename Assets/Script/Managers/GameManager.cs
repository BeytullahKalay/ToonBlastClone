using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoSingleton<GameManager>
{
    [Header("Condition Values")] 
    [SerializeField] private int a = 4;
    [SerializeField] private int b = 7;
    [SerializeField] private int c = 9;
    
    [Header("Map Values")]
    [SerializeField] private int height = 11;
    [SerializeField] private int width = 5;
    
    
    [SerializeField] private TileAndTag[] tilesAndTags;

    public TileAndTag GetRandomTileAndTag => tilesAndTags[Random.Range(0,tilesAndTags.Length)];

    public int Height => height;
    public int Width => width;

}

[Serializable]
public class TileAndTag
{
    public Sprite SpriteDefault;
    public Sprite SpriteA;
    public Sprite SpriteB;
    public Sprite SpriteC;
    public string Tag;
}