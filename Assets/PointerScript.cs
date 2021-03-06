﻿
using System.Collections;
using System.Collections.Generic;
using VRTK;
using UnityEngine;


public class PointerScript : VRTK_BasePointer {

    // The globe itself, initialised in the Start() method
    private GameObject Globe;

    // Hard coded map texture width and height in pixel
    private int map_height = 10800;
    private int map_width = 21600;

    // Variables for grabbing implementation
    private bool isGripPressed = false;
    private Vector3 latest_tip_position;

    private bool isTouchPadPressed = false;

    [Header("Simple Pointer Settings", order = 3)]

    [Tooltip("The thickness and length of the beam can also be set on the script as well as the ability to toggle the sphere beam tip that is displayed at the end of the beam (to represent a cursor).")]
    public float pointerThickness = 0.002f;
    [Tooltip("The distance the beam will project before stopping.")]
    public float pointerLength = 100f;
    [Tooltip("Toggle whether the cursor is shown on the end of the pointer beam.")]
    public bool showPointerTip = true;
    [Header("Custom Appearance Settings", order = 4)]
    [Tooltip("A custom Game Object can be applied here to use instead of the default sphere for the pointer cursor.")]
    public GameObject customPointerCursor;
    [Tooltip("Rotate the pointer cursor to match the normal of the target surface (or the pointer direction if no target was hit).")]
    public bool pointerCursorMatchTargetNormal = false;
    [Tooltip("Rescale the pointer cursor proportionally to the distance from this game object (useful when used as a gaze pointer).")]
    public bool pointerCursorRescaledAlongDistance = true;

    private GameObject pointerHolder;
    private GameObject pointerBeam;
    private GameObject pointerTip;
    private Vector3 pointerTipScale = new Vector3(0.05f, 0.05f, 0.05f);
    private Vector3 pointerCursorOriginalScale = Vector3.one;
    private bool activeEnabled;
    private bool storedBeamState;
    private bool storedTipState;

    private static float lastLat = 0;
    private static float lastLng = 0;

    private InteractionHandler ih;

    protected override void OnEnable()
    {
        base.OnEnable();
        InitPointer();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        if (pointerHolder != null)
        {
            Destroy(pointerHolder);
        }
    }

    protected override void Start()
    {
        base.Start();

        Globe = GameObject.Find("Globe");
        GetComponent<VRTK_ControllerEvents>().TriggerPressed += new ControllerInteractionEventHandler(grabInit);
        GetComponent<VRTK_ControllerEvents>().TriggerReleased += new ControllerInteractionEventHandler(grabDeinit);

        ih = GameObject.FindObjectOfType<InteractionHandler>();
    }

    protected override void Update()
    {
        base.Update();
        if (pointerBeam && pointerBeam.activeSelf)
        {
            Ray pointerRaycast = new Ray(GetOriginPosition(), GetOriginForward());
            RaycastHit pointerCollidedWith;
            var rayHit = Physics.Raycast(pointerRaycast, out pointerCollidedWith, pointerLength, ~layersToIgnore);
            var pointerBeamLength = GetPointerBeamLength(rayHit, pointerCollidedWith);
            SetPointerTransform(pointerBeamLength, pointerThickness);
            if (rayHit)
            {
                coord2latlng(pointerCollidedWith.textureCoord.x, pointerCollidedWith.textureCoord.y);
                if (pointerCursorMatchTargetNormal)
                {
                    pointerTip.transform.forward = -pointerCollidedWith.normal;
                }
                if (pointerCursorRescaledAlongDistance)
                {
                    float collisionDistance = Vector3.Distance(pointerCollidedWith.point, GetOriginPosition());
                    pointerTip.transform.localScale = pointerCursorOriginalScale * collisionDistance;
                }

                /*Added by GlobeVR*/
                // when laser beam pointing at earth and trigger is pressed, rotate the earth
                if (isGripPressed && pointerCollidedWith.collider.gameObject == Globe)
                {
                    // Adjust the rotation of the globe while taking the current rotation into consideration (*=)
                   Globe.transform.rotation *= Quaternion.FromToRotation(latest_tip_position, pointerTip.transform.position);
                }

                // save latest position of laser tip
                latest_tip_position = pointerTip.transform.position;


            }
            else
            {
                if (pointerCursorMatchTargetNormal)
                {
                    pointerTip.transform.forward = GetOriginForward();
                }
                if (pointerCursorRescaledAlongDistance)
                {
                    pointerTip.transform.localScale = pointerCursorOriginalScale * pointerBeamLength;
                }
            }

            if (activeEnabled)
            {
                activeEnabled = false;
                pointerBeam.GetComponentInChildren<Renderer>().enabled = storedBeamState;
                pointerTip.GetComponentInChildren<Renderer>().enabled = storedTipState;
            }
        }
    }

