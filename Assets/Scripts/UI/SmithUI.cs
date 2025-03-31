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

    public int populationGold = 1000;
    public int rewordGold = 3000;
    public int statGold = 4000;
    public int traitGold = 5000;

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
        UpgrageGold(populationGoldText, populationGold);
  
    }

    public void OnClickUpgrageReword()
    {
        UpgrageGold(rewordGoldText, rewordGold);

    }

    public void OnClickStat()
    {
        UpgrageGold(statGoldText, statGold);

    }

    public void OnClickTraitButton()
    {
        UpgrageGold(traitGoldText, traitGold);

    }

    public void OnClickExit()
    {
        uiManager.OnClickLobby();
    }

    public void UpgrageGold(TextMeshProUGUI menutext, int gold)
    {
        uiManager.SpenGold(gold);
        gold += 2000;
        menutext.text = gold.ToString();
    }
}
