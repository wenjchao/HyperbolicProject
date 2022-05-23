using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save2D : MonoBehaviour
{
    //public int[][] myarray;
    //myarray = new int[20][];
    //public int[][] myarray[0] = new int[]{0,1,1};

    public bool[] cube2 = new bool[7 * 7]{
        false,false,false,true ,false,false,false,
        false,false,true ,true ,true ,false,false,
        false,false,true ,true ,true ,false,false,
        false,false,true ,true ,true ,false,false,
        false,false,true ,true ,true ,false,false,
        false,false,true ,true ,true ,false,false,
        false,false,false,true ,false,false,false,
    };
    public bool[] cube3 = new bool[7 * 7]{
        false,false,false,true ,false,false,false,
        false,false,true ,true ,true ,false,false,
        true ,true ,true ,true ,true ,false,false,
        false,true ,true ,true ,true ,false,false,
        false,true ,true ,true ,true ,false,false,
        false,false,true ,true ,false,false,false,
        false,false,false,false,false,false,false,
    };
    public bool[] cube5 = new bool[7 * 7]
    {
        false,false,false,true,true,false,false,
        false,false,false,true,true,true,false,
        false,false,false,true,false,false,false,
        false,false,true,true,true,false,false,
        false,true,true,true,true,true,false,
        false,true,true,true,true,true,false,
        false,false,true,true,true,false,false
    };
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
