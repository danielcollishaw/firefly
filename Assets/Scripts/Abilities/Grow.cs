using System;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu]
public class Grow : Ability
{

    public override void Activate(GameObject parent)
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.GrowSFX, parent.transform.position);
        StretchMechanic stretch = parent.GetComponent<StretchMechanic>();
        stretch.SetInput(true);
    }

    public override void Deactivate(GameObject parent)
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.ShrinkSFX, parent.transform.position);
        StretchMechanic stretch = parent.GetComponent<StretchMechanic>();
        stretch.SetInput(false);
    }
}
