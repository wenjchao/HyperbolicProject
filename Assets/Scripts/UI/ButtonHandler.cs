using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonHandler : MonoBehaviour
{
    public TextMeshProUGUI mytext;

    public void Setstring(string newstring)
    {
        mytext.text = newstring;
    }
    // Start is called before the first frame update
    public void Teststring(string newstring)
    {
        TextMeshProUGUI newtext = transform.Find("Text").GetComponent<TextMeshProUGUI>();
        newtext.text = newstring;
    }

    public void Toprint()
    {
        GameObject.Find("Canvas").GetComponent<createcodecenter>().printed = true;
    }
}
