using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionController : MonoBehaviour
{
    public List<PlacedObject> Planets;

    public Color ActiveColor = Color.blue;
    public Color InActiveColor = Color.gray;

    public GameObject WelcomPanel;
    public Button DismissButton;

    public Camera ArCamera;

    private void Awake()
    {
        DismissButton.onClick.AddListener(Dismiss);
    }

    private void Dismiss() => WelcomPanel.SetActive(false);


    void Start()
    {
        ChangeSelectedObject(Planets[0]);
    }

    // Update is called once per frame
    void Update()
    {
        if (WelcomPanel.activeSelf)
        {
            return;
        }

         Vector2 touchPositon;
        if (Input.touchCount > 0)
        {
            Debug.Log("touch screen");
            Touch touch = Input.GetTouch(0);
            touchPositon = touch.position;

            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = ArCamera.ScreenPointToRay(touchPositon);
                RaycastHit hitObject;
                if (Physics.Raycast(ray, out hitObject))
                {
                    PlacedObject planet = hitObject.transform.GetComponent<PlacedObject>();
                    if (planet != null)
                    {
                        ChangeSelectedObject(planet);
                    }
                }
            }
        }
        
        // Debug.Log("click screen");
        // if (Input.GetMouseButtonDown(0))
        // {
        //     Vector2 mousePosition = Input.mousePosition;
        //     Ray ray = ArCamera.ScreenPointToRay(mousePosition);
        //     RaycastHit hitObject;
        //     if (Physics.Raycast(ray, out hitObject))
        //     {
        //         PlacedObject placed = hitObject.transform.GetComponent<PlacedObject>();
        //         if (placed != null)
        //         {
        //             ChangeSelectedObject(placed);
        //         }
        //     }
        // }
    }
        
    void ChangeSelectedObject(PlacedObject selectedPlaced)
    {
        Debug.Log("Enter ChangeSelectedObject");
        foreach (PlacedObject planet in Planets)
        {
            MeshRenderer meshRenderer = planet.GetComponent<MeshRenderer>();
            if (selectedPlaced != planet)
            {
                planet.IsSelected = false;
                meshRenderer.material.color = InActiveColor;
            } else
            {
                planet.IsSelected = true;
                meshRenderer.material.color = ActiveColor;
            }
            planet.ToggleOverlay();
        }
    }
}
