#region Copyright
/*---------------------------------------------------------------*/
/*        File: ChooseLevelComp.cs                               */
/*        Firefly                                                */
/*        AI For Game Programming - CAP 4053                     */
/*        Copyright (c) 2023 Serenity Studios                    */
/*        All rights reserved.                                   */
/*        Made with love, by Justin Sasso.                       */
/*---------------------------------------------------------------*/
#endregion

using System;
using UnityEngine;
using TMPro;

public class ChooseLevelComp : MonoBehaviour
{
    [SerializeField]
    private GameObject levelNameObject;
    [SerializeField]
    private GameObject canvasObject;

    private GameObject parentObject;
    private TextMeshProUGUI textField;
    private Canvas canvasField;
    private OverworldLevel overworldLevel;
    
    
    private void Start()
    {
        
    }
    private void Update()
    {
        if (overworldLevel != null)
        {
            FaceTowardsPlayer();
        }
    }
    public void Init(GameObject parentObject)
    {
        this.parentObject = parentObject;

        if (levelNameObject.TryGetComponent<TextMeshProUGUI>(out var textField))
        {
            this.textField = textField;
        }
        else
        {
            Debug.Log("Couldn't find LevelName textField.");
            return;
        }

        if (canvasObject.TryGetComponent<Canvas>(out var canvasField))
        {
            this.canvasField = canvasField;
        }
        else
        {
            Debug.Log("Couldn't find canvas field.");
        }

        if (parentObject.TryGetComponent<OverworldLevel>(out var overworldLevel))
        {
            this.overworldLevel = overworldLevel;
        }
        else
        {
            Debug.Log("Couldn't find overworld level.");
        }   
    }
    public void SetLevelText(string text)
    {
        textField.text = text;
    }
    public void SetCanvasCamera(Camera camera)
    {
        canvasField.worldCamera = camera;
    }
    private void FaceTowardsPlayer()
    {
        transform.LookAt(overworldLevel.DevinCamera.transform, overworldLevel.DevinCamera.transform.up);
    }
}
