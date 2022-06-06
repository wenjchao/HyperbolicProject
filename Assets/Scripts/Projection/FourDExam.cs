using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FourDExam : MonoBehaviour
{
    public int w;
    public int x;
    public int y;
    public int z;
    public float width ;
    public bool[] Matrix;
    public bool[] Display;
    public GameObject[] cubes_right;
    public GameObject[] cubes_left;

    public int switches;
    public int layer;

    public GameObject united_right;
    public Transform theunitedTransform_right;
    public GameObject united_left;
    public Transform theunitedTransform_left;
    public Scrollbar rightbar_column;
    public Scrollbar rightbar_row;
    public Scrollbar leftbar_column;
    public Scrollbar leftbar_row;



    // Start is called before the first frame update
    void Start()
    {
        InitialUnitedRight();
        InitialUnitedLeft();
        Matrix = GetComponent<Save>().cube2;
        Display = new bool[x*y*z];
        layer = 0;
        cubes_right = new GameObject[x*y*z];
        cubes_left = new GameObject[x * y * z];

        InitialLeft();
        InitialRight();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && layer > 0)layer--;
        if (Input.GetKeyDown(KeyCode.RightArrow) && layer < w-1)layer++;
        
    }

    void FixedUpdate() {


        SetLeft();
        SetRight();
        
    }

    void InitialUnitedRight()
    {
        united_right = GameObject.CreatePrimitive(PrimitiveType.Cube);
        united_right.name = "unitedright";
        theunitedTransform_right = united_right.transform;
        theunitedTransform_right.position = new Vector3(0, 0, 0);
        theunitedTransform_right.localScale = new Vector3(0.001f, 0.001f, 0.001f);
        Color oldColor = united_right.GetComponent<Renderer>().material.color;
        Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, 0);
        united_right.GetComponent<Renderer>().material.SetColor("_Color", newColor);
        united_right.layer = 6;
    }
    void InitialUnitedLeft()
    {
        united_left = GameObject.CreatePrimitive(PrimitiveType.Cube);
        united_left.name = "unitedleft";
        theunitedTransform_left = united_left.transform;
        theunitedTransform_left.position = new Vector3(0, 0, 0);
        theunitedTransform_left.localScale = new Vector3(0.001f, 0.001f, 0.001f);
        Color oldColor = united_left.GetComponent<Renderer>().material.color;
        Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, 0);
        united_left.GetComponent<Renderer>().material.SetColor("_Color", newColor);
        united_left.layer = 7;
    }

    public void Switchswitches()
    {
        if (switches == 1) switches = 2;
        else if (switches == 2) switches = 3;
        else if (switches == 3) switches = 4;
        else switches = 1;
    }

    void InitialRight()
    {
        for (int k = 0; k < z; k++){
            for (int j = 0; j < y; j++){
                for (int i = 0; i < x; i++)
                {
                    int temp = i + j * x + k * y * x;
                    cubes_right[temp] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cubes_right[temp].transform.position = new Vector3((i * 2 + 1 - x) * width, (j * 2 + 1 - y) * width, (k * 2 + 1 - z) * width);
                    cubes_right[temp].name = "Cuberight (" + temp + ")";
                    cubes_right[temp].transform.parent = united_right.transform;
                    cubes_right[temp].layer = 6;
                }
            }
        }
    }

    void SetRight()
    {
        for (int k = 0; k < z; k++){
            for (int j = 0; j < y; j++){
                for (int i = 0; i < x; i++)
                {
                    int temp = i + j * x + k * y * x;

                    if (switches == 1) Display[temp] = Matrix[i + j * x + k * y * x + x * y * z * layer];
                    else if (switches == 2) Display[temp] = Matrix[i + j * x + layer * y * x + x * y * z * k];
                    else if (switches == 3) Display[temp] = Matrix[i + layer * x + k * y * x + j * x * y * z];
                    else if (switches == 4) Display[temp] = Matrix[layer + j * x + k * y * x + i * x * y * z];

                    if (Display[temp]) cubes_right[temp].SetActive(true);
                    else cubes_right[temp].SetActive(false);
                }
            }
        }
        Quaternion currentrotation = Quaternion.Euler(rightbar_row.value * 360, rightbar_column.value * 360, 0);
        theunitedTransform_right.rotation = currentrotation;
    }

    void InitialLeft()
    {
        for (int k = 0; k < z; k++)
        {
            for (int j = 0; j < y; j++)
            {
                for (int i = 0; i < x; i++)
                {
                    int temp = i + j * x + k * y * x;
                    cubes_left[temp] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cubes_left[temp].transform.position = new Vector3((i * 2 + 1 - x) * width, (j * 2 + 1 - y) * width, (k * 2 + 1 - z) * width);
                    cubes_left[temp].name = "Cubeleft (" + temp + ")";
                    cubes_left[temp].transform.parent = united_left.transform;
                    cubes_left[temp].layer = 7;
                    cubes_left[temp].SetActive(true);
                }
            }
        }
    }
    void SetLeft()
    {
        for (int k = 0; k < z; k++)
        {
            for (int j = 0; j < y; j++)
            {
                for (int i = 0; i < x; i++)
                {
                    int temp = i + j * x + k * y * x;

                    if (switches == 1) Display[temp] = Matrix[i + j * x + k * y * x + x * y * z * layer];
                    else if (switches == 2) Display[temp] = Matrix[i + j * x + layer * y * x + x * y * z * k];
                    else if (switches == 3) Display[temp] = Matrix[i + layer * x + k * y * x + j * x * y * z];
                    else if (switches == 4) Display[temp] = Matrix[layer + j * x + k * y * x + i * x * y * z];


                    if (Display[temp]) cubes_left[temp].SetActive(true);
                    else cubes_left[temp].SetActive(false);
                }
            }
        }
        Quaternion currentrotation = Quaternion.Euler(leftbar_row.value * 360, leftbar_column.value * 360, 0);
        theunitedTransform_left.rotation = currentrotation;
    }
}
