using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class PunGenerator : MonoBehaviour {

    private TextAsset punAsset;
    private Text tombText;
    private Text tombDate;
    private XmlDocument xmlDoc;

    // Use this for initialization
    void Start () {

        punAsset = (TextAsset)Resources.Load("puns");
        tombText = gameObject.GetComponent<Text>();
        xmlDoc = new XmlDocument(); // xmlDoc is the new xml document.

    }

    string generatePun()
    {

        //xmlDoc.LoadXml(File.ReadAllText(Application.dataPath + "/Resources/puns.xml")); // load the file.
        xmlDoc.LoadXml(punAsset.text);
        XmlNodeList punList = xmlDoc.SelectNodes("/PunList/pun");
        int selectedPun = Random.Range(0, punList.Count);

        return ((XmlElement)punList[selectedPun]).GetAttribute("num");

        //print("LIST of:  " + punList.Count);
    }

    public void updatePun()
    {
        tombText.text = generatePun();
    }

	
}
