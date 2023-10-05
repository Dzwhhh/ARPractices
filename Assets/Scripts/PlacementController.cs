using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


[RequireComponent(typeof(ARRaycastManager))]
public class PlacementController : MonoBehaviour
{
    [SerializeField]
    private GameObject placedPrefab;

    private ARRaycastManager raycastManager;
    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }
        touchPosition = default;
        return false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!TryGetTouchPosition(out Vector2 touchPosition))
        {
            Debug.Log("no touch detected");
            return;
        }
        
        Debug.Log($"touch position:{touchPosition.ToString()}");    
        if (raycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = hits[0].pose;

            // instantiate new object
            hitPose.position.y += 0.05f;

            var placedObject = Instantiate(placedPrefab, hitPose.position, hitPose.rotation);
            StartCoroutine(DestroyObject(placedObject));
        }
    }

    IEnumerator DestroyObject(GameObject gameObject)
    {
        yield return new WaitForSeconds(5.0f);
        Destroy(gameObject);
    }
}
