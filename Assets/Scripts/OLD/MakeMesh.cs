using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(MeshFilter),typeof(MeshRenderer))]
public class MakeMesh : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject stepPrefab;
    Mesh mesh;
    Vector3[] verticesOnSphere;
    Vector3[] verticesOnPlane;
    int[] triangles;
    public float density;
    public int cellnum;
    bool lft;
    bool rght;
    bool frwrd;
    bool bckwrd;
    public int quadrant;
    public float height;
    float SinSpeed;
    float CosSpeed;

    void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        //float temp = Mathf.PI * 0.5f / density ;
        //cellnum = (int) temp + 1 ;
        float temp = cellnum ;
        density = Mathf.PI * 0.5f / temp  ;
        SinSpeed = Mathf.Sin( Mathf.PI*0.01f );
        CosSpeed = Mathf.Cos( Mathf.PI*0.01f );
    }
    void Start()
    {
        InitialOnSphereDiscr();
        UpdateMesh();
    }

    // Update is called once per frame
    void Update(){
        Sense();
    }

    void FixedUpdate()
    {
        for( int i = 0 ; i < verticesOnSphere.Length ; i++ ){
            verticesOnSphere[i] = SpinSphere( verticesOnSphere[i] );
            verticesOnPlane [i] = Transform2Plane( verticesOnSphere [i] );
            verticesOnPlane [verticesOnSphere.Length + i] = (verticesOnSphere[i] + 4 * Vector3.forward )*5;
        }
        UpdateMesh();
    }
    void InitialOnSphereConti(){
        //Quaternion Rota = GetComponent<Transform>().rotation;
        verticesOnSphere = new Vector3 [ (cellnum + 1) * (cellnum + 1) ];
        verticesOnPlane = new Vector3 [ (cellnum + 1) * (cellnum + 1) * 2 ];
        triangles = new int [ cellnum * cellnum * 6 * 2 ];

        for( int i = 0 ; i <= cellnum  ; i++ ){
            float tempsini = Mathf.Sin( i * density );
            float tempcosi = Mathf.Cos( i * density );
            for ( int j = 0 ; j <= cellnum ; j++ ){
                float tempcosj = Mathf.Cos( Mathf.PI * 0.5f - j * density );
                float tempsinj = Mathf.Sin( Mathf.PI * 0.5f - j * density );

                if(quadrant == 1) verticesOnSphere [ i * (cellnum + 1) + j ] = new Vector3 ( tempsini * tempcosj , tempsinj , tempcosi * tempcosj ) ;
                else if (quadrant == 2) verticesOnSphere [ i * (cellnum + 1) + j ] = new Vector3 ( tempsini * tempcosj , tempsinj , -tempcosi * tempcosj ) ;
                else if (quadrant == 3) verticesOnSphere [ i * (cellnum + 1) + j ] = new Vector3 ( -tempsini * tempcosj , tempsinj , -tempcosi * tempcosj ) ;
                else if (quadrant == 4) verticesOnSphere [ i * (cellnum + 1) + j ] = new Vector3 ( -tempsini * tempcosj , tempsinj , tempcosi * tempcosj ) ;
                else if (quadrant == 5) verticesOnSphere [ i * (cellnum + 1) + j ] = new Vector3 ( tempsini * tempcosj , -tempsinj , tempcosi * tempcosj ) ;
                else if (quadrant == 6) verticesOnSphere [ i * (cellnum + 1) + j ] = new Vector3 ( tempsini * tempcosj , -tempsinj , -tempcosi * tempcosj ) ;
                else if (quadrant == 7) verticesOnSphere [ i * (cellnum + 1) + j ] = new Vector3 ( -tempsini * tempcosj , -tempsinj , -tempcosi * tempcosj ) ;
                else if (quadrant == 8) verticesOnSphere [ i * (cellnum + 1) + j ] = new Vector3 ( -tempsini * tempcosj , -tempsinj , tempcosi * tempcosj ) ;

                verticesOnPlane [ i * (cellnum + 1) + j ] = Transform2Plane( verticesOnSphere [ i * (cellnum + 1) + j ] );
                verticesOnPlane [ (cellnum + 1) * (cellnum + 1) + i * (cellnum + 1) + j ] = ( verticesOnSphere [ i * (cellnum + 1) + j ] + 4 * Vector3.forward )*5 ;
                //Instantiate( stepPrefab , new Vector3 ( tempsini * tempcosj , tempsinj , tempcosi * tempcosj + 4f ) * 5 , Rota );
            }
        }

        for( int i = 0 ; i < cellnum ; i++ ){
            for( int j = 0 ; j < cellnum ; j ++ ){
                if (quadrant == 1 || quadrant == 3 || quadrant == 6 || quadrant == 8 ){
                    triangles [ (i * cellnum + j) * 6 ] = i * (cellnum + 1) + j ;
                    triangles [ (i * cellnum + j) * 6 + 1 ] = triangles [ (i * cellnum + j) * 6 + 4 ] = i * (cellnum + 1) + (j + 1) ;
                    triangles [ (i * cellnum + j) * 6 + 2 ] = triangles [ (i * cellnum + j) * 6 + 3 ] = (i + 1) * (cellnum + 1) + j ;
                    triangles [ (i * cellnum + j) * 6 + 5 ] = (i + 1) * (cellnum + 1) + (j + 1) ;

                    triangles [ cellnum * cellnum * 6 + (i * cellnum + j) * 6 ] = (cellnum + 1) * (cellnum + 1) + i * (cellnum + 1) + j ;
                    triangles [ cellnum * cellnum * 6 + (i * cellnum + j) * 6 + 1 ] = triangles [ cellnum * cellnum * 6 + (i * cellnum + j) * 6 + 4 ] = (cellnum + 1) * (cellnum + 1) + i * (cellnum + 1) + (j + 1) ;
                    triangles [ cellnum * cellnum * 6 + (i * cellnum + j) * 6 + 2 ] = triangles [ cellnum * cellnum * 6 + (i * cellnum + j) * 6 + 3 ] = (cellnum + 1) * (cellnum + 1) + (i + 1) * (cellnum + 1) + j ;
                    triangles [ cellnum * cellnum * 6 + (i * cellnum + j) * 6 + 5 ] = (cellnum + 1) * (cellnum + 1) + (i + 1) * (cellnum + 1) + (j + 1) ;
                }
                if (quadrant == 2 || quadrant == 4 || quadrant == 5 || quadrant == 7 ){
                    triangles [ (i * cellnum + j) * 6 ] = i * (cellnum + 1) + j ;
                    triangles [ (i * cellnum + j) * 6 + 1 ] = triangles [ (i * cellnum + j) * 6 + 4 ] = (i + 1) * (cellnum + 1) + j ;
                    triangles [ (i * cellnum + j) * 6 + 2 ] = triangles [ (i * cellnum + j) * 6 + 3 ] = i * (cellnum + 1) + (j + 1) ;
                    triangles [ (i * cellnum + j) * 6 + 5 ] = (i + 1) * (cellnum + 1) + (j + 1) ;

                    triangles [ cellnum * cellnum * 6 + (i * cellnum + j) * 6 ] = (cellnum + 1) * (cellnum + 1) + i * (cellnum + 1) + j ;
                    triangles [ cellnum * cellnum * 6 + (i * cellnum + j) * 6 + 1 ] = triangles [ cellnum * cellnum * 6 + (i * cellnum + j) * 6 + 4 ] = (cellnum + 1) * (cellnum + 1) + (i + 1) * (cellnum + 1) + j ;
                    triangles [ cellnum * cellnum * 6 + (i * cellnum + j) * 6 + 2 ] = triangles [ cellnum * cellnum * 6 + (i * cellnum + j) * 6 + 3 ] = (cellnum + 1) * (cellnum + 1) + i * (cellnum + 1) + (j + 1) ;
                    triangles [ cellnum * cellnum * 6 + (i * cellnum + j) * 6 + 5 ] = (cellnum + 1) * (cellnum + 1) + (i + 1) * (cellnum + 1) + (j + 1) ;
                }
            }
        }
    }

    void InitialOnSphereDiscr(){
        Quaternion Rota = GetComponent<Transform>().rotation;
        verticesOnSphere = new Vector3 [ cellnum * cellnum * 4 ];
        verticesOnPlane = new Vector3 [ cellnum * cellnum * 4 * 2 ];
        triangles = new int [ cellnum * cellnum * 6 * 2 ];

        for( int i = 0 ; i <= cellnum  ; i++ ){
            float tempsini = Mathf.Sin( i * density );
            float tempcosi = Mathf.Cos( i * density );

            for ( int j = 0 ; j <= cellnum ; j++ ){
                float tempcosj = Mathf.Cos( Mathf.PI * 0.5f - j * density );
                float tempsinj = Mathf.Sin( Mathf.PI * 0.5f - j * density );

                if(i == 0){
                    if(j == 0) verticesOnSphere [ (2*i)*cellnum + 2*j ] = new Vector3 ( tempsini * tempcosj , tempsinj , tempcosi * tempcosj ) ;
                    else if(j == cellnum) verticesOnSphere [ (2*i)*cellnum + 2*j-1 ] = new Vector3 ( tempsini * tempcosj , tempsinj , tempcosi * tempcosj ) ;
                    else verticesOnSphere [ (2*i)*cellnum + 2*j-1 ] = verticesOnSphere [ (2*i)*cellnum + 2*j ] = new Vector3 ( tempsini * tempcosj , tempsinj , tempcosi * tempcosj ) ;

                }
                else if(i == cellnum){
                    if(j == 0) verticesOnSphere [ (2*i-1)*cellnum*2 + 2*j ] = new Vector3 ( tempsini * tempcosj , tempsinj , tempcosi * tempcosj ) ;
                    else if(j == cellnum) verticesOnSphere [ (2*i-1)*cellnum*2 + 2*j-1 ] = new Vector3 ( tempsini * tempcosj , tempsinj , tempcosi * tempcosj ) ;
                    else verticesOnSphere [ (2*i-1)*cellnum*2 + 2*j-1 ] = verticesOnSphere [ (2*i-1)*cellnum*2 + 2*j ] = new Vector3 ( tempsini * tempcosj , tempsinj , tempcosi * tempcosj ) ;
                }
                else{
                    if(j == 0) verticesOnSphere [ (2*i-1)*cellnum*2 + 2*j ] =verticesOnSphere [ (2*i)*cellnum*2 + 2*j ] = new Vector3 ( tempsini * tempcosj , tempsinj , tempcosi * tempcosj ) ;
                    else if(j == cellnum) verticesOnSphere [ (2*i-1)*cellnum*2 + 2*j-1 ] = verticesOnSphere [ (2*i)*cellnum*2 + 2*j-1 ] = new Vector3 ( tempsini * tempcosj , tempsinj , tempcosi * tempcosj ) ;
                    else verticesOnSphere [ (2*i-1)*cellnum*2 + 2*j-1 ] = verticesOnSphere [ (2*i-1)*cellnum*2 + 2*j ] = verticesOnSphere [ (2*i)*cellnum*2 + 2*j-1 ] = verticesOnSphere [ (2*i)*cellnum*2 + 2*j ] = new Vector3 ( tempsini * tempcosj , tempsinj , tempcosi * tempcosj ) ;
                }

            }

        }

        for( int i = 0 ; i < cellnum ; i++ ){
            for( int j = 0 ; j < cellnum ; j ++ ){

                int Vone = i * 4 * cellnum + 2*j;
                int Vtwo = i * 4 * cellnum + (2*j + 1) ;
                int Vthree = i * 4 * cellnum + 2 * cellnum + 2*j;
                int Vfour = i * 4 * cellnum + 2 * cellnum + (2*j + 1);
                
                if(quadrant == 2 || quadrant == 3 || quadrant == 6 || quadrant == 7 ){
                    verticesOnSphere [ Vone ].z = -verticesOnSphere [ Vone ].z;
                    verticesOnSphere [ Vtwo ].z = -verticesOnSphere [ Vtwo ].z;
                    verticesOnSphere [ Vthree ].z = -verticesOnSphere [ Vthree ].z;
                    verticesOnSphere [ Vfour ].z = -verticesOnSphere [ Vfour ].z;
                }
                if(quadrant == 5 || quadrant == 6 || quadrant == 7 || quadrant == 8 ){
                    verticesOnSphere [ Vone ].y = -verticesOnSphere [ Vone ].y;
                    verticesOnSphere [ Vtwo ].y = -verticesOnSphere [ Vtwo ].y;
                    verticesOnSphere [ Vthree ].y = -verticesOnSphere [ Vthree ].y;
                    verticesOnSphere [ Vfour ].y = -verticesOnSphere [ Vfour ].y;
                }
                if(quadrant == 3 || quadrant == 4 || quadrant == 7 || quadrant == 8 ){
                    verticesOnSphere [ Vone ].x = -verticesOnSphere [ Vone ].x;
                    verticesOnSphere [ Vtwo ].x = -verticesOnSphere [ Vtwo ].x;
                    verticesOnSphere [ Vthree ].x = -verticesOnSphere [ Vthree ].x;
                    verticesOnSphere [ Vfour ].x = -verticesOnSphere [ Vfour ].x;
                }

                verticesOnPlane [ Vone ] = Transform2Plane( verticesOnSphere [ Vone ] );
                verticesOnPlane [ Vtwo ] = Transform2Plane( verticesOnSphere [ Vtwo ] );
                verticesOnPlane [ Vthree ] = Transform2Plane( verticesOnSphere [ Vthree ] );
                verticesOnPlane [ Vfour ] = Transform2Plane( verticesOnSphere [ Vfour ] );

                verticesOnPlane [ cellnum * cellnum * 4 + Vone ] = (verticesOnSphere [ Vone ] + 4 * Vector3.forward ) * 5 ;
                verticesOnPlane [ cellnum * cellnum * 4 + Vtwo ] = (verticesOnSphere [ Vtwo ] + 4 * Vector3.forward ) * 5 ;
                verticesOnPlane [ cellnum * cellnum * 4 + Vthree ] = (verticesOnSphere [ Vthree ] + 4 * Vector3.forward ) * 5 ;
                verticesOnPlane [ cellnum * cellnum * 4 + Vfour ] = (verticesOnSphere [ Vfour ] + 4 * Vector3.forward ) * 5 ;

                if (quadrant == 1 || quadrant == 3 || quadrant == 6 || quadrant == 8 ){
                    triangles [ (i * cellnum + j) * 6 ] = Vone ;
                    triangles [ (i * cellnum + j) * 6 + 1 ] = triangles [ (i * cellnum + j) * 6 + 4 ] = Vtwo ;
                    triangles [ (i * cellnum + j) * 6 + 2 ] = triangles [ (i * cellnum + j) * 6 + 3 ] = Vthree ;
                    triangles [ (i * cellnum + j) * 6 + 5 ] = Vfour ;

                    triangles [ cellnum * cellnum * 6 + (i * cellnum + j) * 6 ] = cellnum * cellnum * 4 + Vone ;
                    triangles [ cellnum * cellnum * 6 + (i * cellnum + j) * 6 + 1 ] = triangles [ cellnum * cellnum * 6 + (i * cellnum + j) * 6 + 4 ] = cellnum * cellnum * 4 + Vtwo ;
                    triangles [ cellnum * cellnum * 6 + (i * cellnum + j) * 6 + 2 ] = triangles [ cellnum * cellnum * 6 + (i * cellnum + j) * 6 + 3 ] = cellnum * cellnum * 4 + Vthree ;
                    triangles [ cellnum * cellnum * 6 + (i * cellnum + j) * 6 + 5 ] = cellnum * cellnum * 4 + Vfour ;
                }
                else if (quadrant == 2 || quadrant == 4 || quadrant == 5 || quadrant == 7 ){
                    triangles [ (i * cellnum + j) * 6 ] = Vone ;
                    triangles [ (i * cellnum + j) * 6 + 1 ] = triangles [ (i * cellnum + j) * 6 + 4 ] = Vthree ;
                    triangles [ (i * cellnum + j) * 6 + 2 ] = triangles [ (i * cellnum + j) * 6 + 3 ] = Vtwo ;
                    triangles [ (i * cellnum + j) * 6 + 5 ] = Vfour ;

                    triangles [ cellnum * cellnum * 6 + (i * cellnum + j) * 6 ] = cellnum * cellnum * 4 + Vone ;
                    triangles [ cellnum * cellnum * 6 + (i * cellnum + j) * 6 + 1 ] = triangles [ cellnum * cellnum * 6 + (i * cellnum + j) * 6 + 4 ] = cellnum * cellnum * 4 + Vthree ;
                    triangles [ cellnum * cellnum * 6 + (i * cellnum + j) * 6 + 2 ] = triangles [ cellnum * cellnum * 6 + (i * cellnum + j) * 6 + 3 ] = cellnum * cellnum * 4 + Vtwo ;
                    triangles [ cellnum * cellnum * 6 + (i * cellnum + j) * 6 + 5 ] = cellnum * cellnum * 4 + Vfour ;
                }
                
            }
        }

    }

    void UpdateMesh(){
        mesh.Clear();
        mesh.vertices = verticesOnPlane;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }

    public Vector3 Transform2Plane( Vector3 VecOnSphere ){

        Vector3 VecOnPlane = new Vector3 ( 0, 0.1f, 0 );
        float CoronalPos  = 0f;
        float SagittalPos = 0f;
        if( VecOnSphere.y > 0){

            if( VecOnSphere.x > VecOnSphere.y ){
                CoronalPos = 1- Mathf.Atan( VecOnSphere.y / VecOnSphere.x ) * 2 / Mathf.PI ;
            }
            else if( VecOnSphere.x < VecOnSphere.y ){
                CoronalPos = Mathf.Atan( VecOnSphere.x / VecOnSphere.y ) * 2 / Mathf.PI ;
            }
            if( VecOnSphere.z  > VecOnSphere.y ){
                SagittalPos = 1- Mathf.Atan( VecOnSphere.y / VecOnSphere.z ) * 2 / Mathf.PI ;
            }
            else if( VecOnSphere.z  < VecOnSphere.y ){
                SagittalPos = Mathf.Atan( VecOnSphere.z / VecOnSphere.y ) * 2 / Mathf.PI ;
            }
            VecOnPlane.z = Mathf.Sign(SagittalPos)*15*Mathf.Sqrt( ( SagittalPos*SagittalPos )*( CoronalPos*CoronalPos - 1 ) /  ( SagittalPos*CoronalPos*SagittalPos*CoronalPos - 1 )  );
            VecOnPlane.x = Mathf.Sign(CoronalPos)*15*Mathf.Sqrt( ( CoronalPos*CoronalPos )*( SagittalPos*SagittalPos - 1 ) /  ( SagittalPos*CoronalPos*SagittalPos*CoronalPos - 1 )  );
            VecOnPlane.y = height;

        }
        else {
            VecOnPlane.y = -1;
        }
        return VecOnPlane;
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
    public Vector3 SpinSphere( Vector3 VecOnSphere ){

        Vector3 axis = Vector3.zero;
        float X;
        float Y;
        float Z;
        Matrix4x4 Coronal = Matrix4x4.zero;
        Matrix4x4 Sagittal = Matrix4x4.zero;
        Vector4 OutAxis;

        X = VecOnSphere.x;
        Y = VecOnSphere.y;
        Z = VecOnSphere.z;

        if( rght && !lft ){
            Coronal.SetColumn(0, new Vector4( CosSpeed , 0 , SinSpeed  , 0 ) );
            Coronal.SetColumn(1, new Vector4(  0 , 1 , 0 , 0 ) );
            Coronal.SetColumn(2, new Vector4( -SinSpeed , 0 , CosSpeed , 0 ) );
            Coronal.SetColumn(3, new Vector4( 0 , 0 , 0 , 1 ) );
        }
        else if ( !rght && lft ){
            Coronal.SetColumn(0, new Vector4( CosSpeed , 0 , -SinSpeed  , 0 ) );
            Coronal.SetColumn(1, new Vector4(  0 , 1 , 0 , 0 ) );
            Coronal.SetColumn(2, new Vector4( SinSpeed , 0 , CosSpeed , 0 ) );
            Coronal.SetColumn(3, new Vector4( 0 , 0 , 0 , 1 ) );
        }
        else{
            Coronal.SetColumn(0, new Vector4( 1 , 0 , 0 , 0 ) );
            Coronal.SetColumn(1, new Vector4( 0 , 1 , 0 , 0 ) );
            Coronal.SetColumn(2, new Vector4( 0 , 0 , 1 , 0 ) );
            Coronal.SetColumn(3, new Vector4( 0 , 0 , 0 , 1 ) );
        }

        if ( frwrd && !bckwrd ){
            Sagittal.SetColumn(0, new Vector4( 1 , 0 , 0 , 0 ) );
            Sagittal.SetColumn(1, new Vector4( 0 , CosSpeed , -SinSpeed , 0 ) );
            Sagittal.SetColumn(2, new Vector4( 0 , SinSpeed , CosSpeed , 0 ) );
            Sagittal.SetColumn(3, new Vector4( 0 , 0 , 0 , 1 ) );
        }
        else if ( bckwrd && !frwrd ){
            Sagittal.SetColumn(0, new Vector4( 1 , 0 , 0 , 0 ) );
            Sagittal.SetColumn(1, new Vector4( 0 , CosSpeed , SinSpeed , 0 ) );
            Sagittal.SetColumn(2, new Vector4( 0 , -SinSpeed , CosSpeed , 0 ) );
            Sagittal.SetColumn(3, new Vector4( 0 , 0 , 0 , 1 ) );
        }
        else{
            Sagittal.SetColumn(0, new Vector4( 1 , 0 , 0 , 0 ) );
            Sagittal.SetColumn(1, new Vector4( 0 , 1 , 0 , 0 ) );
            Sagittal.SetColumn(2, new Vector4( 0 , 0 , 1 , 0 ) );
            Sagittal.SetColumn(3, new Vector4( 0 , 0 , 0 , 1 ) );
        }

        OutAxis = Sagittal * (Coronal * new Vector4(X,Y,Z,1) );
        return new Vector3(OutAxis.x,OutAxis.y,OutAxis.z);
    }

}
