using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThreeDObject : MonoBehaviour
{
    public int num;
    public float width;
    public bool[] Matrix;
    public bool[] Display;
    public GameObject[] cubes;
    public GameObject[] cube3ds;
    public GameObject theplane;
    public GameObject united;
    public Transform theplaneTransform;
    public Transform theunitedTransform;

    public int switches;
    public int layer;

    public Material mat;
    public float alpha_y = 1;
    public float alpha_n = 0.1f;//half transparency

    public Scrollbar bar_column;
    public Scrollbar bar_row;

    // Start is called before the first frame update
    void Start()
    {
        InitialUnited();
        Matrix = GetComponent<Save3D>().cube1;
        layer = 0;
        Display = new bool[num * num];
        cubes = new GameObject[num*num];
        cube3ds = new GameObject[num * num * num];
        InitialCube(cubes);
        InitialCube3d(cube3ds);
        SetCubePosition();
        SetCubeColor();
        SetCube3dPositionAndColor();
        InitialPlane();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && layer > 0) layer--;
        if (Input.GetKeyDown(KeyCode.RightArrow) && layer < num - 1) layer++;
    }

    void FixedUpdate()
    {
        SetCubeColor();
        Quaternion currentrotation = Quaternion.Euler(bar_row.value * 360, bar_column.value * 360, 0);
        SetPlanePosition(currentrotation);
        theunitedTransform.rotation = currentrotation;
    }

    void ChangeAlpha(Material mat, float alphaVal)
    {
        Color oldColor = mat.color;
        Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, alphaVal);
        mat.SetColor("_Color", newColor);
    }

    void ChangeAlpha(GameObject obj, float alphaVal)
    {
        Color oldColor = obj.GetComponent<Renderer>().material.color;
        Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, alphaVal);
        obj.GetComponent<Renderer>().material.SetColor("_Color", newColor);
    }

    void InitialUnited()
    {
        united = GameObject.CreatePrimitive(PrimitiveType.Cube);
        theunitedTransform = united.transform ;
        theunitedTransform.position = new Vector3(0, 0, 0);
        theunitedTransform.localScale = new Vector3(0.001f, 0.001f,0.001f);
    }

    void InitialCube(GameObject[] array)
    {
        for (int j = 0; j < num; j++){
            for (int i = 0; i < num; i++)
            {
                int temp = i + j * num;
                array[temp] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                array[temp].name = "Cube2d (" + temp + ")";
            }
        }
    }

    void SetCubePosition()
    {
        for (int i = 0; i < num * num; i++)
        {
            cubes[i].transform.position = new Vector3(((i % num) * 2 + 1 - num) * width, 0, ( num - ((i / num) * 2 + 1)) * width);
            cubes[i].GetComponent<Renderer>().material = mat;
            cubes[i].layer = 6;
        }
    }

    void SetCubeColor()
    {
        for (int i = 0; i < num; i++) {
            for (int j = 0; j < num; j++)
            {
                int temp = i + j * num;
                if (switches == 1) Display[temp] = Matrix[i + j * num + num * num * layer];
                else if (switches == 2) Display[temp] = Matrix[layer + (num - i - 1) * num + num * num * j];
                else if (switches == 3) Display[temp] = Matrix[i + layer * num + num * num * j];

                if (Display[temp]) ChangeAlpha(cubes[temp], alpha_y);
                else ChangeAlpha(cubes[temp], alpha_n);
            }
        }
    }

    void InitialCube3d(GameObject[] array)
    {
        for (int k = 0; k < num; k++){
            for (int j = 0; j < num; j++){
                for (int i = 0; i < num; i++)
                {
                    int temp = i + j * num + k * num * num;
                    array[temp] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    array[temp].name = "Cube3d (" + temp + ")";
                    array[temp].transform.parent = united.transform;
                }
            }
        }
    }

    void SetCube3dPositionAndColor()
    {
        for (int i = 0; i < num * num * num ; i++)
        {
            cube3ds[i].transform.position = new Vector3(((i % num) * 2 + 1 - num) * width - 0, (num - ((i / num) / num * 2 + 1)) * width, (num - ((i / num)%num * 2 + 1)) * width);
            cube3ds[i].GetComponent<Renderer>().material = mat;
            if (Matrix[i]) cube3ds[i].SetActive(true);
            else cube3ds[i].SetActive(false);
            cube3ds[i].layer = 7;
        }
    }

    void InitialPlane()
    {
        theplane = GameObject.CreatePrimitive(PrimitiveType.Cube);
        theplane.GetComponent<Renderer>().material = mat;
        theplane.name = "theplane";
        theplaneTransform = theplane.transform;
        theplaneTransform.localScale = new Vector3(10, 0.3f, 10);
        theplaneTransform.position = new Vector3(0, 0, 0);
        theplane.layer = 7;

        Color oldColor = mat.color;
        Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, 0.05f);
        theplane.GetComponent<Renderer>().material.SetColor("_Color", newColor);
    }

    void SetPlanePosition( Quaternion rotation )
    {
        if (switches == 1) {
            theplaneTransform.position = rotation * new Vector3(0, (num - (layer * 2 + 1)) * width, 0);
            theplaneTransform.rotation = rotation * Quaternion.Euler(0, 0, 0);
        }
        else if (switches == 2){
            theplaneTransform.position = rotation * new Vector3(0 + ((layer * 2 + 1) - num) * width, 0, 0);
            theplaneTransform.rotation = rotation * Quaternion.Euler(0, 0, 90);
        }
        else if (switches == 3)
        {
            theplaneTransform.position = rotation * new Vector3(0, 0, (num - (layer * 2 + 1)) * width);
            theplaneTransform.rotation = rotation * Quaternion.Euler(90, 0, 0);
        }
    }

    public void Switchswitches()
    {
        if (switches == 1) switches = 2;
        else if (switches == 2) switches = 3;
        else switches = 1;
    }
}
