using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


[RequireComponent(typeof(ARRaycastManager))]
public class PlacementControllerV2 : MonoBehaviour
{
    [SerializeField] private Button placeCubeButton;
    
    [SerializeField] private Button placeSphereButton;
    
    [SerializeField] private Button placeCylinderButton;

    public TextMeshProUGUI prefabNumberText;
    
    public TextMeshProUGUI prefabNameText;
    
    private ARRaycastManager raycastManager;
    private static List<ARRaycastHit> _hits = new List<ARRaycastHit>();
    private GameObject currentPrefab;
    private int currentPrefabNum = 0;

    void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
        
        // add button click event
        placeCubeButton.onClick.AddListener(() => {ChangePrefab("NormalCube");});
        placeSphereButton.onClick.AddListener(() => {ChangePrefab("NormalSphere");});
        placeCylinderButton.onClick.AddListener(() => {ChangePrefab("NormalCylinder");});
    }

    void ChangePrefab(string prefabName)
    {
        currentPrefab = Resources.Load<GameObject>($"Prefabs/{prefabName}");

        if (currentPrefab == null)
        {
            Debug.LogError("currentPrefab is empty");
        }
        else
        {
            string prefabAppendix = prefabName.Substring(6);  // escape 'Normal'
            prefabNameText.text = $"Current Prefab Shape: {prefabAppendix}";
        }
    }

    private void Start()
    {
        // set default prefab number
        prefabNumberText.text = "Current Prefab Num: 0";
        
        // set default prefab
        ChangePrefab(prefabName: "NormalSphere");
    }

    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            touchPosition = touch.position;
            if (touch.phase != TouchPhase.Began)
                return false;
            return true;
        }
        touchPosition = default;
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!TryGetTouchPosition(out Vector2 touchPosition))
            return;
        
        // EventSystem.current.IsPointerOverGameObject() 解决UGUI 射线穿透UI问题
        if (raycastManager.Raycast(touchPosition, _hits, TrackableType.PlaneWithinPolygon) &&
            !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
        {
            var hitPose = _hits[0].pose;

            // instantiate new object
            hitPose.position.y += 0.05f;

            Instantiate(currentPrefab, hitPose.position, hitPose.rotation);
            
            currentPrefabNum += 1;
            prefabNumberText.text = $"Current Prefab Num: {Convert.ToString(currentPrefabNum)}";
        }
    }
}