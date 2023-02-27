#region Copyright
/*---------------------------------------------------------------*/
/*        File: ObjectiveTrigger.cs                              */
/*        Firefly                                                */
/*        AI For Game Programming - CAP 4053                     */
/*        Copyright (c) 2023 Serenity Studios                    */
/*        All rights reserved.                                   */
/*        Made with love, by Justin Sasso.                       */
/*---------------------------------------------------------------*/
#endregion

using System;
using UnityEngine;
using UnityEngine.Events;

public class ObjectiveTrigger : MonoBehaviour
{
    public readonly OverlapEvent EventOverlap = new();

    public BoxCollider OverlapTrigger;


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Trigger {other.name} entered!");
        LevelManager.Single.EventLevelComplete.Invoke();
    }


}
[Serializable]
public class OverlapEvent : UnityEvent { }