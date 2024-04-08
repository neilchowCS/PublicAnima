using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



public class SelectedSpiritUI : MonoBehaviour
{
    public TextMeshProUGUI selectedUnitText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI energyText;

    public Spirit selectedSpirit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnEnable()
    {
        if (selectedSpirit == null)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            SetText();
        }
    }

    public void SetText()
    {
        selectedUnitText.text = selectedSpirit.pieceName;
        energyText.text = "Energy: " + selectedSpirit.energy;
    }

}
