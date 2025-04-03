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
        m_RightPane.Clear(); // 기존 내용 초기화

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
                $"레벨 : {character.info.level}\n" +
                $"경험치 : {character.info.curExp}/{character.info.totalExp}\n" +
                $"공격력 : {character.stat.attack.GetValueToString()}\n" +
                $"방어력 : {character.stat.defence.GetValueToString()}\n" +
                $"체력 : {character.stat.health.GetValueToString()}\n" +
                $"기력 : {character.stat.mana.GetValueToString()}\n" +
                $"치명타 : {character.stat.critical.GetValueToString()}\n" +
                $"회피 : {character.stat.evasion.GetValueToString()}\n" +
                $"정확도 : {character.stat.accuracy.GetValueToString()}\n" +
                $"속도 : {character.stat.speed.GetValueToString()}\n");
            InfoBox.Add(info);

            var levelUpButton = new Button(() =>
            {
                character.info.LevelUp();
                OnSpriteSelectionChange(new List<object>() { character });
            })
            { 
                text = "레벨업"
            };
            InfoBox.Add(levelUpButton);
            m_RightPane.Add(InfoBox);

            var stat = new Label(" 적용된 특성:");
            m_RightPane.Add(stat);

            var rerollButton = new Button(() =>
            {
                character.traitManager.ApplyTaritReroll();
                OnSpriteSelectionChange(new List<object> { character }); // UI 갱신
            })
            {
                text = "특성 리롤하기"
            };
            m_RightPane.Add(rerollButton);

            if (traits.Count == 0)
            {
                m_RightPane.Add(new Label("적용된 특성이 없습니다."));
                continue;
            }

            foreach (var trait in traits)
            {
                var traitBox = new VisualElement();
                traitBox.style.marginBottom = 5;
                traitBox.style.paddingBottom = 5;
                traitBox.style.borderBottomColor = Color.gray;
                traitBox.style.borderBottomWidth = 1;

                var nameLabel = new Label($"이름: {trait.traitName}");
                var descLabel = new Label($"설명: {trait.description}");
                var statLabel = new Label($"영향 스탯: {trait.affectedStat}");
                var valueLabel = new Label($"수치 변경: {trait.valueModifier}, 배율 변경: {trait.multiplierModifier}");

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
                    text = "이 특성 교체"
                };
                traitBox.Add(replaceButton);

                m_RightPane.Add(traitBox);
            }
        }
    }
}
