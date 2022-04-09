using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeRoad : MonoBehaviour
{
    Mesh mesh;
    public Vector2 Pos;
    public float AdjustedPos;
    Vector3[] vertices;
    int[] triangles;
    public float divisions;
    public float num;
    public int circularnum;
    public int innercircle;
    public GameObject eyes;
    public float LowPos;
    public float HighPos;

    void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        
    }
    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        Pos = new Vector2( eyes.GetComponent<Transform>().position.x , eyes.GetComponent<Transform>().position.z );
        if(Mathf.Abs(Pos.x)>Mathf.Abs(Pos.y)){
            AdjustedPos = Mathf.Atan2(Pos.y , Pos.x);
        }
        else AdjustedPos = Mathf.PI*0.5f - Mathf.Atan2(Pos.x , Pos.y);

        if (AdjustedPos < 0) AdjustedPos = AdjustedPos + Mathf.PI * 2f;


        Calculate();
        UpdateMesh();
    }
    // Start is called before the first frame update

    void UpdateMesh(){
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }

    void Calculate(){
        vertices = new Vector3[ 2*circularnum + 2 ];
        triangles = new int[ 6*circularnum ];
        LowPos = num / divisions * 2 * Mathf.PI;
        HighPos = (num + 1) / divisions * 2 * Mathf.PI;
        if(divisions == 1){

        }
        else{
            bool silenced = false;
            float contraHighPos = 0;
            float contraLowPos = 0;

            if( AdjustedPos >= Mathf.PI*0.5f && AdjustedPos <= Mathf.PI*1.5f ){
                if( LowPos > (AdjustedPos + Mathf.PI*0.5f) && HighPos < (AdjustedPos - Mathf.PI*0.5f) ) silenced = true;
                else{
                    if( LowPos < AdjustedPos - Mathf.PI*0.5f )LowPos = AdjustedPos - Mathf.PI*0.5f;
                    if( HighPos > AdjustedPos + Mathf.PI*0.5f )HighPos = AdjustedPos + Mathf.PI*0.5f;
                }
            }

            else if (AdjustedPos < Mathf.PI*0.5f){
                //Debug.Log("AdjustedPos + Mathf.PI*0.5f="+ (AdjustedPos + Mathf.PI*0.5f) );
                //Debug.Log("AdjustedPos + Mathf.PI*1.5f="+ (AdjustedPos + Mathf.PI*1.5f) );
                if( LowPos > (AdjustedPos + Mathf.PI*0.5f) && HighPos < (AdjustedPos + Mathf.PI*1.5f) ) silenced = true;
                else{
                    if( LowPos > (AdjustedPos + Mathf.PI*0.5f) && LowPos < (AdjustedPos + Mathf.PI*1.5f) )LowPos = AdjustedPos + Mathf.PI*1.5f;
                    if( HighPos > (AdjustedPos + Mathf.PI*0.5f) && HighPos < (AdjustedPos + Mathf.PI*1.5f) )HighPos = AdjustedPos + Mathf.PI*0.5f;
                }
            }

            else if (AdjustedPos > Mathf.PI*1.5f){
                if( LowPos > (AdjustedPos - Mathf.PI*1.5f) && HighPos < (AdjustedPos - Mathf.PI*0.5f) ) silenced = true;
                else{
                    if( LowPos > (AdjustedPos - Mathf.PI*1.5f) && LowPos < (AdjustedPos - Mathf.PI*0.5f) )LowPos = AdjustedPos - Mathf.PI*0.5f;
                    if( HighPos > (AdjustedPos - Mathf.PI*1.5f) && HighPos < (AdjustedPos - Mathf.PI*0.5f) )HighPos = AdjustedPos - Mathf.PI*1.5f;
                }
            }

            if (silenced == false){
                contraHighPos = AdjustedPos*2 + Mathf.PI - HighPos;
                contraLowPos = AdjustedPos*2 + Mathf.PI - LowPos;
                while( contraHighPos > Mathf.PI*2 )contraHighPos = contraHighPos - Mathf.PI*2;
                while( contraLowPos > Mathf.PI*2 )contraLowPos = contraLowPos - Mathf.PI*2;
                while( contraHighPos < 0 )contraHighPos = contraHighPos + Mathf.PI*2;
                while( contraLowPos < 0 )contraLowPos = contraLowPos + Mathf.PI*2;

                float density = (HighPos - LowPos)/ (float)circularnum;
                for (int i = 0 ; i <= circularnum ; i++){
                    vertices[2*i] = new Vector3( Mathf.Cos( LowPos + i*density ) , 0 , Mathf.Sin( LowPos + i*density ) )*innercircle;
                    vertices[2*i+1] = new Vector3( Mathf.Cos( contraLowPos - i*density ) , 0 , Mathf.Sin( contraLowPos - i*density ) )*innercircle;
                }

                for (int i = 0 ; i < circularnum ; i++){
                    triangles[ i*6 ] = triangles[ i*6 + 5 ] = 0 + i*2;
                    triangles[ i*6 + 1 ] = 1+ i*2;
                    triangles[ i*6 + 2 ] = triangles[ i*6 + 3 ] = 3+ i*2;
                    triangles[ i*6 + 4 ] = 2+ i*2;
                }
            }

        }
    }

}
