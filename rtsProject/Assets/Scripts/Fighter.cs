using UnityEngine;
using System.Collections;

public class Fighter : Unit 
{

	// Use this for initialization
	public override void Start () 
    {
        base.Start();
        unitName = "Fighter";
        print(unitName + " - started");
	}
	
    public override void ActivateSpecial()
    {
        print(unitName + " - activated special");
    }
}
