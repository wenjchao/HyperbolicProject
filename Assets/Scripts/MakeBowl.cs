using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeBowl : MonoBehaviour
{
    /*
    Mesh mesh;
    public int outercircle;
    public int circularnum;
    public int depth;
    public int depthnum;
    public float depdensity;
    public float cirdensity;
    Vector3[] vertices;
    int[] triangles;
    public int divisions;
    public int num;
    public Vector3[] playerlocat;
    bool lft;
    bool rght;
    bool frwrd;
    bool bckwrd;


    void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        //float temp = Mathf.PI * 0.5f / density ;
        //cellnum = (int) temp + 1 ;
        float temp = depthnum;
        depdensity = Mathf.PI * 0.5f / temp;
        temp = circularnum * divisions;
        cirdensity = Mathf.PI * 2.0f / temp;
    }
    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        Initial();
        UpdateMesh();
    }

    // Update is called once per frame
    void Update()
    {
        Sense();
    }

    void FixedUpdate()
    {
        //UpdateMesh();
        
    }

    void Initial()
    {
        vertices = new Vector3[ (circularnum + 1) * ( depthnum + 1 ) ];
        triangles = new int [ circularnum * depthnum * 6 ];

        for ( int i = 0 ; i <= circularnum ; i++ ){
            float tempsini = Mathf.Sin( ( i + circularnum * num ) * cirdensity );
            float tempcosi = Mathf.Cos( ( i + circularnum * num ) * cirdensity );

            for( int j = 0 ; j <= depthnum ; j++ ){
                float tempcosj = Mathf.Cos( j * depdensity );
                float tempsinj = Mathf.Sin( j * depdensity );
                
                vertices[ i*( depthnum+1 ) + j ] = new Vector3( outercircle * tempcosj * tempcosi , -depth*tempsinj , outercircle * tempcosj * tempsini );
            }
        }


        for ( int i = 0 ; i < circularnum ; i++ ){
            for( int j = 0 ; j < depthnum  ; j++ ){
                triangles[ 0 + j*6 + i*depthnum*6 ] = 0 + j + i * (depthnum+1) ;
                triangles[ 1 + j*6 + i*depthnum*6 ] = triangles[ 4 + j*6 + i*depthnum*6 ] = 1 + j + i * (depthnum+1);
                triangles[ 2 + j*6 + i*depthnum*6 ] = triangles[ 3 + j*6 + i*depthnum*6 ] = depthnum + 1 + j + i * (depthnum+1);
                triangles[ 5 + j*6 + i*depthnum*6 ] = depthnum + 1 + j + 1 + i * (depthnum+1);
            }
        }

    }

    void UpdateMesh(){
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }

    void Sense(){

        if (Input.GetKey(KeyCode.LeftArrow)) lft = true;
        else lft = false;

        if (Input.GetKey(KeyCode.RightArrow)) rght = true;
        else rght = false; 

        if (Input.GetKey(KeyCode.UpArrow)) frwrd = true;
        else frwrd = false;

        if (Input.GetKey(KeyCode.DownArrow)) bckwrd = true;
        else bckwrd = false;
        
    }
*/
}