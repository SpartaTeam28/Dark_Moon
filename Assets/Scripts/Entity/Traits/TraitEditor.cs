using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class TraitEditor : EditorWindow
{
    private VisualElement m_RightPane;


    [MenuItem("Window/Trait Editor")]
    public static void ShowTraitEditor()
    {
        GetWindow<TraitEditor>("TraitEditor");
    }

    public void CreateGUI()
    {
        var characters = new List<Character>();
        foreach (var character in GameManager.instance.friendlyCharacterList)
        {
            characters.Add(character);
        }

        // Create a two-pane view with the left pane being fixed with
        var splitView = new TwoPaneSplitView(0, 250, TwoPaneSplitViewOrientation.Horizontal);

        // Add the view to the visual tree by adding it as a child to the root element
        rootVisualElement.Add(splitView);

        // A TwoPaneSplitView always needs exactly two child elements
        var leftPane = new ListView();
        splitView.Add(leftPane);
        m_RightPane = new VisualElement();
        splitView.Add(m_RightPane);

        leftPane.makeItem = () => new Label();
        leftPane.bindItem = (item, index) => { (item as Label).text = characters[index].name; };
        leftPane.itemsSource = characters;

        // React to the user's selection
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

            // ĳ���� �̸�
            var title = new Label($"[{character.name}] ����� Ư��:");
            title.style.unityFontStyleAndWeight = FontStyle.Bold;
            title.style.marginBottom = 8;
            m_RightPane.Add(title);

            var rerollButton = new Button(() =>
            {
                character.traitManager.ApplyTaritReroll();
                OnSpriteSelectionChange(new List<object> { character }); // UI ����
            })
            {
                text = "Ư�� �����ϱ�"
            };
            rerollButton.style.marginBottom = 10;
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
