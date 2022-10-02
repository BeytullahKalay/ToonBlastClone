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
    [Range(1,6)][SerializeField] private int numberOfColors = 5;

    [Header("Collapsing Values")]
    [SerializeField] private float spawnOffset = 10f;
    [SerializeField] private float fallSpeed = 15f;
    [SerializeField] private float collapsingSpeed = 15f;

    
    
    [Space(10)]
    
    [SerializeField] private TileAndTag[] tilesAndTags;

    public TileAndTag GetRandomTileAndTag => tilesAndTags[Random.Range(0,tilesAndTags.Length)];

    public int Height => height;
    public int Width => width;
    public int NumberOfColors => numberOfColors;
    public int A_Condition => a;
    public int B_Condition => b;
    public int C_Condition => c;


    public float SpawnOffset => spawnOffset;
    public float CollapsingSpeed => collapsingSpeed;
    public float FallSpeed => fallSpeed;

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
