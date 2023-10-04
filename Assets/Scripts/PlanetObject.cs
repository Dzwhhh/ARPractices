using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlanetObject : MonoBehaviour
{
    public bool IsSelected { get; set; }

    [SerializeField]
    private TextMeshPro overlayText;

    [SerializeField]
    private string overlayDisplayText;

    private void Awake()
    {
        overlayText = GetComponentInChildren<TextMeshPro>();
        if (overlayText != null)
        {
            overlayText.gameObject.SetActive(false);
        }
    }

    public void ToggleOverlay()
    {
        overlayText.gameObject.SetActive(IsSelected);
        overlayText.text = overlayDisplayText;
    }
}
