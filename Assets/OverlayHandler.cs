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
        string url = "http://localhost:3000/overlay/" + type;
        WWW www = new WWW(url);
        StartCoroutine(loadImage(www));
       
    }

    IEnumerator loadImage(WWW www)
    {

        Texture2D tex;
        tex = new Texture2D(4, 4, TextureFormat.DXT1, false);
        yield return www;
        www.LoadImageIntoTexture(tex);
        //GetComponent<Renderer>().material.mainTexture = tex;

        GameObject globe = GameObject.FindGameObjectWithTag("Globe");
        Renderer globeRenderer = globe.GetComponent<Renderer>();
        Material globeMaterial = globeRenderer.material;
        globeMaterial.EnableKeyword("_EMISSION");
        globeMaterial.SetTexture("_EMISSION", tex);
    }
}
