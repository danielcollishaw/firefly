using System;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu]
public class Grow : Ability
{

    public override void Activate(GameObject parent)
    {
        StretchMechanic stretch = parent.GetComponent<StretchMechanic>();

        stretch.SetInput(true);
    }

    public override void Deactivate(GameObject parent)
    {
        StretchMechanic stretch = parent.GetComponent<StretchMechanic>();
        
        stretch.SetInput(false);
    }
}
