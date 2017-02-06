using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverlayHandler : MonoBehaviour
{

    // Legend Items

    Sprite tempLegend;
    Sprite precipitationLegend;
    Sprite pressureLegend;
    Sprite windLegend;

    // Use this for initialization
    void Start()
    {

        windLegend = Resources.Load<Sprite>("wind_legend");
        pressureLegend = Resources.Load<Sprite>("pressure_legend");
        precipitationLegend = Resources.Load<Sprite>("precipitation_legend");
        tempLegend = Resources.Load<Sprite>("temp_legend");

    }

    // Update is called once per frame
    void Update()
    {

    }

    // adds Overlay to Globe
    // @param type type of overlay, "none" for no overlay (removing the actual one)
    public void addOverlay(string type)
    {
        if (type == "none")
        {
            GameObject globe = GameObject.FindGameObjectWithTag("Globe");
            Renderer globeRenderer = globe.GetComponent<Renderer>();
            Material globeMaterial = globeRenderer.material;
            globeMaterial.EnableKeyword("_EMISSION");
            // set no texture, does it work?
            globeMaterial.SetTexture("_EmissionMap", null); // maxbe change to other map: http://answers.unity3d.com/questions/914923/standard-shader-emission-control-via-script.html
            globeMaterial.SetColor("_EmissionColor", new Color(0f, 0f, 0f));
        }
        else
        {
            string url = "";
            if (type == "clouds")
            {
                // use local file for clouds
                url = "file:///C:/Users/sitcomlab/Desktop/GlobeVR/Assets/clouds_reproj.png";
            }
            else
            {
                // get image von globeVR backend
                Debug.Log(type);
                url = "http://giv-project12:3000/overlay/" + type;
            }

            Debug.Log(url);
            WWW www = new WWW(url);
            Debug.Log(www.error);
            StartCoroutine(loadImage(www));
        }

        this.addLegend(type);
    }

    IEnumerator loadImage(WWW www)
    {
        yield return www;

        Texture2D tex;
        tex = new Texture2D(128, 256);

        www.LoadImageIntoTexture(tex);
        //GetComponent<Renderer>().material.mainTexture = tex;
        Debug.Log(tex);


        GameObject globe = GameObject.FindGameObjectWithTag("Globe");
        Renderer globeRenderer = globe.GetComponent<Renderer>();
        Material globeMaterial = globeRenderer.material;
        globeMaterial.EnableKeyword("_EMISSION");
        // http://answers.unity3d.com/questions/914923/standard-shader-emission-control-via-script.html
        // globeMaterial.SetTexture("_DetailAlbedoMap", tex);
        globeMaterial.SetTexture("_EmissionMap", tex);

        globeMaterial.SetColor("_EmissionColor", new Color(0.75f, 0.75f, 0.75f));
    }

    void addLegend(string type)
    {
        GameObject legend = GameObject.FindGameObjectWithTag("Legend");
        RectTransform canvasRectTransform = legend.GetComponent<RectTransform>();

        SpriteRenderer background = legend.GetComponent<SpriteRenderer>();

        Debug.Log(background);
        // Set the background to white + transparent
        //background.color = new Color(1f, 1f, 1f, 0.75f);

        // Display Loading Image:
        //TODO: delete image
        switch (type)
        {
            case "temp":
                background.sprite = tempLegend;
                break;
            case "wind":
                background.sprite = windLegend;
                break;
            case "precipitation":
                background.sprite = precipitationLegend;
                break;
            case "pressure":
                background.sprite = pressureLegend;
                break;
            case "none":
                background.sprite = null;
                break;
            case "clouds":
                background.sprite = null;
                break;
        }
        //canvasRectTransform.localScale = new Vector3(0.025f, 0.025f, 0.005f);
        if (type == "precipitation")
        {
            canvasRectTransform.sizeDelta = new Vector2(10f, 2f);
        }
        else
        {
            canvasRectTransform.sizeDelta = new Vector2(10f, 1f);
        }
    }
}
