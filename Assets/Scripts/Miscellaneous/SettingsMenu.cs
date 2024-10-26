using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [Header("Space between menu items")]
    [SerializeField] private Vector2 spacing;

    private Button mainButton;
    private SettingsMenuItem[] menuItems;
    private bool isExpanded = false;

    private Vector2 mainButtonPosition;
    private int menuItemsCount;

    private void Start()
    {
        menuItemsCount = transform.childCount - 1;
        menuItems = new SettingsMenuItem[menuItemsCount];
        for (int i = 0; i < menuItemsCount; i++)
        {
            menuItems[i] = transform.GetChild(i + 1).GetComponent<SettingsMenuItem>();
        }

        mainButton = transform.GetChild(0).GetComponent<Button>();
        mainButton.onClick.AddListener(ToggleMenu);
        mainButton.transform.SetAsLastSibling();

        mainButtonPosition = mainButton.transform.position;

        //Reset all menu items position to main button position
        ResetPositions();
    }

    private void ResetPositions()
    {
        for (int i = 0; i < menuItemsCount; i++)
        {
            menuItems[i].trans.position = mainButtonPosition;
        }
    }

    void ToggleMenu()
    {
        isExpanded = !isExpanded;

        if (isExpanded)
        {
            //Menu opened
            for (int i = 0; i < menuItemsCount; i++)
            {
                menuItems[i].ToggleActive();
                menuItems[i].trans.position = mainButtonPosition + spacing * (i+1);
            }
        }
        else
        {
            //Menu closed
            for (int i = 0; i < menuItemsCount; i++)
            {
                menuItems[i].ToggleActive();
                menuItems[i].trans.position = mainButtonPosition;
            }
        }
    }

    private void OnDestroy()
    {
        mainButton.onClick.RemoveListener(ToggleMenu);
    }
}
