using System;
using System.Collections;
using System.Collections.Generic;
using Unity.UIElements.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class Control : MonoBehaviour
{
    
    public PanelRenderer m_GameScreen;      // the UI screen
    public Transform Cube;
    private Boolean toggleValue = false;

    ///////////////////////////////////////////////////////////////////////////////////////////////////
    // MonoBehaviour States

    // OnEnable
    // Register our postUxmlReload callbacks to be notified if and when
    // the UXML or USS assets being user are changed (by the UI Builder).
    // In these callbacks, we just rebind UI VisualElements to data or
    // to click events.
    private void OnEnable()
    {
        m_GameScreen.postUxmlReload = BindGameScreen;
    }


    // Start
    // Just go to main menu.
    private void Start()
    {
    #if !UNITY_EDITOR
            if (Screen.fullScreen)
                Screen.fullScreen = false;
    #endif

        GoToMainMenu();
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////
    // Bind UI to logic

    // Try to find specific elements by name in the main menu screen and
    // bind them to callbacks and data. Not finding an element is a valid
    // state.
    private IEnumerable<Object> BindGameScreen()
    {
        var root = m_GameScreen.visualTree;

        // Buttons
        var largerButton = root.Q<Button>("larger");
        if (largerButton != null)
        {
            largerButton.clickable.clicked += () =>
            {
                Cube.localScale += new Vector3(1.0f, 1.0f, 1.0f);
            };
        }
        var toggleButton = root.Q<Button>("toggle");
        if (toggleButton != null)
        {
            toggleButton.clickable.clicked += () =>
            {
                if (toggleValue)
                {
                    Cube.position += new Vector3(1f, 1f, 1f);
                }
                else {
                    Cube.position -= new Vector3(1f, 1f, 1f);
                }
                toggleValue = !toggleValue;
            };
        }
        var smallerButton = root.Q<Button>("smaller");
        if (smallerButton != null)
        {
            smallerButton.clickable.clicked += () =>
            {
                Cube.localScale -= new Vector3(1.0f, 1.0f, 1.0f);
            };
        }

        return null;
    }





    ///////////////////////////////////////////////////////////////////////////////////////////////////
    // Screen Transition Logic

    // If we have multiple screens in the future, 
    // it should be added here: 
    private void GoToMainMenu()
    {
        SetScreenEnableState(m_GameScreen, true);
        //SetScreenEnableState(m_GameScreen, false);
        //SetScreenEnableState(m_EndScreen, false);
    }

    void SetScreenEnableState(PanelRenderer screen, bool state)
    {
        if (state)
        {
            screen.visualTree.style.display = DisplayStyle.Flex;
            screen.enabled = true;
            screen.gameObject.GetComponent<UIElementsEventSystem>().enabled = true;
        }
        else
        {
            screen.visualTree.style.display = DisplayStyle.None;
            screen.enabled = false;
            screen.gameObject.GetComponent<UIElementsEventSystem>().enabled = false;
        }
    }


}
