using UnityEngine;
using System.Collections;

public class Mage : Unit
{

    // Use this for initialization
    public override void Start()
    {
        base.Start();
        unitName = "Mage";
        print(unitName + " - started");
    }

    public override void ActivateSpecial()
    {
        print(unitName + " - activated special");
    }
}
