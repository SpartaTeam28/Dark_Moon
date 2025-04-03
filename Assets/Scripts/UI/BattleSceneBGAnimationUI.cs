using System.Collections;
using UnityEngine;
using DG.Tweening;


public class BattleSceneBGAnimation : MonoBehaviour
{
    public SpriteRenderer scene1, scene2, scene3;

    void Start()
    {
        StartCoroutine(PlayAnimation());
    }

    IEnumerator PlayAnimation()
    {
        while (true)
        {
            // �ʱ� ���� ����
            SetAlpha(scene1, 0);
            SetAlpha(scene2, 0);
            SetAlpha(scene3, 0);

            // ù ��° ��� ����
            yield return ShowScene(scene1);

            // �� ��° ��� ��ȯ
            yield return ShowScene(scene2);

            // �� ��° ��� ��ȯ
            yield return ShowScene(scene3);
        }
    }

    //���İ� ���� �Լ� (�ʱ�ȭ��)
    void SetAlpha(SpriteRenderer sprite, float alpha)
    {
        Color color = sprite.color;
        color.a = alpha;
        sprite.color = color;
    }

    //��� ���̵� �� & �ƿ� ó�� �Լ�
    IEnumerator ShowScene(SpriteRenderer sprite)
    {
        sprite.DOFade(1, 1.5f);
        yield return new WaitForSeconds(5f);
        yield return sprite.DOFade(0, 1f).WaitForCompletion();
    }
}
