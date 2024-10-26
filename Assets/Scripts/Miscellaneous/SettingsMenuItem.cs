using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuItem : MonoBehaviour
{
    [HideInInspector] public Image img;
    [HideInInspector] public Transform trans;
    [HideInInspector] private bool isActive = false;

    private void Awake()
    {
        img = GetComponent<Image>();
        trans = transform;
        gameObject.SetActive(isActive);
    }

    public void ToggleActive()
    {
        isActive = !isActive;
        img.enabled = isActive;
        gameObject.SetActive(isActive);
    }
}
