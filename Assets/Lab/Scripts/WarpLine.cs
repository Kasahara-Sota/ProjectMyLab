using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpLine : MonoBehaviour
{
    [SerializeField]Transform[] _warpObjects;
    LineRenderer _lineRenderer;
    // Start is called before the first frame update
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < _warpObjects.Length; i++)
        {
            Vector3 pos = _warpObjects[i].position;
            _lineRenderer.SetPosition(i,pos);
        }
    }
}
