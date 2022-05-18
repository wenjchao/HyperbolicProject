using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{

    public Matrix4x4 scaleMatrix;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 scale = new Vector3(1, 2, 1);
        scaleMatrix = Matrix4x4.Scale(scale);

        Debug.Log(scaleMatrix);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
