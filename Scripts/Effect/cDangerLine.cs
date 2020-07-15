using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cDangerLine : MonoBehaviour
{
    TrailRenderer _Line;
    public Vector3 _EndPosition;
    void Start()
    {
        _Line = GetComponent<TrailRenderer>();
        _Line.startColor = new Color(1, 0, 0, 0.6f); 
        _Line.endColor = new Color(1, 0, 0, 0.6f);
        Destroy(gameObject, 3f);
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, _EndPosition, Time.deltaTime * 3.5f);        
    }
}
