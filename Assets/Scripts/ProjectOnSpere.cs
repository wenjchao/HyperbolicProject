using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectOnSpere : MonoBehaviour
{
    public GameObject Player;
    public Vector3 Pos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Pos = Player.GetComponent<TransformToSphere>().PosOnSphere;
        transform.position = Pos*5 + new Vector3(0f,0f,20f) ;
    }
}
