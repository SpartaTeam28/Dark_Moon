using UnityEngine;
using System.Collections.Generic;

public class NameGenerator : MonoBehaviour
{
    private string[] surnames = { "��", "��", "��", "��", "��", "��", "��", "��", "��", "ȫ", "��", "��", "��" };

    // ���� �̸� ����
    private string[] maleFirstSyllables = { "��", "��", "��", "��", "��", "��", "��", "��", "��", "��" };
    private string[] maleSecondSyllables = { "��", "ȣ", "��", "��", "��", "��", "ȯ", "ö", "��", "��" };

    // ���� �̸� ����
    private string[] femaleFirstSyllables = { "��", "��", "��", "��", "��", "��", "��", "��", "��", "��" };
    private string[] femaleSecondSyllables = { "��", "ȭ", "��", "��", "��", "��", "��", "��", "��", "��" };

    // �ߺ� ������
    private HashSet<string> usedNames = new HashSet<string>();

    public string GetRandomName(bool isMale = true)
    {
        int maxAttempts = 100;

        for (int i = 0; i < maxAttempts; i++)
        {
            string surname = surnames[Random.Range(0, surnames.Length)];

            string first = isMale
                ? maleFirstSyllables[Random.Range(0, maleFirstSyllables.Length)]
                : femaleFirstSyllables[Random.Range(0, femaleFirstSyllables.Length)];

            string second = isMale
                ? maleSecondSyllables[Random.Range(0, maleSecondSyllables.Length)]
                : femaleSecondSyllables[Random.Range(0, femaleSecondSyllables.Length)];

            string fullName = surname + first + second;

            if (!usedNames.Contains(fullName))
            {
                usedNames.Add(fullName);
                return fullName;
            }
        }

        Debug.LogWarning("�� �̻� ������ �� �ִ� �̸��� �����ϴ�.");
        return "�̸�����";
    }

    public void RemoveUsedName(string name)
    {
        usedNames.Remove(name);
    }

    public void ResetUsedNames()
    {
        usedNames.Clear();
    }
}
