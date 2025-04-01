using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSlot : MonoBehaviour
{
    public Character character = null;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI jobText;
    public TextMeshProUGUI LvText;
    public Image characterIcon;
    public Button selectButton;
    public int slotIndex;


    public Transform traitSlotsTransform;
    public TraitSlot traitSlotPrefab;

    private void Awake()
    {
        traitSlotsTransform = UIManager.instance.SetTraitTransform();
    }

    public void OnClickSelectUnit()
    {
        if (character == UIManager.instance.SelectCharacter()) return;

        UIManager.instance.SetCharacterInfo(character, slotIndex);
        ClearTraitSlots();
        InstatiateTraitSlot();
    }


    public void SetNameText()
    {
        if (character == null) return;

        nameText.text = "이름 : " + character.info.characterName;
    }

    public void SetJobText()
    {
        if (character == null) return;

        jobText.text ="직업 : "  + character.info.job.jobName;
    }

    public void SetLvText()
    {
        if (character == null) return;

        LvText.text = "레벨 : " + character.info.level;
    }

    public void SetCharacterImage()
    {
        if (character == null) return;

        characterIcon.sprite = character.icon;
    }

    public void InstatiateTraitSlot()
    {
        for (int i = 0; i < character.trait.appliedTraits.Count; i++)
        {
            TraitSlot newTraitSlot = Instantiate(traitSlotPrefab, traitSlotsTransform);
            newTraitSlot.traitData = character.trait.appliedTraits[i];
            newTraitSlot.SetTraitName();
        }
    }

    public void RerollTrait()
    {
        character.trait.RerollTrait();

        // 기존 슬롯 제거
        ClearTraitSlots();

        // 새로운 슬롯 생성
        InstatiateTraitSlot();
    }

    // 기존 TraitSlot 삭제
    private void ClearTraitSlots()
    {
        foreach (Transform child in traitSlotsTransform)
        {
            Destroy(child.gameObject);
        }
    }



}
