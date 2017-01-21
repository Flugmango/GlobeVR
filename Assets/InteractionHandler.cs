using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionHandler : MonoBehaviour {

    // The maximum number of open canvas elements
    public int maxCanvas = 5;

    // A public array containing the created infoscreens
    public VRTK.Infoscreen[] infoscreens;

    // Canvas Size Params
    public int canvas_width = 500;
    public int canvas_height = 281;

    public int currentCanvasCount;

	// Use this for initialization
	void Start () {

        // Init infoscreens array
        infoscreens = new VRTK.Infoscreen[maxCanvas];
        currentCanvasCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
        // Check some stuff here each frame



	}


    /*
        Member functions by GeoVR Team
    */

    public bool createInfoScreen(float lat, float lng, string type) {

        // Start Request to the backend to receive the image here

        Image canvasImage = getCanvasImage(lat, lng, type);

        // Create Canvas Element and display it

        var newCanvasGO = new GameObject("InfoScreen Prototype");

        var canvas = newCanvasGO.AddComponent<Canvas>();
        var canvasRT = canvas.GetComponent<RectTransform>();

        canvasRT.localScale = new Vector3(0.005f, 0.005f, 0.005f);
        //canvasRT.eulerAngles = new Vector3(0f, 0f, 0f);

        canvasRT.position = new Vector3(4.5f, 0.5f, 15f);
        canvasRT.sizeDelta = new Vector2(500f, 281f);




        /* Removed since we're displaying a image from the backend here

        var background = newCanvasGO.AddComponent<Image>();

        // Set the background to white + transparent
        background.color = new Color(1f, 1f, 1f, 0.15f);

        var newTextGO = new GameObject("Text");
        newTextGO.transform.parent = newCanvasGO.transform;

        var textRT = newTextGO.AddComponent<RectTransform>();
        textRT.position = new Vector3(0f, 0f, 0f);
        textRT.anchoredPosition = new Vector3(0f, 0f, 0f);
        textRT.localPosition = new Vector3(0f, 0f, 0f);
        textRT.sizeDelta = new Vector2(500f, 281f);
        textRT.localScale = new Vector3(1f, 1f, 1f);
        textRT.localEulerAngles = new Vector3(0f, 180f, 0f);

        var txt = newTextGO.AddComponent<Text>();

        txt.text = "InfoScreen";
        txt.color = Color.black;

        txt.fontSize = 40;
        txt.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;

        */

        // Return true when the canvas has been displayed correctly
        return true;
    }

    private Image getCanvasImage(float lat, float lng, string type) {

        // Contact Backend here, for now we only scrape data from an example site to grab an image

        string url = string.Format("https://placekitten.com/g/{0}/{1}", canvas_width, canvas_height);

        //string url = "https://placekitten.com/g/500/281";
        WWW www = new WWW(url);
        StartCoroutine(GetImage(www));

        Debug.Log("get Canvas");
        Debug.Log(www);

        // TODO: Try to access the data here and


        return null;
    }

    IEnumerator GetImage(WWW www)
    {
        yield return www;

        Debug.Log("get Image");
        Debug.Log(www.texture);

        // check for errors
        if (www.error == null)
        {
            Debug.Log("WWW Ok!: " + www.text);
        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }
    }

}
