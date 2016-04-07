using UnityEngine;
using System.Collections;

public class Archer : Unit
{

    // Use this for initialization
    public override void Start()
    {
        base.Start();
        unitName = "Archer";
        print(unitName + " - started");
    }

    public override void ActivateSpecial()
    {
        print(unitName + " - activated special");
    }
}
