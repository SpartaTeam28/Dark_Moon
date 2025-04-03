using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;




public class Synergy : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    protected Image spriteRenderer;

    private GameObject descriptionUI;

    private TextMeshProUGUI synergyName;
    private TextMeshProUGUI synergydescription;
    private TextMeshProUGUI synergyeffect;

    private void Awake()
    {
        spriteRenderer = GetComponent<Image>();
        descriptionUI = transform.GetChild(0).gameObject;
        synergyName = descriptionUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        synergydescription = descriptionUI.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        synergyeffect = descriptionUI.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
    }

    public void SetIcon(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }
    public void SetText(SynergyData synergy)
    {
        synergyName.text = synergy.synergyName;
        synergydescription.text = synergy.synergyDes;
        synergyeffect.text = synergy.synergyEffect;
    }



    public void OnPointerEnter(PointerEventData eventData)
    {
        descriptionUI.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        descriptionUI.SetActive(false);
    }
}
