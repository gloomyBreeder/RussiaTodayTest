using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpaceProceduralGenerator : BasicManager<SpaceProceduralGenerator>
{
    Terrain _terrain;

    // width of line in grid
    [SerializeField]
    float _lineWidth = 10f;

    [SerializeField]
    int _rowCount = 10;

    [SerializeField]
    int _columnCount = 10;

    [SerializeField]
    int _gridTextureIndex = 1;

    [SerializeField]
    GameObject _cellPrefab;

    [SerializeField]
    Transform _grid;

    [SerializeField]
    GameObject _spaceshipPrefab;

    Vector2 _currentCellSize;

    List<GameObject> _cells = new List<GameObject>();

    void Start()
    {
        _terrain = GetComponent<Terrain>();
        GenerateGridWithCells();
        UpdateTerrain(_terrain.terrainData);
        PlanetManager.instance.PlacePlanets(_cells, _currentCellSize);
        PlanetManager.instance.GeneratePlanetRatings();
        PlacePlayer();
    }

    void PlacePlayer()
    {
        // get middle coors of terrain
        Vector3 middlePos = new Vector3(_terrain.terrainData.size.x / 2, 0, _terrain.terrainData.size.z / 2);
        List<GameObject> orderedCells = _cells;
        // find first cell which is near to middle coors of terrain and doesn't have planet
        GameObject middleCell = orderedCells.OrderBy(cell => Vector3.Distance(middlePos, cell.transform.position)).First(c => !c.GetComponent<CellBase>().HasPlanet);
        GameObject ship = Instantiate(_spaceshipPrefab, middleCell.transform);
        ship.GetComponent<SpaceshipController>().SetCells(_cells, _currentCellSize.x);
        ship.transform.localScale = new Vector3(_currentCellSize.y, transform.localScale.y, _currentCellSize.x);
        CameraController.instance.PlaceCamera(ship.transform);
        
    }

    void UpdateTerrain(TerrainData data)
    {
        // all textures of terrain
        float[,,] alphas = data.GetAlphamaps(0, 0, data.alphamapWidth, data.alphamapHeight);
        for (int i = 0; i < data.alphamapWidth; i++)
        {
            for (int j = 0; j < data.alphamapHeight; j++)
            {
                if (OnGrid(i, j))
                {
                    for (int k = 0; k < data.alphamapLayers; k++)
                    {
                        alphas[i, j, k] = 0f;
                    }
                    alphas[i, j, _gridTextureIndex] = 1f;
                }
            }
        }
        data.SetAlphamaps(0, 0, alphas);
    }

    bool OnGrid(int i, int j)
    {
        return HorizontalCheck(i) || VerticalCheck(j);
    }

    bool HorizontalCheck(int i)
    {
        TerrainData data = _terrain.terrainData;
        float terrainRealWidth = data.size.x;
        
        int lineWidth = (int)(_lineWidth / terrainRealWidth * data.alphamapWidth);

        float linePos = data.alphamapWidth / _columnCount;
        float linePosPrev = linePos * (int)(i / linePos);
        float linePosNext = linePos * (int)(i / linePos + 1);

        return (i - linePosPrev <= lineWidth / 2 || linePosNext - i <= lineWidth / 2);
    }

    bool VerticalCheck(int j)
    {
        TerrainData data = _terrain.terrainData;
        float terrainRealHeight = data.size.z;
        int lineHeight = (int)(_lineWidth / terrainRealHeight * data.alphamapHeight);

        float linePos = data.alphamapHeight / _rowCount;
        float linePosPrev = linePos * (int)(j / linePos);
        float linePosNext = linePos * (int)(j / linePos + 1);

        return (j - linePosPrev <= lineHeight / 2 || linePosNext - j <= lineHeight / 2);
    }

    void GenerateGridWithCells()
    {
        for (int i = 0; i < _columnCount; i++)
        {
            for (int j = 0; j < _rowCount; j++)
            {
                GameObject cellObj = Instantiate(_cellPrefab, _grid);
                CellBase cell = cellObj.GetComponent<CellBase>();
                // set size of cell according to terrain size
                cell.CellSize = _currentCellSize = new Vector2(_terrain.terrainData.size.x / _columnCount, _terrain.terrainData.size.z / _rowCount);
                cell.Index = new Vector2Int(i, j);
                cell.SetPositionAndSize();
                _cells.Add(cellObj);
            }
        }
    }
}
