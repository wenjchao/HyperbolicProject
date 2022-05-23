using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class createcodecenter : MonoBehaviour
{
    GameObject[] toggles;
    public bool[] result;
    public bool printed;
    string temp;
    // Start is called before the first frame update
    void Start()
    {
        toggles = new GameObject[50];
        for (int i = 0; i < 50; i++) toggles[i] = GameObject.Find("Toggle (" + i.ToString() + ")");
        result = new bool[49];
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < 49; i++)
        {
            result[i] = toggles[i].GetComponent<Toggle>().isOn;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {

            for (int i = 0; i < 49; i++)
            {
                if (result[i]) temp = temp + "true,";
                else temp = temp + "false,";
            }
            temp = temp + "**";
            print(temp);
        }
    }

    

}
