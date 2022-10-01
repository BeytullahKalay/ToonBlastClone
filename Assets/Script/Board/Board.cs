using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{

    [SerializeField] private GameObject tilePrefab;
    
    public GameObject[,] allBlocks;
    
    private GameManager _gm;
    
    private int width, height;

    private List<TileAndTag> _selectedTiles = new List<TileAndTag>();


    private void Awake()
    {
        Initialize();
        SelectTiles();
        SetUpTilesAndNames();
        SetUpCamera();

    }

    private void Initialize()
    {
        _gm = GameManager.Instance;
        width = _gm.Width;
        height = _gm.Height;
        allBlocks = new GameObject[width, height];
    }

    private void SelectTiles()
    {
        while (_selectedTiles.Count < _gm.NumberOfColors)
        {
            var tileAndTag = _gm.GetRandomTileAndTag;
            if (!_selectedTiles.Contains(tileAndTag))
            {
                _selectedTiles.Add(tileAndTag);
            }
        }
    }

    private void SetUpTilesAndNames()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                var tile = Instantiate(tilePrefab, new Vector2(i, j), Quaternion.identity);
                tile.transform.SetParent(transform);
                tile.name = "( " + i + ", " + j + " )";

                allBlocks[i, j] = tile;
                
                tile.GetComponent<Tile>().Width = i;
                tile.GetComponent<Tile>().Height = j;
            }
        }
    }

    private void SetUpCamera()
    {
        float x = width % 2 == 0 ? (width / 2) - .5f : (width / 2);
        float y = height % 2 == 0 ? (height / 2) - .5f : (height / 2);
        
        Camera.main.transform.position = new Vector3(x, y, -10);
    }

    private void CheckRows()
    {
        int nullCount = 0;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (allBlocks[i,j] == null)
                    nullCount++;
                else if (nullCount > 0)
                    allBlocks[i,j].GetComponent<Collaps>().MoveDownBlock(nullCount);
            }
            nullCount = 0;
        }
    }
    
    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.E))
    //     {
    //         print("input getted");
    //         CheckRows();
    //     }
    // }
    
    

    public TileAndTag GetRandomTileFromSelectedTiles => _selectedTiles[Random.Range(0,_selectedTiles.Count)];
}