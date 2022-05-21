using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoDObject : MonoBehaviour
{
    public int num;
    public float width;
    public bool[] Matrix;
    public bool[] Display;
    public GameObject[] cubes;

    public int switches;
    public int layer;
    // Start is called before the first frame update
    void Start()
    {
        Matrix = GetComponent<Save2D>().cube1;
        Display = new bool[num];
        layer = 0;
        cubes = new GameObject[num];

        for (int i = 0; i < num; i++)
        {
            int temp = i;
            cubes[temp] = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cubes[temp].transform.position = new Vector3((i * 2 - 1 - num) * width, 0, 0);
            cubes[temp].name = "Cube_" + temp;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && layer > 0) layer--;
        if (Input.GetKeyDown(KeyCode.RightArrow) && layer < num - 1) layer++;
    }

    void FixedUpdate()
    {
        for (int i = 0; i < num; i++)
        {
            int temp = i;

            if (switches == 1) Display[temp] = Matrix[i + num * layer];
            else if (switches == 2) Display[temp] = Matrix[layer + i * num];

            if (Display[temp]) cubes[temp].SetActive(true);
            else cubes[temp].SetActive(false);
        }
    }
}