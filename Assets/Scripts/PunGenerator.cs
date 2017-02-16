using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class PunGenerator : MonoBehaviour {

	// Use this for initialization
	void Start () {

        Text tombText =gameObject.GetComponent<Text>();
        TextAsset file = (TextAsset) Resources.Load("puns");

        //XmlTextReader reader = new XmlTextReader(new StringReader(file.text));

        //XmlDocument xmlDoc = new XmlDocument(); // xmlDoc is the new xml document.
        //xmlDoc.LoadXml(file.text); // load the file.
        //XmlNodeList punList = xmlDoc.GetElementsByTagName("PunList"); // array of the level nodes.
        //print("LIST of:  " + punList.Count);
    }
	
}
