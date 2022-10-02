using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Board : MonoBehaviour
{

    [SerializeField] private GameObject tilePrefab;

    [SerializeField] private UpdateIconManager updateIconManager;
    
    public GameObject[,] allBlocks;
    
    private GameManager _gm;
    
    private int _width, _height;

    private List<TileAndTag> _selectedTiles = new List<TileAndTag>();

    private void OnEnable()
    {
        EventManager.OnBlockDestroyingActions += OnBlockDestroyingAction;
    }

    private void OnDisable()
    {
        EventManager.OnBlockDestroyingActions -= OnBlockDestroyingAction;
    }

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
        _width = _gm.Width;
        _height = _gm.Height;
        allBlocks = new GameObject[_width, _height];
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
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
               var obj = CreateTileAt(i, j);
               var tet = obj.GetComponents<Initializer>();
               foreach (var initializer in tet)
               {
                   initializer.Initialize();
               }
            }
        }
    }

    private GameObject CreateTileAt(int i, int j)
    {
        var tile = Instantiate(tilePrefab, new Vector2(i, j), Quaternion.identity);
        tile.transform.SetParent(transform);
        tile.name = "( " + i + ", " + j + " )";
        allBlocks[i, j] = tile;
        tile.GetComponent<Tile>().UpdateValues(j, i);
        return tile;
    }

    private void SetUpCamera()
    {
        float x = _width % 2 == 0 ? (_width / 2) - .5f : (_width / 2);
        float y = _height % 2 == 0 ? (_height / 2) - .5f : (_height / 2);
        
        Camera.main.transform.position = new Vector3(x, y, -10);
    }

    private void CheckRows()
    {
        int nullCount = 0;
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                if (allBlocks[i,j] == null)
                    nullCount++;
                else if (nullCount > 0)
                    allBlocks[i,j].GetComponent<Collaps>().MoveDownBlock(nullCount);
            }
            nullCount = 0;
        }
    }

    private void RefillBoard()
    {
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                if (allBlocks[i,j] == null)
                {
                    var obj = CreateTileAt(i, j);
                    var tet = obj.GetComponents<Initializer>();
                    foreach (var initializer in tet)
                    {
                        initializer.Initialize();
                    }
                }
            }
        }
        updateIconManager.SetIcons();
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            EventManager.OnBlockDestroyingActions?.Invoke();
        }
    }

    private void OnBlockDestroyingAction()
    {
        CheckRows();
        RefillBoard();
    }

    public TileAndTag GetRandomTileFromSelectedTiles => _selectedTiles[Random.Range(0,_selectedTiles.Count)];
}