using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIController : MonoBehaviour 
{

    public Text unitNameText;
    public GameObject unitInfoPanel;

    public void SetUnitInfo(Unit unit)
    {
        if(unit == null)
        {
            HideUnitPanel();
            return;
        }
        else
        {
            unitNameText.text = unit.unitName;
            ShowUnitPanel();
        }
    }
    
    public void ShowUnitPanel()
    {
        unitInfoPanel.SetActive(true);
    }

    public void HideUnitPanel()
    {
        unitInfoPanel.SetActive(false);
    }
}
