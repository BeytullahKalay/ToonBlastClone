using UnityEngine;

public class Board : MonoBehaviour
{

    [SerializeField] private GameObject tilePrefab;
    
    public GameObject[,] allBlocks;
    
    private GameManager _gm;
    
    private int width, height;


    private void Awake()
    {
        _gm = GameManager.Instance;
        width = _gm.Width;
        height = _gm.Height;
        
        allBlocks = new GameObject[width, height];    

        SetUpTilesAndNames();
        SetUpCamera();

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
            }
        }


    }

    private void SetUpCamera()
    {
        Camera.main.transform.position = new Vector3(width / 2, height / 2, -10);
    }
}