    protected override void UpdateObjectInteractor()
    {
        base.UpdateObjectInteractor();
        //if the object interactor is too far from the pointer tip then set it to the pointer tip position to prevent glitching.
        if (Vector3.Distance(objectInteractor.transform.position, pointerTip.transform.position) > 0)
        {
            objectInteractor.transform.position = pointerTip.transform.position;
        }
    }

    protected override void InitPointer()
    {
        pointerHolder = new GameObject(string.Format("[{0}]BasePointer_SimplePointer_Holder", gameObject.name));
        pointerHolder.transform.localPosition = Vector3.zero;
        VRTK_PlayerObject.SetPlayerObject(pointerHolder, VRTK_PlayerObject.ObjectTypes.Pointer);

        pointerBeam = GameObject.CreatePrimitive(PrimitiveType.Cube);
        pointerBeam.transform.name = string.Format("[{0}]BasePointer_SimplePointer_Pointer", gameObject.name);
        pointerBeam.transform.SetParent(pointerHolder.transform);
        pointerBeam.GetComponent<BoxCollider>().isTrigger = true;
        pointerBeam.AddComponent<Rigidbody>().isKinematic = true;
        pointerBeam.layer = LayerMask.NameToLayer("Ignore Raycast");

        var pointerRenderer = pointerBeam.GetComponent<MeshRenderer>();
        pointerRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        pointerRenderer.receiveShadows = false;
        pointerRenderer.material = pointerMaterial;

        VRTK_PlayerObject.SetPlayerObject(pointerBeam, VRTK_PlayerObject.ObjectTypes.Pointer);

        if (customPointerCursor)
        {
            pointerTip = Instantiate(customPointerCursor);
        }
        else
        {
            pointerTip = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            pointerTip.transform.localScale = pointerTipScale;

            var pointerTipRenderer = pointerTip.GetComponent<MeshRenderer>();
            pointerTipRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            pointerTipRenderer.receiveShadows = false;
            pointerTipRenderer.material = pointerMaterial;
        }

        pointerCursorOriginalScale = pointerTip.transform.localScale;
        pointerTip.transform.name = string.Format("[{0}]BasePointer_SimplePointer_PointerTip", gameObject.name);
        pointerTip.transform.SetParent(pointerHolder.transform);
        pointerTip.GetComponent<Collider>().isTrigger = true;
        pointerTip.AddComponent<Rigidbody>().isKinematic = true;
        pointerTip.layer = LayerMask.NameToLayer("Ignore Raycast");
        VRTK_PlayerObject.SetPlayerObject(pointerTip, VRTK_PlayerObject.ObjectTypes.Pointer);

        base.InitPointer();

        if (showPointerTip && objectInteractor)
        {
            objectInteractor.transform.localScale = pointerTip.transform.localScale * 1.05f;
        }

        SetPointerTransform(pointerLength, pointerThickness);
        TogglePointer(false);
    }

    protected override void SetPointerMaterial(Color color)
    {
        base.SetPointerMaterial(color);

        base.ChangeMaterialColor(pointerBeam, color);
        base.ChangeMaterialColor(pointerTip, color);
    }

