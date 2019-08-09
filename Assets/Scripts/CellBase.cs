using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellBase : MonoBehaviour
{
    private Vector2Int _index;
    private Vector2 _cellSize;
    private bool _hasPlanet;

    public Vector2 CellSize { get => _cellSize; set => _cellSize = value; }
    public Vector2Int Index { get => _index; set => _index = value; }
    public bool HasPlanet { get => _hasPlanet; set => _hasPlanet = value; }

    public void SetPositionAndSize()
    {
        Vector3 newPos = new Vector3();
        newPos.x = _index.y * _cellSize.y + 0.5f * _cellSize.y;
        newPos.z = _index.x * _cellSize.x + 0.5f * _cellSize.x;
        newPos.y = 0f;
        transform.position = newPos;
        transform.localScale = new Vector3(_cellSize.y, transform.localScale.y, _cellSize.x);
    }

}
