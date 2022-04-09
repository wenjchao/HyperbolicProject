using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourDObject : MonoBehaviour
{
    public int w;
    public int x;
    public int y;
    public int z;
    public float width;
    public bool[] Matrix;
    public bool[] Display;
    public GameObject[] cubes;

    public int switches;
    public int layer;
    
    

    // Start is called before the first frame update
    void Start()
    {
        int[] Default = { 0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,
        0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,
        1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,
        0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,

        0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,
        1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,
        1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,
        1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,
        0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,

        0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,
        1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,
        1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,
        1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,
        0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,

        0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,
        1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,
        1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,
        1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,
        0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,

        0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,
        0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,
        1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,
        0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0, };/*

        int[] Default = { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,

        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,

        0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,
        1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,
        1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,
        1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,
        0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,

        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,

        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, };*/
        
        Matrix = new bool[w*x*y*z];
        for(int i = 0 ; i < w*x*y*z ; i ++){
            if(Default[i] == 1) Matrix[i] = true;
            else Matrix[i] = false;
        }
        /*for(int i = 0 ; i < w*x*y*z ; i ++){
            if(i%4==0){
                Matrix[i] = false;
            }
            else Matrix[i] = true;
        }*/
        Display = new bool[x*y*z];
        layer = 0;
        cubes = new GameObject[x*y*z];

        Vector3 pos = new Vector3( 0,0,0 );

        for(int k = 0 ; k < z ; k++){
            for (int j = 0 ; j < y ; j++){
                for (int i = 0; i < x; i++)
                {
                    int temp = i+j*x+k*y*x;
                    cubes[temp] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cubes[temp].transform.position = new Vector3( (i*2-1-x)*width , (j*2-1-y)*width, (k*2-1-z)*width );
                    cubes[temp].name = "Cube_" + temp;
                }
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))layer--;
        if (Input.GetKeyDown(KeyCode.RightArrow))layer++;
        
    }
    void FixedUpdate() {
        if( switches == 0 ){
            for (int i = 0 ; i < x*y*z ; i++){
                Display[i] = Matrix [ i + x*y*z*layer ];
                if (Display[i]) cubes[i].SetActive(true);
                else cubes[i].SetActive(false);
            }
        }
        else if (switches ==1){
            for(int k = 0 ; k < z ; k++){
                for (int j = 0 ; j < y ; j++){
                    for (int i = 0; i < x; i++)
                    {
                        int temp = i+j*x+k*y*x;
                        Display[temp] = Matrix [ i+j*x+k*y*x + x*y*z*layer ];
                        if (Display[temp]) cubes[temp].SetActive(true);
                        else cubes[temp].SetActive(false);
                    }
                }
            }
        }
        else if (switches ==2){
            for(int k = 0 ; k < z ; k++){
                for (int j = 0 ; j < y ; j++){
                    for (int i = 0; i < x; i++)
                    {
                        int temp = i+j*x+k*y*x;
                        Display[temp] = Matrix [ i+j*x+layer*y*x + x*y*z*k ];
                        if (Display[temp]) cubes[temp].SetActive(true);
                        else cubes[temp].SetActive(false);
                    }
                }
            }
        }
        else if (switches ==3){
            for(int k = 0 ; k < z ; k++){
                for (int j = 0 ; j < y ; j++){
                    for (int i = 0; i < x; i++)
                    {
                        int temp = i+j*x+k*y*x;
                        Display[temp] = Matrix [ i + layer*x + k*y*x + j*x*y*z ];
                        if (Display[temp]) cubes[temp].SetActive(true);
                        else cubes[temp].SetActive(false);
                    }
                }
            }
        }
        else if (switches ==4){
            for(int k = 0 ; k < z ; k++){
                for (int j = 0 ; j < y ; j++){
                    for (int i = 0; i < x; i++)
                    {
                        int temp = i+j*x+k*y*x;
                        Display[temp] = Matrix [ layer + j*x + k*y*x + i*x*y*z ];
                        if (Display[temp]) cubes[temp].SetActive(true);
                        else cubes[temp].SetActive(false);
                    }
                }
            }
        }

    }
}
