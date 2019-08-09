using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : BasicManager<CanvasManager>
{
    [SerializeField]
    GameObject _textPrefab;

    [SerializeField]
    Camera _camera;
    public void CreateText(Transform cell, string str)
    {
        GameObject text = Instantiate(_textPrefab, transform);
        //var p = RectTransformUtility.WorldToScreenPoint(Camera.main, cell.GetComponent<Renderer>().bounds.max);

        text.transform.position = _camera.WorldToScreenPoint(new Vector3(cell.transform.position.x, 0, cell.transform.position.z));

        text.GetComponent<TextController>().SetParams(_camera, cell);

        text.GetComponent<Text>().text = str;
    }
}
