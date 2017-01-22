namespace VRTK
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class Infoscreen : VRTK_InteractableObject
    {


        public GameObject canvasGameObject;
        public Canvas canvas;
        public RectTransform canvasRectTransform;

        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {

        }


        public void init(float lat, float lng, string type)
        {

            
            //this.canvasGameObject = new GameObject("Infoscreen Gameobject");
            this.canvas = gameObject.AddComponent<Canvas>();
            this.canvasRectTransform = gameObject.GetComponent<RectTransform>();

            var background = gameObject.AddComponent<Image>();
            // Set the background to white + transparent
            background.color = new Color(1f, 1f, 1f, 0.15f);

            // Set size of the canvas
            this.canvasRectTransform.localScale = new Vector3(0.005f, 0.005f, 0.005f);
            this.canvasRectTransform.sizeDelta = new Vector2(500f, 281f);


            Debug.Log("lat");
            Debug.Log(lat);

            Debug.Log("lng");
            Debug.Log(lng);

            Debug.Log("type");
            Debug.Log(type);

            // This places the infoscreen just in front to the user. TODO: Switch case to differentiate between several positions
            // when more than one infoscreen is displayed
            //this.canvasGameObject.transform.parent = Camera.main.transform;
            gameObject.transform.localPosition = Camera.main.transform.position + new Vector3(1, 0, 1);

            // Add "Loading Screen"-Texture until backend comm is finished
            // Load that from local assets, e.g. 

            // Start Backend Comm
            // e.g. https://docs.unity3d.com/ScriptReference/WWW.LoadImageIntoTexture.html

        }


        public override void StartUsing(GameObject usingObject)
        {
            base.StartUsing(usingObject);
        }

        public override void StopUsing(GameObject usingObject)
        {
            base.StopUsing(usingObject);
        }





    }
}