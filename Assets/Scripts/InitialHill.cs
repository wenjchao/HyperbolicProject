using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialHill : MonoBehaviour
{
    Mesh mesh;
    void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
