using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : BasicManager<CameraController>
{
    Transform _target;
    public void PlaceCamera(Transform target)
    {
        _target = target;
        transform.position = new Vector3(_target.position.x, transform.position.y, _target.position.z);
    }

    void Update()
    {
        if (_target == null)
            return;
        transform.position = new Vector3(_target.position.x, transform.position.y, _target.position.z);
    }
}