    protected override void TogglePointer(bool state)
    {
        state = (pointerVisibility == pointerVisibilityStates.Always_On ? true : state);
        base.TogglePointer(state);
        if (pointerBeam)
        {
            pointerBeam.SetActive(state);
        }

        var tipState = (showPointerTip ? state : false);
        if (pointerTip)
        {
            pointerTip.SetActive(tipState);
        }

        if (pointerBeam && pointerBeam.GetComponentInChildren<Renderer>() && pointerVisibility == pointerVisibilityStates.Always_Off)
        {
            pointerBeam.GetComponentInChildren<Renderer>().enabled = false;
        }

        activeEnabled = state;

        if (activeEnabled)
        {
            storedBeamState = pointerBeam.GetComponentInChildren<Renderer>().enabled;
            storedTipState = pointerTip.GetComponentInChildren<Renderer>().enabled;

            pointerBeam.GetComponentInChildren<Renderer>().enabled = false;
            pointerTip.GetComponentInChildren<Renderer>().enabled = false;
        }
    }

    private void SetPointerTransform(float setLength, float setThicknes)
    {
        //if the additional decimal isn't added then the beam position glitches
        var beamPosition = setLength / (2 + 0.00001f);

        pointerBeam.transform.localScale = new Vector3(setThicknes, setThicknes, setLength);
        pointerBeam.transform.localPosition = new Vector3(0f, 0f, beamPosition);
        pointerTip.transform.localPosition = new Vector3(0f, 0f, setLength - (pointerTip.transform.localScale.z / 2));

        pointerHolder.transform.position = GetOriginPosition();
        pointerHolder.transform.rotation = GetOriginRotation();
        base.UpdateDependencies(pointerTip.transform.position);
    }

    private float GetPointerBeamLength(bool hasRayHit, RaycastHit collidedWith)
    {
        var actualLength = pointerLength;

        //reset if beam not hitting or hitting new collider
        if (!hasRayHit || (pointerContactRaycastHit.collider && pointerContactRaycastHit.collider != collidedWith.collider))
        {
            if (pointerContactRaycastHit.collider != null)
            {
                base.PointerOut();
            }

            pointerContactDistance = 0f;
            pointerContactTarget = null;
            pointerContactRaycastHit = new RaycastHit();
            destinationPosition = Vector3.zero;

            UpdatePointerMaterial(pointerMissColor);
        }

        //check if beam has hit a new target
        if (hasRayHit)
        {
            pointerContactDistance = collidedWith.distance;
            pointerContactTarget = collidedWith.transform;
            pointerContactRaycastHit = collidedWith;
            destinationPosition = pointerTip.transform.position;

            UpdatePointerMaterial(pointerHitColor);

            base.PointerIn();
        }

        //adjust beam length if something is blocking it
        if (hasRayHit && pointerContactDistance < pointerLength)
        {
            actualLength = pointerContactDistance;
        }

        return OverrideBeamLength(actualLength);
    }

    // convert x and y texture coordinates in geographical coordinates
    float[] coord2latlng(float x, float y) {

        // WAS: pointercollidedwith.texturecoord.x
        float map_x = x * map_width;
        float map_y = y * map_height;

        float lat = (map_y / (map_height / 180) - 90);
        float lng = map_x / (map_width / 360) - 180;

        lastLat = lat;
        lastLng = lng;

        return new float[] { lat, lng };
    }

    // returns the last coordinates
    // static so that other classes can access this function
     //@return float[] { lat, lng }
    public static float[] getLastCoords()
    {
        return new float[] { lastLat, lastLng };
    }

    private void grabInit(object sender, ControllerInteractionEventArgs e)
    {

        //latest_tip_position = pointerTip.transform.position;
        if (!isGripPressed)
        {
            isGripPressed = true;
        }
    }

    private void grabDeinit(object sender, ControllerInteractionEventArgs e)
    {
        if (isGripPressed)
        {
            isGripPressed = false;
        }
    }

}
