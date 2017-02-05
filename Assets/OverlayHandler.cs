using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlayHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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

                GameObject legend = GameObject.FindGameObjectWithTag("Legend");
                //legend.
            }

            Debug.Log(url);
            WWW www = new WWW(url);
            Debug.Log(www.error);
            StartCoroutine(loadImage(www));
        }
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

        globeMaterial.SetColor("_EmissionColor", new Color(0.75f,0.75f,0.75f));
    }
}
