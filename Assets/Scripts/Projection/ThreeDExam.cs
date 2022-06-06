using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThreeDExam : MonoBehaviour
{
    public int num;
    public float width;
    public bool[] Matrix;
    public bool[] Display;
    public GameObject[] cubes_right;
    public GameObject[] cubes_left;
    public GameObject theplane;
    public GameObject united_for_left;
    public GameObject united_for_right;
    public Transform theunitedTransform_right;
    public Transform theunitedTransform_left;

    public GameObject[] toggles;
    public Toggle[] get_toggle;
    public bool[] toggle_memory;

    public int switches;
    public int layer;

    public Material mat;
    public float alpha_y = 1;
    public float alpha_n = 0.1f;//half transparency

    // Start is called before the first frame update
    void Start()
    {
        InitialUnited();
        Matrix = GetComponent<Save3D>().cube1;
        layer = 0;
        Display = new bool[num * num];
        cubes_right = new GameObject[num*num];
        cubes_left = new GameObject[num * num];
        toggles = new GameObject[5];
        get_toggle = new Toggle[5];
        toggle_memory = new bool[5];
        InitialToggles();
        InitialrightCube();
        SetrightCubePosition();
        SetrightCubeColor();
        InitialleftCube();
        SetleftCubePosition();
        //SetleftCubeColor();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && layer > 0) layer--;
        if (Input.GetKeyDown(KeyCode.RightArrow) && layer < num - 1) layer++;
        Setswitches();
    }

    void FixedUpdate()
    {
        if (switches == 0) SetrightCubeColor();
        else SetleftCubeColor();
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
        united_for_right = GameObject.CreatePrimitive(PrimitiveType.Cube);
        united_for_right.name = "Cube3D";
        theunitedTransform_right = united_for_right.transform ;
        theunitedTransform_right.position = new Vector3(0, 0, 0);
        theunitedTransform_right.localScale = new Vector3(0.001f, 0.001f,0.001f);
        united_for_left = GameObject.CreatePrimitive(PrimitiveType.Cube);
        united_for_left.name = "Cube3D";
        theunitedTransform_left = united_for_left.transform;
        theunitedTransform_left.position = new Vector3(0, 0, 0);
        theunitedTransform_left.localScale = new Vector3(0.001f, 0.001f, 0.001f);
    }


    void InitialrightCube()
    {
        for (int j = 0; j < num; j++){
            for (int i = 0; i < num; i++)
            {
                int temp = i + j * num;
                cubes_right[temp] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cubes_right[temp].name = "Cuberight (" + temp + ")";
            }
        }
    }

    void SetrightCubePosition()
    {
        for (int i = 0; i < num * num; i++)
        {
            cubes_right[i].transform.position = new Vector3(((i % num) * 2 + 1 - num) * width, 0, ( num - ((i / num) * 2 + 1)) * width);
            cubes_right[i].GetComponent<Renderer>().material = mat;
            cubes_right[i].layer = 6;
        }
    }

    void SetrightCubeColor()
    {
        for (int i = 0; i < num; i++) {
            for (int j = 0; j < num; j++)
            {
                int temp = i + j * num;
                Display[temp] = Matrix[i + j * num + num * num * layer];

                if (Display[temp]) ChangeAlpha(cubes_right[temp], alpha_y);
                else ChangeAlpha(cubes_right[temp], alpha_n);
            }
        }
    }
    void InitialleftCube()
    {
        for (int j = 0; j < num; j++)
        {
            for (int i = 0; i < num; i++)
            {
                int temp = i + j * num;
                cubes_left[temp] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cubes_left[temp].name = "Cubeleft (" + temp + ")";
            }
        }
    }

    void SetleftCubePosition()
    {
        for (int i = 0; i < num * num; i++)
        {
            cubes_left[i].transform.position = new Vector3(((i % num) * 2 + 1 - num) * width, 0, (num - ((i / num) * 2 + 1)) * width);
            cubes_left[i].GetComponent<Renderer>().material = mat;
            cubes_left[i].layer = 7;
            ChangeAlpha(cubes_left[i], alpha_n);
        }
    }

    void SetleftCubeColor()
    {
        for (int i = 0; i < num; i++)
        {
            for (int j = 0; j < num; j++)
            {
                int temp = i + j * num;
                if (switches == 1) Display[temp] = Matrix[layer + (num - i - 1) * num + num * num * j];
                else if (switches == 2) Display[temp] = Matrix[i + layer * num + num * num * j];

                if (Display[temp]) ChangeAlpha(cubes_left[temp], alpha_y);
                else ChangeAlpha(cubes_left[temp], alpha_n);
            }
        }
    }

    void InitialToggles()
    {
        for (int i = 0; i < 5; i++)
        {
            toggles[i] = GameObject.Find("Toggle (" + i.ToString() + ")");
            get_toggle[i] = toggles[i].GetComponent<Toggle>();
            get_toggle[i].isOn = false;
        }
        switches = 0;
        get_toggle[0].isOn = true;
    }

    public void Setswitches()
    {
        for (int i = 0; i < 5; i++)
        {
            if (get_toggle[i].isOn && toggle_memory[i] == false)
            {
                toggle_memory[i] = true;
                switches = i;
                for (int j = 0; j < 5; j++)
                {
                    if (j != i)
                    {
                        toggle_memory[j] = false;
                        get_toggle[j].isOn = false;
                    }
                }
                break;
            }
        }
    }
}
