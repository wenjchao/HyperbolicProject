using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepPink : MonoBehaviour
{
    public GameObject stepPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)){
            Instantiate( stepPrefab , new Vector3(0f, 5f ,20f) , GetComponent<Transform>().rotation  );
        }
    }
}