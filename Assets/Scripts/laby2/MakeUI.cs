using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(MeshFilter),typeof(MeshRenderer))]
public class MakeUI : MonoBehaviour
{
    public GameObject stepPrefab;
    public GameObject pointA;
    public GameObject pointB;
    public float Angle;
    public float num;
    public int intnum;
    public int LeftOrRight;
    public float thickness;
    Mesh mesh;
    Vector3[] vertices;
    public Vector3[] midpoints;
    int[] triangles;
    void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        InitialMesh();
        UpdateMesh();
    }

    void UpdateMesh(){
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
    
    void InitialMesh(){
        Vector3 posA = pointA.GetComponent<Transform>().position;
        Vector3 posB = pointB.GetComponent<Transform>().position;
        Quaternion Rota = GetComponent<Transform>().rotation;
        

        Vector3 BminusA = LeftOrRight * (posB - posA);
        Vector3 verticalBA = new Vector3( BminusA.z, BminusA.y , -BminusA.x ) / Mathf.Tan(Angle / 2 / 180 * Mathf.PI ) ;
        //Debug.Log(verticalBA);
        Vector3 Center = ( posA + posB + verticalBA ) / 2f ;
        //Instantiate (stepPrefab , Center , Rota );
        float Radius = (posA - Center).magnitude;
        //Debug.Log(posA - Center);
        //Debug.Log(Radius);

        vertices = new Vector3[intnum*2+2];
        triangles = new int[intnum*6];
        midpoints = new Vector3[intnum + 1];
        
        float SinTheta = Mathf.Sin(Angle / num / 180 * Mathf.PI);
        float CosTheta = Mathf.Cos(Angle / num / 180 * Mathf.PI);
        Matrix4x4 Rotati = Matrix4x4.zero;

        Rotati.SetColumn(0, new Vector4( CosTheta , 0 , -SinTheta * LeftOrRight , 0 ) );
        Rotati.SetColumn(1, new Vector4( 0 , 1 , 0 , 0 ) );
        Rotati.SetColumn(2, new Vector4( SinTheta * LeftOrRight , 0 , CosTheta , 0 ) );
        Rotati.SetColumn(3, new Vector4( 0 , 0 , 0 , 1 ) );

        Vector3 temp = posA-Center;
        float thickrelative = thickness / temp.magnitude;

        for(int i = 0 ; i < intnum ; i++){
            
            //Instantiate (stepPrefab , temp + Center , Rota );

            midpoints[i] = temp + Center;
            vertices[ 2 * i ] = temp * ( 1-thickrelative * LeftOrRight ) + Center;
            vertices[ 2 * i + 1 ] = temp * ( 1+thickrelative * LeftOrRight ) + Center;

            triangles[ 6 * i ] = 2 * i;
            triangles[ 6 * i + 1 ] = triangles[ 6 * i + 4 ] =  2 * i + 1;
            triangles[ 6 * i + 2 ] = triangles[ 6 * i + 3 ] =  2 * i + 2;
            triangles[ 6 * i + 5 ] = 2 * i + 3;

            temp = Rotati * temp ; 

        }
        
        //Instantiate (stepPrefab , temp + Center , Rota );

        midpoints[ intnum ] = temp + Center;
        vertices[ 2 * intnum ] = temp * ( 1-thickrelative ) + Center;
        vertices[ 2 * intnum + 1 ] = temp * ( 1+thickrelative ) + Center;
    }

    

}
