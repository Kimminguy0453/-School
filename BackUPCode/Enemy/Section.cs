using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Section : MonoBehaviour
{
    private List<Vector3> route;
    public int routeCount { get => route.Count; }
    public bool Onactive { get => this.gameObject.activeSelf; }

    private void Start()
    {
        route = new List<Vector3>();
        for (int i = 0; i < this.transform.childCount; i++)
            route.Add(this.transform.GetChild(i).position);
    }

    public Vector3 GetRoute(int num)
    {
        return route[num];
    }
}
