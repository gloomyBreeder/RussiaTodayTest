using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpaceshipController : BasicManager<SpaceshipController>
{
    List<GameObject> _cells;
    float _cellSize;
    GameObject _cell;
    Vector3 _middlePos;

    public enum Direction
    {
        None,
        Up,
        Down,
        Right,
        Left
    }

    public void SetCells(List<GameObject> cells, float size)
    {
        _cells = cells;
        _cellSize = size;
        _middlePos = transform.position;
    }

    public void Move(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                _cell = _cells.FirstOrDefault(c => c.transform.position.z == transform.position.z + _cellSize && c.transform.position.x == transform.position.x);
                break;
            case Direction.Down:
                _cell = _cells.FirstOrDefault(c => c.transform.position.z == transform.position.z - _cellSize && c.transform.position.x == transform.position.x);
                break;
            case Direction.Right:
                _cell = _cells.FirstOrDefault(c => c.transform.position.z == transform.position.z && c.transform.position.x == transform.position.x + _cellSize);
                break;
            case Direction.Left:
                _cell = _cells.FirstOrDefault(c => c.transform.position.z == transform.position.z && c.transform.position.x == transform.position.x - _cellSize);
                break;
        }

        if (_cell != null)
        {
            transform.position = _cell.transform.position;
            transform.SetParent(_cell.transform);
        }
        else
        {
            // didn't test this
            Debug.Log("you reached the end of a screen");
            List<GameObject> orderedCells = _cells;
            GameObject middleCell = orderedCells.OrderBy(cell => Vector3.Distance(_middlePos, cell.transform.position)).First(c => !c.GetComponent<CellBase>().HasPlanet);
            transform.position = middleCell.transform.position;
            transform.SetParent(middleCell.transform); 
        }

    }

}
