using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planefortwo : MonoBehaviour
{
    public GameObject Target;
    void Update()
    {
        GetComponent<Transform>().position = new Vector3(GetComponent<Transform>().position.x ,GetComponent<Transform>().position.y, Target.GetComponent<Transform>().position.z + 3f );
    }
}
