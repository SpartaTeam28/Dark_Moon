using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;  // DOTween 사용

public class TitleAnimation : MonoBehaviour
{
    public Image scene1, scene2, scene3, scene4;
    public TextMeshProUGUI text1, text2, text3, text4, text5, title;

    void Start()
    {
        
        // 모든 텍스트를 처음에는 투명하게 설정
        SetTextAlpha(text1, 0);
        SetTextAlpha(text2, 0);
        SetTextAlpha(text3, 0);
        SetTextAlpha(text4, 0);
        SetTextAlpha(text5, 0);
        SetTextAlpha(title, 0);


        StartCoroutine(PlayAnimation());
    }

    IEnumerator PlayAnimation()
    {
        // 초기 상태 설정
        scene1.color = new Color(1, 1, 1, 0);
        scene2.color = new Color(1, 1, 1, 0);
        scene3.color = new Color(1, 1, 1, 0);
        scene4.color = new Color(1,1,1,0);

        // 첫 번째 텍스트 등장
        text1.DOFade(1, 1f);
        yield return new WaitForSeconds(1.5f);
        
        yield return text1.DOFade(0, 1f).WaitForCompletion();

       //첫 번째 장면 등장
        scene1.DOFade(1, 1.5f);
        yield return new WaitForSeconds(1.5f);

        yield return scene1.DOFade(0, 1f).WaitForCompletion();

        //두 번째 텍스트 등장
        text2.DOFade(1, 1f);
        yield return new WaitForSeconds(1.5f);
        yield return text2.DOFade(0, 1f).WaitForCompletion();

        //두 번째 장면으로 전환
        scene2.DOFade(1, 1.5f);
        yield return new WaitForSeconds(1.5f);
        yield return scene2.DOFade(0, 1f).WaitForCompletion();


        //세 번째 텍스트 등장
        text3.DOFade(1, 1f);
        yield return new WaitForSeconds(1.5f);
        yield return text3.DOFade(0, 1f).WaitForCompletion();

        //세 번째 장면 전환
        scene3.DOFade(1, 1.5f);
        yield return new WaitForSeconds(1.5f);
        yield return scene3.DOFade(0, 1f).WaitForCompletion();

        //네 번째 텍스트 등장
        text4.DOFade(1, 1f);
        yield return new WaitForSeconds(1.5f);
        yield return text4.DOFade(0, 1f).WaitForCompletion();


        //다섯 번째 텍스트 등장
        text5.DOFade(1, 1f);
        yield return new WaitForSeconds(1.5f);
        yield return text5.DOFade(0, 1f).WaitForCompletion();


        //네 번째 장면 전환
        scene4.DOFade(1, 1.5f);
        yield return new WaitForSeconds(1.5f);

        // 8. 타이틀 등장
        title.DOFade(1,1f);
        yield return new WaitForSeconds(2.5f);

        // 9. 모든 애니메이션 종료
        this.gameObject.SetActive(false);
        UIManager.instance.OnClickTitle();
        yield return new WaitForSeconds(2f);
    }

    void SetTextAlpha(TextMeshProUGUI text, float alpha)
    {
        Color color = text.color;
        color.a = alpha;
        text.color = color;
    }

}
