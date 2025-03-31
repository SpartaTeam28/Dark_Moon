using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSlot : MonoBehaviour
{
    public Character character = null;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI jobText;
    public TextMeshProUGUI LvText;
    public Image characterImage;
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

        nameText.text = "�̸� : " + character.info.characterName;
    }

    public void SetJobText()
    {
        if (character == null) return;

        jobText.text ="���� : "  + character.info.job.jobName;
    }

    public void SetLvText()
    {

    }

    public void SetCharacterImage()
    {

    }

    public void InstatiateTraitSlot()
    {
        for (int i = 0; i < character.traitManager.appliedTraits.Count; i++)
        {
            TraitSlot newTraitSlot = Instantiate(traitSlotPrefab, traitSlotsTransform);
            newTraitSlot.traitData = character.traitManager.appliedTraits[i];
            newTraitSlot.SetTraitName();
        }
    }

    public void RerollTrait()
    {
        character.traitManager.ApplyTaritReroll();

        // ���� ���� ����
        ClearTraitSlots();

        // ���ο� ���� ����
        InstatiateTraitSlot();
    }

    // ���� TraitSlot ����
    private void ClearTraitSlots()
    {
        foreach (Transform child in traitSlotsTransform)
        {
            Destroy(child.gameObject);
        }
    }



}
