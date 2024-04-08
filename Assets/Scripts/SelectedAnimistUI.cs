using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectedAnimistUI : MonoBehaviour
{
    public TextMeshProUGUI selectedUnitText;
    public TextMeshProUGUI healthText;

    public Animist selectedAnimist;

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
        if (selectedAnimist == null)
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
        selectedUnitText.text = selectedAnimist.pieceName;
        healthText.text = $"Health: {selectedAnimist.energy}/{selectedAnimist.energy}";
    }

}
