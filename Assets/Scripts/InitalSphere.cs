using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitalSphere : MonoBehaviour
{
    public GameObject stepPrefab;
    // Start is called before the first frame update
    void Start()
    {
        float i;
        float j;
        float density = 0.03f ;
        Quaternion Rota = GetComponent<Transform>().rotation ;

        for( i = 0 ; i < Mathf.PI*0.5f ; i = i + density){
            for (j = 0 ; j < Mathf.PI*0.5f ; j = j + density){
                Instantiate( stepPrefab , new Vector3 ( Mathf.Sin(i) * Mathf.Cos(j) , Mathf.Sin(j) , Mathf.Cos(i) * Mathf.Cos(j)+2f ) * 10 , Rota );
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
