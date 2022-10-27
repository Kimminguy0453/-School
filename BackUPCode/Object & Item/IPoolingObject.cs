using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolingObject
{
    void Initialize(object value);
    void SetPosition(Vector3 pos);
}
