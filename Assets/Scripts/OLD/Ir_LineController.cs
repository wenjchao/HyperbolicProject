using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ir_LineController : MonoBehaviour
{
    // Start is called before the first frame update
    public LineRenderer lr;
    public Transform[] points;
    void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }
    public void SetUpLine(Transform[] points){
        lr.positionCount = points.Length;
        this.points = points;
    }
    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < points.Length; i++){
            lr.SetPosition(i, points[i].position);
            
        }
    }
}
