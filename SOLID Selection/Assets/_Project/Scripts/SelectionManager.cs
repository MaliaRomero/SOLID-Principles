﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private string selectableTag = "Selectable";

    private ISelectionResponse _selectionResponse;

    private Transform _selection;

    private void Awake()
    {
        SceneManager.LoadScene("Environment", LoadSceneMode.Additive);
        SceneManager.LoadScene("UI", LoadSceneMode.Additive);

        _selectionResponse = GetComponent<ISelectionResponse>();
    }

    private void Update()
    {
        //Deselection/ Selection
        if (_selection != null)
        {
            _selectionResponse.OnDeselect(_selection);
        }

        //Create ray
        var ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        
        //Selection determination
        _selection = null;
        if (Physics.Raycast(ray, out var hit))
        {
            var selection = hit.transform;
            if (selection.CompareTag(selectableTag))
            {
                _selection = selection;
            }
        }

        //Deselection/ Selection Response
        if (_selection != null)
        {
            _selectionResponse.OnSelect(_selection);
        }
    }
}