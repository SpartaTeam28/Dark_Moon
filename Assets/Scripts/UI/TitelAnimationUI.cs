using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;  // DOTween ���

public class TitleAnimation : MonoBehaviour
{
    public Image scene1, scene2, scene3, scene4;
    public TextMeshProUGUI text1, text2, text3, text4, text5, title;

    void Start()
    {
        
        // ��� �ؽ�Ʈ�� ó������ �����ϰ� ����
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
        // �ʱ� ���� ����
        scene1.color = new Color(1, 1, 1, 0);
        scene2.color = new Color(1, 1, 1, 0);
        scene3.color = new Color(1, 1, 1, 0);
        scene4.color = new Color(1,1,1,0);

        // ù ��° �ؽ�Ʈ ����
        text1.DOFade(1, 1f);
        yield return new WaitForSeconds(1.5f);
        
        yield return text1.DOFade(0, 1f).WaitForCompletion();

       //ù ��° ��� ����
        scene1.DOFade(1, 1.5f);
        yield return new WaitForSeconds(1.5f);

        yield return scene1.DOFade(0, 1f).WaitForCompletion();

        //�� ��° �ؽ�Ʈ ����
        text2.DOFade(1, 1f);
        yield return new WaitForSeconds(1.5f);
        yield return text2.DOFade(0, 1f).WaitForCompletion();

        //�� ��° ������� ��ȯ
        scene2.DOFade(1, 1.5f);
        yield return new WaitForSeconds(1.5f);
        yield return scene2.DOFade(0, 1f).WaitForCompletion();


        //�� ��° �ؽ�Ʈ ����
        text3.DOFade(1, 1f);
        yield return new WaitForSeconds(1.5f);
        yield return text3.DOFade(0, 1f).WaitForCompletion();

        //�� ��° ��� ��ȯ
        scene3.DOFade(1, 1.5f);
        yield return new WaitForSeconds(1.5f);
        yield return scene3.DOFade(0, 1f).WaitForCompletion();

        //�� ��° �ؽ�Ʈ ����
        text4.DOFade(1, 1f);
        yield return new WaitForSeconds(1.5f);
        yield return text4.DOFade(0, 1f).WaitForCompletion();


        //�ټ� ��° �ؽ�Ʈ ����
        text5.DOFade(1, 1f);
        yield return new WaitForSeconds(1.5f);
        yield return text5.DOFade(0, 1f).WaitForCompletion();


        //�� ��° ��� ��ȯ
        scene4.DOFade(1, 1.5f);
        yield return new WaitForSeconds(1.5f);

        // 8. Ÿ��Ʋ ����
        title.DOFade(1,1f);
        yield return new WaitForSeconds(2.5f);

        // 9. ��� �ִϸ��̼� ����
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
