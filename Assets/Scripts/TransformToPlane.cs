using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformToPlane : MonoBehaviour
{
    public GameObject ObjectOnSphere;
    public float CoronalPos;
    public float SagittalPos;
    public float XPos;
    public float ZPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(ObjectOnSphere.GetComponent<Transform>().position.y > 0){
            if( ObjectOnSphere.GetComponent<Transform>().position.x > ObjectOnSphere.GetComponent<Transform>().position.y ){
                CoronalPos = 1- Mathf.Atan( ObjectOnSphere.GetComponent<Transform>().position.y / ObjectOnSphere.GetComponent<Transform>().position.x ) * 2 / Mathf.PI ;
            }
            else if( ObjectOnSphere.GetComponent<Transform>().position.x < ObjectOnSphere.GetComponent<Transform>().position.y ){
                CoronalPos = Mathf.Atan( ObjectOnSphere.GetComponent<Transform>().position.x / ObjectOnSphere.GetComponent<Transform>().position.y ) * 2 / Mathf.PI ;
            }
            if( (ObjectOnSphere.GetComponent<Transform>().position.z - 20f) > ObjectOnSphere.GetComponent<Transform>().position.y ){
                SagittalPos = 1- Mathf.Atan( ObjectOnSphere.GetComponent<Transform>().position.y / (ObjectOnSphere.GetComponent<Transform>().position.z - 20f) ) * 2 / Mathf.PI ;
            }
            else if( (ObjectOnSphere.GetComponent<Transform>().position.z - 20f) < ObjectOnSphere.GetComponent<Transform>().position.y ){
                SagittalPos = Mathf.Atan( (ObjectOnSphere.GetComponent<Transform>().position.z - 20f) / ObjectOnSphere.GetComponent<Transform>().position.y ) * 2 / Mathf.PI ;
            }
            ZPos = Mathf.Sign(SagittalPos)*15*Mathf.Sqrt( ( SagittalPos*SagittalPos )*( CoronalPos*CoronalPos - 1 ) /  ( SagittalPos*CoronalPos*SagittalPos*CoronalPos - 1 )  );
            XPos = Mathf.Sign(CoronalPos)*15*Mathf.Sqrt( ( CoronalPos*CoronalPos )*( SagittalPos*SagittalPos - 1 ) /  ( SagittalPos*CoronalPos*SagittalPos*CoronalPos - 1 )  );
        
        }
        else {
            XPos = -100;
            ZPos = 0;
        }


        

        GetComponent<Transform>().position = new Vector3 ( XPos ,0.15f , ZPos);
    }
}
