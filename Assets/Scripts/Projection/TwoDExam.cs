using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TwoDExam : MonoBehaviour
{
    public int num;
    public float width;
    public bool[] Matrix;
    public bool[] Display;
    public GameObject[] cubes;
    public GameObject[] test;
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
        Matrix = GetComponent<Save2D>().cube5;
        Display = new bool[num];
        layer = 0;
        cubes = new GameObject[num];
        InitialCube();
        test = new GameObject[num];
        InitialTest();
        toggles = new GameObject[5];
        get_toggle = new Toggle[5];
        toggle_memory = new bool[5];
        InitialToggles();
        
    }

    // Update is called once per frame
    void Update()
    {
        ChangeAlpha(mat, 1);
        if (Input.GetKeyDown(KeyCode.LeftArrow) && layer > 0) layer--;
        if (Input.GetKeyDown(KeyCode.RightArrow) && layer < num - 1) layer++;
        Setswitches();
    }

    void FixedUpdate()
    {
        if (switches == 0)
        {
            for (int i = 0; i < num; i++)
            {
                Display[i] = Matrix[i + num * layer];
                if (Display[i]) ChangeAlpha(cubes[i], alpha_y);
                else ChangeAlpha(cubes[i], alpha_n);
            }
        }
        else if (switches == 1)
        {
            for (int i = 0; i < num; i++)
            {
                Display[i] = Matrix[layer + i * num];
                if (Display[i]) ChangeAlpha(test[i], alpha_y);
                else ChangeAlpha(test[i], alpha_n);
            }
        }

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

    void InitialCube()
    {
        for (int i = 0; i < num; i++)
        {
            cubes[i] = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cubes[i].name = "Cube (" + i + ")";
            cubes[i].GetComponent<Renderer>().material = mat;
            cubes[i].transform.position = new Vector3((i * 2 + 1 - num) * width, 0.51f, 0);
        }
    }

    void InitialTest()
    {
        for (int i = 0; i < num; i++)
        {
            test[i] = GameObject.CreatePrimitive(PrimitiveType.Cube);
            test[i].name = "Test (" + i + ")";
            test[i].GetComponent<Renderer>().material = mat;
            test[i].transform.position = new Vector3( -18 , 0.51f, ( num - i * 2 - 2 ) * width);
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
        for (int i = 0; i < 5; i++){
            if (get_toggle[i].isOn && toggle_memory[i] == false)
            {
                toggle_memory[i] = true;
                switches = i;
                for (int j = 0; j < 5; j++){
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