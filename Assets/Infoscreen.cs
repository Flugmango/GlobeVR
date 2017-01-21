namespace VRTK
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class Infoscreen : VRTK_InteractableObject {


        public GameObject canvasGameObject;
        public Canvas canvas;
        public RectTransform canvasRectTransform;

	    // Use this for initialization
	    void Start (float lat, float lng, string type) {

            this.canvasGameObject = new GameObject("Infoscreen Gameobject");
            this.canvas = this.canvasGameObject.AddComponent<Canvas>();
            this.canvasRectTransform = this.canvasGameObject.GetComponent<RectTransform>();

            // Set size of the canvas
            this.canvasRectTransform.localScale = new Vector3(0.005f, 0.005f, 0.005f);
            this.canvasRectTransform.sizeDelta = new Vector2(500f, 281f);

            // Set the position somewhere relative to the users position
            Vector3 viewer_position = Camera.main.transform.position;
            
            // This places the infoscreen just in front to the user. TODO: Switch case to differentiate between several positions
            // when more than one infoscreen is displayed
            this.canvasGameObject.transform.parent = Camera.main.transform;
            this.canvasGameObject.transform.localPosition = new Vector3(1, 0, 1);

            // Add "Loading Screen"-Texture until backend comm is finished
            // Load that from local assets, e.g. 

            // Start Backend Comm
            // e.g. https://docs.unity3d.com/ScriptReference/WWW.LoadImageIntoTexture.html


            // Add to InteractionHandler Infoscreens array
            InteractionHandler InteractionHandler = GetComponent<InteractionHandler>();
            InteractionHandler.infoscreens[InteractionHandler.currentCanvasCount] = this;

        }

        // Update is called once per frame
        void Update () {
		
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