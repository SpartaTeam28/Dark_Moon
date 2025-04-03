using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class CharacterEditor : EditorWindow
{
    private VisualElement m_RightPane;


    [MenuItem("Window/Character Editor")]
    public static void ShowTraitEditor()
    {
        GetWindow<CharacterEditor>("CharacterEditor");
    }

    public void CreateGUI()
    {
        var characters = new List<Character>();
        foreach (var character in GameManager.instance.friendlyCharacterList)
        {
            characters.Add(character);
        }

        var splitView = new TwoPaneSplitView(0, 250, TwoPaneSplitViewOrientation.Horizontal);

        rootVisualElement.Add(splitView);

        var leftPane = new ListView();
        splitView.Add(leftPane);
        m_RightPane = new VisualElement();
        splitView.Add(m_RightPane);

        leftPane.makeItem = () => new Label();
        leftPane.bindItem = (item, index) => { (item as Label).text = characters[index].name; };
        leftPane.itemsSource = characters;

        leftPane.selectionChanged += OnSpriteSelectionChange;
    }

    private void OnSpriteSelectionChange(IEnumerable<object> selectedItems)
    {
        m_RightPane.Clear(); // ���� ���� �ʱ�ȭ

        foreach (var selected in selectedItems)
        {
            var character = selected as Character;
            if (character == null || character.traitManager == null) continue;

            var traits = character.traitManager.appliedTraits;

            var title = new Label($"[{character.info.characterName}]:");
            title.style.unityFontStyleAndWeight = FontStyle.Bold;
            title.style.marginBottom = 8;
            m_RightPane.Add(title);

            var InfoBox = new VisualElement();
            var info = new Label(
                $"���� : {character.info.level}\n" +
                $"����ġ : {character.info.curExp}/{character.info.totalExp}\n" +
                $"���ݷ� : {character.stat.attack.GetValueToString()}\n" +
                $"���� : {character.stat.defence.GetValueToString()}\n" +
                $"ü�� : {character.stat.health.GetValueToString()}\n" +
                $"��� : {character.stat.mana.GetValueToString()}\n" +
                $"ġ��Ÿ : {character.stat.critical.GetValueToString()}\n" +
                $"ȸ�� : {character.stat.evasion.GetValueToString()}\n" +
                $"��Ȯ�� : {character.stat.accuracy.GetValueToString()}\n" +
                $"�ӵ� : {character.stat.speed.GetValueToString()}\n");
            InfoBox.Add(info);

            var levelUpButton = new Button(() =>
            {
                character.info.LevelUp();
                OnSpriteSelectionChange(new List<object>() { character });
            })
            { 
                text = "������"
            };
            InfoBox.Add(levelUpButton);
            m_RightPane.Add(InfoBox);

            var stat = new Label(" ����� Ư��:");
            m_RightPane.Add(stat);

            var rerollButton = new Button(() =>
            {
                character.traitManager.ApplyTaritReroll();
                OnSpriteSelectionChange(new List<object> { character }); // UI ����
            })
            {
                text = "Ư�� �����ϱ�"
            };
            m_RightPane.Add(rerollButton);

            if (traits.Count == 0)
            {
                m_RightPane.Add(new Label("����� Ư���� �����ϴ�."));
                continue;
            }

            foreach (var trait in traits)
            {
                var traitBox = new VisualElement();
                traitBox.style.marginBottom = 5;
                traitBox.style.paddingBottom = 5;
                traitBox.style.borderBottomColor = Color.gray;
                traitBox.style.borderBottomWidth = 1;

                var nameLabel = new Label($"�̸�: {trait.traitName}");
                var descLabel = new Label($"����: {trait.description}");
                var statLabel = new Label($"���� ����: {trait.affectedStat}");
                var valueLabel = new Label($"��ġ ����: {trait.valueModifier}, ���� ����: {trait.multiplierModifier}");

                traitBox.Add(nameLabel);
                traitBox.Add(descLabel);
                traitBox.Add(statLabel);
                traitBox.Add(valueLabel);

                var replaceButton = new Button(() =>
                {
                    character.traitManager.ReplaceTrait(trait);
                    OnSpriteSelectionChange(new List<object> { character });
                })
                {
                    text = "�� Ư�� ��ü"
                };
                traitBox.Add(replaceButton);

                m_RightPane.Add(traitBox);
            }
        }
    }
}
