#region Copyright
/*---------------------------------------------------------------*/
/*        File: FallCountCanvas.cs                               */
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

public class FallCountCanvas : MonoBehaviour
{
    [SerializeField]
    private GameObject fallCountObject;
    private TextMeshProUGUI fallCountTextMeshPro;
    
    private void Start()
    {
        if (fallCountObject.TryGetComponent<TextMeshProUGUI>(out var fallCountTextMeshPro))
        {
            this.fallCountTextMeshPro = fallCountTextMeshPro;
        }
        else
        {
            string componentNames = Extend.GetComponentNames(fallCountObject);
            Debug.Log($"FallCountCanvas>Did not find TextMeshPro- component names: {componentNames} |");
        }
    }
    public void Toggle(bool activated)
    {
        gameObject.SetActive(activated);
    }
    public void UpdateFallCount(int fallCount)
    {
        //fallCountTextMeshPro.text = string.Format("Fall Count: {0}", fallCount);
    }
}
