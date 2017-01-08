using UnityEngine;
using System.Collections;
using VRTK;
using UnityEngine.UI;


public class TestCameraScript : MonoBehaviour {

    public Camera camera;
    private static float sphereRadius;

    // Hard coded map texture width and height in pixel
    private int map_height = 10800;
    private int map_width = 21600;

    bool canvas_set = false;




    // Use this for initialization
    void Start () {

      
    }


	
	// Update is called once per frame
	void Update () {


        // Let the (created) InfoScreens look at the user

        Canvas[] infoscreens = FindObjectsOfType<Canvas>();

        foreach (var infoscreen in infoscreens)
        {
            Camera viewer = Camera.main;
            infoscreen.transform.LookAt(viewer.transform.position);
            //infoscreen.transform.rotation = Quaternion.LookRotation(viewer.transform.position - infoscreen.transform.position);

        }

        // Predefine lat & lng
        float lat;
        float lng;

        RaycastHit pointerCollidedWith;
        Ray ray = new Ray(camera.transform.position, camera.transform.forward);

        if (Input.GetKey(KeyCode.R))
        {
            if (Physics.Raycast(ray, out pointerCollidedWith))
            {
                // Transform objectHit = hit.transform;

                if (pointerCollidedWith.collider != null &&
                      pointerCollidedWith.collider.name == "Globe")
                {

                    float x = pointerCollidedWith.textureCoord.x * map_width;
                    float y = pointerCollidedWith.textureCoord.y * map_height;

                    lat = y / (map_height / 180) - 90;
                    lng = x / (map_width / 360) - 180;

                    // Call API function from here
                    //getDataFor(pointerCollidedWith.point);

                }

                // Do something with the object that was hit by the raycast.
            }
        }




        // Test creation of the info window
        if (Input.GetKey(KeyCode.I) && !canvas_set) {


            CreateCanvas();
            canvas_set = true;
        }

        if (Input.GetKey(KeyCode.P)) {

            canvas_set = false;
        }


        float scaleInput = 0.1f; // 0.01f
        if (Input.GetKey(KeyCode.RightArrow))
        {

            Vector3 newPosition = camera.transform.position;
            newPosition.x += scaleInput;
            camera.transform.position = newPosition;

        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {

            Vector3 newPosition = camera.transform.position;
            newPosition.x -= scaleInput;
            camera.transform.position = newPosition;

        }
        if (Input.GetKey(KeyCode.UpArrow))
        {

            Vector3 newPosition = camera.transform.position;
            newPosition.z += scaleInput;
            camera.transform.position = newPosition;

        }
        if (Input.GetKey(KeyCode.DownArrow))
        {

            Vector3 newPosition = camera.transform.position;
            newPosition.z -= scaleInput;
            camera.transform.position = newPosition;

        }


    }

    private void getDataFor(Vector3 hitPoint)
    {
        // First we convert our hitPoint to Latitude and Longitude

        //Vector2 loc = vector2LatLng(hitPoint);
        //Debug.Log("loc-> " + loc);
        // Then we contact the API to receive the data
        string url = "http://httpbin.org/ip";
        WWW www = new WWW(url);
       // StartCoroutine(WaitForRequest(www));


    }

    IEnumerator WaitForRequest(WWW www)
    {
        yield return www;

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

    public void CreateCanvas_old()
    {

        var canvasCount = FindObjectsOfType<Canvas>().Length;
        var newCanvasGO = new GameObject("TempCanvas");
        newCanvasGO.layer = 5;
        var canvas = newCanvasGO.AddComponent<Canvas>();
        var canvasRT = canvas.GetComponent<RectTransform>();
        canvasRT.position = new Vector3(-4f, 2f, 2f + canvasCount);
        canvasRT.sizeDelta = new Vector2(300f, 400f);
        canvasRT.localScale = new Vector3(0.005f, 0.005f, 0.005f);
        //canvasRT.eulerAngles = new Vector3(0f, 0f, 0f);

        var newButtonGO = new GameObject("TempButton");
        newButtonGO.transform.parent = newCanvasGO.transform;
        newButtonGO.layer = 5;

        newButtonGO.AddComponent<RectTransform>();

        var buttonRT = newButtonGO.GetComponent<RectTransform>();
        buttonRT.position = new Vector3(0f, 0f, 0f);
        buttonRT.anchoredPosition = new Vector3(0f, 0f, 0f);
        buttonRT.localPosition = new Vector3(0f, 0f, 0f);
        buttonRT.sizeDelta = new Vector2(180f, 60f);
        buttonRT.localScale = new Vector3(1f, 1f, 1f);
        buttonRT.localEulerAngles = new Vector3(0f, 0f, 0f);

        newButtonGO.AddComponent<Image>();
        var canvasButton = newButtonGO.AddComponent<Button>();
        var buttonColourBlock = canvasButton.colors;
        buttonColourBlock.highlightedColor = Color.red;
        canvasButton.colors = buttonColourBlock;

        var newTextGO = new GameObject("BtnText");
        newTextGO.transform.parent = newButtonGO.transform;
        newTextGO.layer = 5;

        var textRT = newTextGO.AddComponent<RectTransform>();
        textRT.position = new Vector3(0f, 0f, 0f);
        textRT.anchoredPosition = new Vector3(0f, 0f, 0f);
        textRT.localPosition = new Vector3(0f, 0f, 0f);
        textRT.sizeDelta = new Vector2(180f, 60f);
        textRT.localScale = new Vector3(1f, 1f, 1f);
        textRT.localEulerAngles = new Vector3(0f, 180f, 0f);

        var txt = newTextGO.AddComponent<Text>();
        txt.text = "New Button";
        txt.color = Color.black;
        txt.fontSize = 75;
        txt.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;

        FindObjectOfType<VRTK_UIPointer>().SetWorldCanvas(canvas);
    }
    public void CreateCanvas()
    {
        // Limited to one canvas while developing.. DELETE THIS LATER!!
        if (canvas_set) return;

        // Find out how many info screens have been placed
        var canvasCount = FindObjectsOfType<Canvas>().Length;

        var newCanvasGO = new GameObject("InfoScreen Prototype");

        var canvas = newCanvasGO.AddComponent<Canvas>();
        var canvasRT = canvas.GetComponent<RectTransform>();

        canvasRT.localScale = new Vector3(0.005f, 0.005f, 0.005f);
        //canvasRT.eulerAngles = new Vector3(0f, 0f, 0f);

        canvasRT.position = new Vector3(4.5f, 0.5f, 15f);
        canvasRT.sizeDelta = new Vector2(500f, 281f);

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

        Debug.Log(FindObjectOfType<Canvas>());

    }
}
