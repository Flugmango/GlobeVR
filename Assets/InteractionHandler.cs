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
        this.createInfoScreen("weather");
    }

    public void showPopulationData()
    {
        this.createInfoScreen("population");
    }


    public void createInfoScreen(string type) {

        // get last coords of pointerscript
        
        //float[] coords = GameObject.FindGameObjectWithTag("rightController").GetComponent<PointerScript>().getLastCoords();
        float[] coords = PointerScript.getLastCoords();
        GameObject newScreen;

        // Init new InfoScreen
        //VRTK.Infoscreen newScreen = gameObject.AddComponent<VRTK.Infoscreen>();

        newScreen = new GameObject("Infoscreen Prototype");

        VRTK.Infoscreen newInfoScreen = newScreen.AddComponent<VRTK.Infoscreen>();

        newInfoScreen.init(coords[0], coords[1], type);

        this.infoscreens[currentCanvasCount] = newInfoScreen;

        currentCanvasCount++;

    }
}
