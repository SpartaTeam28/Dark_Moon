using UnityEngine;
using System.Collections.Generic;

public class NameGenerator : MonoBehaviour
{
    private string[] surnames = { "김", "이", "박", "최", "정", "한", "조", "윤", "장", "홍", "신", "임", "강" };

    // 남자 이름 음절
    private string[] maleFirstSyllables = { "도", "지", "세", "윤", "성", "태", "무", "수", "강", "중" };
    private string[] maleSecondSyllables = { "현", "호", "석", "우", "범", "진", "환", "철", "수", "기" };

    // 여자 이름 음절
    private string[] femaleFirstSyllables = { "영", "자", "수", "은", "소", "혜", "연", "미", "정", "옥" };
    private string[] femaleSecondSyllables = { "희", "화", "심", "향", "숙", "경", "자", "연", "림", "선" };

    // 중복 방지용
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

        Debug.LogWarning("더 이상 생성할 수 있는 이름이 없습니다.");
        return "이름없음";
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
