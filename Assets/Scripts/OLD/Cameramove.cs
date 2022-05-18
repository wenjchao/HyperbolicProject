using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameramove : MonoBehaviour
{
    public GameObject Target;
    void Update()
    {
        GetComponent<Transform>().position = new Vector3(Target.GetComponent<Transform>().position.x ,Target.GetComponent<Transform>().position.y, Target.GetComponent<Transform>().position.z - 30f );
        GetComponent<Transform>().rotation = Target.GetComponent<Transform>().rotation;
    }
}
