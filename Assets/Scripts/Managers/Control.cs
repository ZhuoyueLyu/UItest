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
    //private Boolean toggleValue = false;

    // We need to update the values of some UI elements so here are
    // their remembered references after being queried from the cloned
    // UXML.
    private Label m_PositionLabel;

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


    // Update
    // Update UI Labels with data from the game. (also run some minimal game logic)
    private void Update()
    {
        // If any of our UI Labels have not been bound, do nothing.
        if (m_PositionLabel == null)
            return;

        // Update UI label text.
        m_PositionLabel.text = "Position: " + Cube.position.ToString();

    }


        ///////////////////////////////////////////////////////////////////////////////////////////////////
        // Bind UI to logic

        // Try to find specific elements by name in the main menu screen and
        // bind them to callbacks and data. Not finding an element is a valid
        // state.
        private IEnumerable<Object> BindGameScreen()
    {
        var root = m_GameScreen.visualTree;

        // Stats
        m_PositionLabel = root.Q<Label>("_position");

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
                //if (toggleValue)
                //{
                //    Cube.position += new Vector3(1f, 1f, 1f);
                //}
                //else {
                //    Cube.position -= new Vector3(1f, 1f, 1f);
                //}
                //toggleValue = !toggleValue;
                Cube.position = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
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
