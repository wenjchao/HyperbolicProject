using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeingProjected : MonoBehaviour
{
    public GameObject real;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Transform>().position = real.GetComponent<SpinandProject>().POS;
    }
}
