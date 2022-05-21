using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeingProjected : MonoBehaviour
{
    public GameObject real;
    SpinandProject sreal;
    // Start is called before the first frame update
    void Start()
    {
        sreal = real.GetComponent<SpinandProject>();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Transform>().position = sreal.POS;
    }
}
