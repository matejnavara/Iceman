using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class DateGenerator : MonoBehaviour {

    private Text tombText;

    // Use this for initialization
    void Start () {
        tombText = gameObject.GetComponent<Text>();
    }

    public void updateDate()
    {
        tombText.text = "11 / 02 / 17 - " + System.DateTime.Today.ToString("dd / MM / yy");
    }

	
}
