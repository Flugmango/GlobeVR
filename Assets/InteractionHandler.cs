using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionHandler : MonoBehaviour
{

    // The maximum number of open canvas elements
    public int maxCanvas = 5;

    // A public array containing the created infoscreens
    public VRTK.Infoscreen[] infoscreens;

    // Canvas Size Params
    public int canvas_width = 500;
    public int canvas_height = 281;

    public int currentCanvasCount;

    // Use this for initialization
    void Start()
    {

        // Init infoscreens array
        infoscreens = new VRTK.Infoscreen[maxCanvas];
        currentCanvasCount = 0;
    }

    // Update is called once per frame
    void Update()
    {

        // Check some stuff here each frame



    }


    /*
        Member functions by GeoVR Team
    */

    // entry points for radial menu
    public void showTemperatureData()
    {
        this.createInfoScreen(51, 7, "weather");
    }

    public void showPopulationData()
    {
        this.createInfoScreen(51, 7, "population");
    }


    public void createInfoScreen(float lat, float lng, string type) {

        GameObject newScreen;

        // Init new InfoScreen
        //VRTK.Infoscreen newScreen = gameObject.AddComponent<VRTK.Infoscreen>();

        newScreen = new GameObject("Infoscreen Prototype");

        VRTK.Infoscreen newInfoScreen = newScreen.AddComponent<VRTK.Infoscreen>();

        newInfoScreen.init(lat, lng, type);

        this.infoscreens[currentCanvasCount] = newInfoScreen;

        currentCanvasCount++;

    }
    

    private Image getCanvasImage(float lat, float lng, string type)
    {


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
