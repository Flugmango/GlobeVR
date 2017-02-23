using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
  This class creates the info screens and manages them
 */
public class InteractionHandler : MonoBehaviour
{

    // The maximum number of open canvas elements
    public int maxCanvas = 5;

    // A public array containing the created infoscreens
    public ArrayList infoscreens;

    // Canvas Size Params
    public int canvas_width = 500;
    public int canvas_height = 281;

    public int currentCanvasCount;

    // Use this for initialization
    void Start()
    {

        // Init infoscreens array
        infoscreens = new ArrayList();
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
        this.createInfoScreen("temperature");
    }

    public void showPopulationData()
    {
        this.createInfoScreen("population");
    }

    // this methods deletes all infoscreens from the scene
    public void deleteAllScreens()
    {
        foreach(VRTK.Infoscreen infoScreen in infoscreens)
        {
            VRTK.Infoscreen.Destroy(infoScreen.gameObject);
        }
        this.infoscreens.Clear();
    }


    /*
      createInfoScreen creates a new info screen
      <param name="type">type of info screen</param>
    */
    public void createInfoScreen(string type) {

        // get last coords of pointerscript
        //float[] coords = GameObject.FindGameObjectWithTag("rightController").GetComponent<PointerScript>().getLastCoords();
        float[] coords = PointerScript.getLastCoords();
        GameObject newScreen;

        // Init new InfoScreen
        //VRTK.Infoscreen newScreen = gameObject.AddComponent<VRTK.Infoscreen>();



        newScreen = new GameObject("Infoscreen Prototype");

        VRTK.Infoscreen newInfoScreen = newScreen.AddComponent<VRTK.Infoscreen>();

        // initialize infoscreen with gicen coords and parameter
        newInfoScreen.init(coords[0], coords[1], type);

        // add info screen to arraylist
        this.infoscreens.Add(newInfoScreen);

        currentCanvasCount++;

    }
}
