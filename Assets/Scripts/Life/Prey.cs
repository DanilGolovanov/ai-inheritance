using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prey : Life
{
    private new void Start()
    {
        base.Start();
        CreateLife(FlockType.Prey);
    }

    private new void Update()
    {
        base.Update();
    }
}
