﻿namespace VRTK
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    /*
      This class represents an Infoscreen
    */
    public class Infoscreen : MonoBehaviour
    {


        //public GameObject canvasGameObject;
        public Canvas canvas;
        public RectTransform canvasRectTransform;
        public Image background;

        // Use this for initialization
        void Start()
        {
            gameObject.AddComponent<VRTK_InteractableObject>();
            gameObject.GetComponent<VRTK_InteractableObject>().isGrabbable = true;
        }

        // Update is called once per frame
        void Update()
        {
            //this.canvasRectTransform.LookAt(Camera.main.transform.position);

            // always make the Infoscreen look to the camera
            this.canvasRectTransform.transform.rotation = Quaternion.LookRotation(this.canvasRectTransform.position - Camera.main.transform.position);

            //Quaternion.LookRotation(transform.position - target.position);

        }


        /*
          init initializes the Infoscreen
          <param name="lat">latitude</param>
          <param name="lon">longitude</param>
          <param name="type">desired type</param>
        */
        public void init(float lat, float lng, string type)
        {
            // creating canvas
            //this.canvasGameObject = new GameObject("Infoscreen Gameobject");
            this.canvas = gameObject.AddComponent<Canvas>();
            this.canvasRectTransform = gameObject.GetComponent<RectTransform>();

            // setting background
            this.background = gameObject.AddComponent<Image>();
            // Set the background to white + transparent
            background.color = new Color(1f, 1f, 1f, 0.75f);

            // Display Loading Image:
            background.sprite = Resources.Load<Sprite>("placeholder");

            // Set size of the canvas
            this.canvasRectTransform.localScale = new Vector3(0.005f, 0.005f, 0.005f);
            this.canvasRectTransform.sizeDelta = new Vector2(500f, 281f);

            // This places the infoscreen just in front to the user
            //gameObject.transform.parent = Camera.main.transform;
            gameObject.transform.position = Camera.main.transform.position + 2 * Camera.main.transform.forward;

            // Add "Loading Screen"-Texture until backend comm is finished
            // Load that from local assets, e.g.

            // Start Backend Comm
            // e.g. https://docs.unity3d.com/ScriptReference/WWW.LoadImageIntoTexture.html
            string url = string.Format("http://giv-project12:3000/getData/{0}/{1}/{2}", lat, lng, type);
            //string url = string.Format("http://lorempixel.com/{0}/{1}", this.canvasRectTransform.sizeDelta.x, this.canvasRectTransform.sizeDelta.y);



            WWW www = new WWW(url);
            StartCoroutine(GetImage(www));


        }

        /*
          GetImage loads the image from the requested url
          <param name="www">www url object</param>
        */
        IEnumerator GetImage(WWW www)
        {
            yield return www;

            Debug.Log("get Image");
            Debug.Log(www.texture);

            // create temp texture
            Texture2D tex = new Texture2D((int)canvasRectTransform.sizeDelta.x, (int)canvasRectTransform.sizeDelta.y);

            www.LoadImageIntoTexture(tex);

            // create sprite from texture
            Sprite content = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));

            // set background as sprite
            background.sprite = content;



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
}
