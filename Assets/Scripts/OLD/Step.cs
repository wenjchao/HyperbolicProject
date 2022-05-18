using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject stepPrefab;
    public Vector3[] Stepslocation;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)){
            Instantiate( stepPrefab , new Vector3(GetComponent<Transform>().position.x, GetComponent<Transform>().position.y-0.43f ,GetComponent<Transform>().position.z) , GetComponent<Transform>().rotation  );
            
            //Stepslocation. = 
        }
    }

}
