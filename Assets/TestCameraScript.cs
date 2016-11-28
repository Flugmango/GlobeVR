using UnityEngine;
using System.Collections;
using VRTK;

public class TestCameraScript : MonoBehaviour {

    public Camera camera;
    private static float sphereRadius;


    // Use this for initialization
    void Start () {

      
    }


	
	// Update is called once per frame
	void Update () {


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


                    Debug.DrawRay(camera.transform.position, camera.transform.forward, Color.red);
                    Debug.Log("Globe hit");

                    Debug.DrawRay(pointerCollidedWith.point, camera.transform.up, Color.green);


                    sphereRadius = pointerCollidedWith.transform.GetComponent<SphereCollider>().transform.lossyScale.x;

                    Debug.Log(pointerCollidedWith.point);

                    // Call API function from here
                    getDataFor(pointerCollidedWith.point);

                }

                // Do something with the object that was hit by the raycast.
            }
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

        Vector2 loc = vector2LatLng(hitPoint);
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

    private Vector2 vector2LatLng(Vector3 point)
    {
        float radius = Mathf.Sqrt(Mathf.Pow(point.x, 2) + Mathf.Pow(point.y, 2) + Mathf.Pow(point.z, 2));

        Debug.Log(point);

        Debug.Log(radius);
        //        float lat = Mathf.Acos(point.y / radius);
        //        float lng = Mathf.Atan(point.z / point.x);

        //calc longitude
        float lng = Mathf.Atan2(point.x, point.z);

        //atan2 does the magic
        float lat = Mathf.Atan2(-point.y, radius);

        //convert to deg
        lat *= Mathf.Rad2Deg;
        lng *= Mathf.Rad2Deg;

        lat *= -1;
        lng *= -1;

        Debug.Log("lat->" + lat + " lng->" + lng);
        return new Vector2(lat, lng);
    }
}
