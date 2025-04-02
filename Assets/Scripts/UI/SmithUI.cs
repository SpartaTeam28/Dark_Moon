using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SmithUI : BaseUI
{

    public Button populationButton;
    public Button rewordButton;
    public Button statButton;
    public Button traitButton;
    public Button exitButton;

    public TextMeshProUGUI goldText;
    public TextMeshProUGUI populationGoldText;
    public TextMeshProUGUI rewordGoldText;
    public TextMeshProUGUI statGoldText;
    public TextMeshProUGUI traitGoldText;
    public TextMeshProUGUI partnerMaxText;

    public int populationGold;
    public int rewordGold;
    public int statGold;
    public int traitGold;

    protected override UIState GetUIState()
    {
        return UIState.Smith;
    }

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
        populationButton.onClick.AddListener(OnClickUpgradePopulation);
        rewordButton.onClick.AddListener(OnClickUpgrageReword);
        statButton.onClick.AddListener(OnClickStat);
        traitButton.onClick.AddListener(OnClickTraitButton);
        exitButton.onClick.AddListener(OnClickExit);


        populationGoldText.text = populationGold.ToString();
        rewordGoldText.text = rewordGold.ToString();
        statGoldText.text = statGold.ToString();
        traitGoldText.text = traitGold.ToString();
        partnerMaxText.text = uiManager.partnerMax.ToString();
    }

    private void OnEnable()
    {
        if (uiManager != null)
        {
            uiManager.OnGoldChanged += UpdateGoldUI;
            UpdateGoldUI(uiManager.GetGold());
        }
    }

    private void OnDisable()
    {
        if (uiManager != null)
        {
            uiManager.OnGoldChanged -= UpdateGoldUI;
        }

    }

    private void UpdateGoldUI(int gold)
    {
        goldText.text = gold.ToString();
    }

    public void OnClickUpgradePopulation()
    {
        UpgrageGold(populationGoldText,ref populationGold);
        uiManager.partnerMax += 1;
  
    }

    public void OnClickUpgrageReword()
    {
        UpgrageGold(rewordGoldText, ref rewordGold);

    }

    public void OnClickStat()
    {
        UpgrageGold(statGoldText, ref statGold);
        
        for(int i =0; i< uiManager.partnerCharacters.Count; i++)
        {
        }

    }

    public void OnClickTraitButton()
    {
        UpgrageGold(traitGoldText, ref traitGold);

    }

    public void OnClickExit()
    {
        uiManager.OnClickLobby();
    }

    public void UpgrageGold(TextMeshProUGUI menutext, ref int gold)
    {
        uiManager.SpenGold(gold);
        gold += 2000;
        menutext.text = gold.ToString();
    }
}
