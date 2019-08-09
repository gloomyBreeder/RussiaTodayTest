using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextController : MonoBehaviour
{
    Camera _camera;
    Transform _cell;
    public void SetParams(Camera cam, Transform cell)
    {
        _camera = cam;
        _cell = cell;
    }

    void Update()
    {
        // up right corner of cell
        //Vector3 cellPos = cell.GetComponent<Renderer>().bounds.max;

        // doesn't work
        _camera.WorldToScreenPoint(new Vector3(_cell.transform.position.x, 0, _cell.transform.position.z));
    }
}
