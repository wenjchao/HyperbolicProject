using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourDObject : MonoBehaviour
{
    public int w;
    public int x;
    public int y;
    public int z;
    public float width ;
    public bool[] Matrix;
    public bool[] Display;
    public GameObject[] cubes;

    public int switches;
    public int layer;
    
    

    // Start is called before the first frame update
    void Start()
    {
        Matrix = GetComponent<Save>().cube2;
        Display = new bool[x*y*z];
        layer = 0;
        cubes = new GameObject[x*y*z];

        for (int k = 0; k < z; k++){
            for (int j = 0; j < y; j++){
                for (int i = 0; i < x; i++)
                {
                    int temp = i + j * x + k * y * x;
                    cubes[temp] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cubes[temp].transform.position = new Vector3((i * 2 - 1 - x) * width, (j * 2 - 1 - y) * width, (k * 2 - 1 - z) * width);
                    cubes[temp].name = "Cube_" + temp;
                }
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && layer > 0)layer--;
        if (Input.GetKeyDown(KeyCode.RightArrow) && layer < w-1)layer++;
        
    }

    void FixedUpdate() {
        
        for (int k = 0; k < z; k++){
            for (int j = 0; j < y; j++){
                for (int i = 0; i < x; i++)
                {
                    int temp = i + j * x + k * y * x;

                    if (switches == 1) Display[temp] = Matrix[i + j * x + k * y * x + x * y * z * layer];
                    else if (switches == 2) Display[temp] = Matrix[i + j * x + layer * y * x + x * y * z * k];
                    else if (switches == 3) Display[temp] = Matrix[i + layer * x + k * y * x + j * x * y * z];
                    else if (switches == 4) Display[temp] = Matrix[layer + j * x + k * y * x + i * x * y * z];

                    if (Display[temp]) cubes[temp].SetActive(true);
                    else cubes[temp].SetActive(false);
                }
            }
        }

    }

}
