using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Predator : Life
{
    private new void Start()
    {
        base.Start();
        CreateLife(FlockType.Predator);
    }

    private new void Update()
    {
        base.Update();
    }
}
