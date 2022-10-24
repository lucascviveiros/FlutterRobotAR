using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARPlaceHologram : MonoBehaviour
{
    [SerializeField] private GameObject _prefabToPlace;
    public Camera myCam;
    private bool firstRay = false;
    private ARRaycastManager _raycastManager;     // Cache ARRaycastManager from XROrigin
    private ARPlaneManager _planeManager;
    private ARPlaneMeshVisualizer _PlaneMeshVisualizer;
    private static readonly List<ARRaycastHit> hits = new List<ARRaycastHit>();     // List for raycast hits is re-used by raycast manager
    private float waitTime = 6f;
    private float timer;
    private Transform selectedObject;
    private bool instantiateOne;

    void Awake()
    {
        _raycastManager = GetComponent<ARRaycastManager>();
        _planeManager = GetComponent<ARPlaneManager>();
    }

    private void HidePlanes()
    {
        GameObject[] arplaneVisualizer = GameObject.FindGameObjectsWithTag("ARPlane"); //looking for ARPlaneV tag added on planes

        foreach (GameObject go in arplaneVisualizer)
        {
            go.GetComponent<MeshRenderer>().enabled = false;
            go.GetComponent<ARPlaneMeshVisualizer>().enabled = false;
            go.GetComponent<LineRenderer>().enabled = false;
            _PlaneMeshVisualizer = go.GetComponent<ARPlaneMeshVisualizer>();
            _PlaneMeshVisualizer.SendMessage("SetVisible", false);
            //go.GetComponent<ARPlaneMeshVisualizer>().BroadcastMessage("UpdateVisibility", false);
        }

        Destroy(_planeManager);
    }

    void Update()
    {
        /*
        Touch touch;
        if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began) { return; }

        // Perform AR raycast to any kind of trackable
        if (_raycastManager.Raycast(touch.position, hits, TrackableType.Planes))
        {
            var hitPose = hits[0].pose;
            Instantiate(_prefabToPlace, hitPose.position, hitPose.rotation);
            _prefabToPlace.transform.LookAt(myCam.transform.position);
        }*/

        if(!instantiateOne)
        {
            if(!Input.GetMouseButton(0)) return;

            if(Input.GetMouseButtonDown(0))
            {
                Ray ray = myCam.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hitObject))
                {
                    selectedObject = hitObject.transform.CompareTag("ARObject") ? hitObject.transform : null;
                }
            }

            if (_raycastManager.Raycast(Input.mousePosition, hits, TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = hits[0].pose;

                if(!selectedObject)
                {
                    selectedObject = Instantiate(_prefabToPlace, hitPose.position, hitPose.rotation).transform;
                    instantiateOne = true;
                    HidePlanes();
                }
                else
                {
                   selectedObject.transform.position = hitPose.position;
                }
            }
        }

        /*
        //Auto placement when recognize a plane
        timer += Time.deltaTime;
        if (timer > waitTime)
        {
            Ray ray = myCam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("I'm looking at " + hit.transform.name);

                if (!firstRay)
                {
                    if (hit.transform.name.Contains("ARPlane"))
                    {
                        Instantiate(_prefabToPlace, hit.transform.position, transform.rotation);
                        _prefabToPlace.transform.LookAt(myCam.transform.position);
                        //interactionSession.TurnOffBool();
                        //Debug.Log("Placed!");
                        firstRay = true;

                    }
                }
            }
        }
        */

    }
}
