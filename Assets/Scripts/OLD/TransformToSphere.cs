using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TransformToSphere : MonoBehaviour
{
    public float sagittal;
    public float coronal;
    public float temp;
    public float Sagdis;
    public float Cordis;
    public float Posdis;
    public Vector3 normSag;
    public Vector3 normCor;
    public Vector3 PosOnSphere;
    public GameObject LocationPrefab;
    public GameObject PlanePrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        sagittal = GetComponent<Transform>().position.x /  Mathf.Sqrt( 225 - GetComponent<Transform>().position.z*GetComponent<Transform>().position.z );
        coronal = GetComponent<Transform>().position.z / Mathf.Sqrt( 225 - GetComponent<Transform>().position.x*GetComponent<Transform>().position.x );
        normSag = new Vector3( -Mathf.Cos( Mathf.PI*sagittal/2 ) , Mathf.Sin( Mathf.PI*sagittal/2 ), 0f  );
        normCor = new Vector3( 0f , -Mathf.Sin( Mathf.PI*coronal/2 ) , Mathf.Cos( Mathf.PI*coronal/2 ) );
        PosOnSphere = Vector3.Cross( normSag , normCor ).normalized;
        Sagdis = normSag.magnitude;
        Cordis = normCor.magnitude;
        Posdis = PosOnSphere.magnitude;
        //Instantiate( PlanePrefab , new Vector3(0,0,20), Quaternion.Euler( 0 , 0 , -90f * sagittal ) );
        //Instantiate( PlanePrefab , new Vector3(0,0,20), Quaternion.Euler( -90f * coronal , 0 , 0 ) );
        //LocationPrefab.GetComponent<Transform>().position.x = 
    }
}
