using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lr_Testing : MonoBehaviour
{
    [SerializeField] private Transform[] points;
    [SerializeField] private Ir_LineController lineController;
    // Start is called before the first frame update
    void Start()
    {
        lineController.SetUpLine(points);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
