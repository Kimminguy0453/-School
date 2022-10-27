using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteController : MonoBehaviour
{
    [SerializeField] private List<Section> section;
    private bool fristMove;

    // Start is called before the first frame update
    private void Start()
    {
        fristMove = true;
    }

    // Update is called once per frame
    public Vector3 SetSection()
    {
        if (fristMove)
        {
            section[0].GetRoute(0);
            fristMove = false;
        }
        int random1;
        while (true)
        {
            random1 = Random.Range(0, section.Count);
            if (section[random1].Onactive)
                break;
        }
        int random2 = Random.Range(0, section[random1].routeCount);

        return section[random1].GetRoute(random2);
    }
}
