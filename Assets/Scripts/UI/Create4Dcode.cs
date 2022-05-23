using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Create4Dcode : MonoBehaviour
{
    public int index;
    // Start is called before the first frame update
    void Start()
    {
        index = Findnumber(GetComponent<Transform>().name);
        Setposition();

    }

    // Update is called once per frame
    void Update()
    {

    }

    int Findnumber(string newstring)
    {
        int numberinstring = 0;
        for (int i = 0; i < newstring.Length; i++)
            for (int j = 0; j < 10; j++) if (char.Equals(newstring[i], j.ToString()[0])) numberinstring = numberinstring * 10 + j;
        return numberinstring;
    }

    void Setposition()
    {
        int row = index / 7;
        int column = index % 7;
        int rowmagnify = 40;
        int columnmagnify = 40;
        GetComponent<RectTransform>().anchoredPosition = new Vector2((column-3) * columnmagnify, (3-row) * rowmagnify);
    }
}
