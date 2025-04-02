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
            // 초기 상태 설정
            SetAlpha(scene1, 0);
            SetAlpha(scene2, 0);
            SetAlpha(scene3, 0);

            // 첫 번째 장면 등장
            yield return ShowScene(scene1);

            // 두 번째 장면 전환
            yield return ShowScene(scene2);

            // 세 번째 장면 전환
            yield return ShowScene(scene3);
        }
    }

    //알파값 설정 함수 (초기화용)
    void SetAlpha(SpriteRenderer sprite, float alpha)
    {
        Color color = sprite.color;
        color.a = alpha;
        sprite.color = color;
    }

    //장면 페이드 인 & 아웃 처리 함수
    IEnumerator ShowScene(SpriteRenderer sprite)
    {
        sprite.DOFade(1, 1.5f);
        yield return new WaitForSeconds(5f);
        yield return sprite.DOFade(0, 1f).WaitForCompletion();
    }
}
