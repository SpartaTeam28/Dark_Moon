using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TraitSlot : MonoBehaviour
{
    public TraitData traitData;
    public TextMeshProUGUI traitText;

    public void SetTraitName()
    {
        traitText.text = traitData.traitName;
    }

}
