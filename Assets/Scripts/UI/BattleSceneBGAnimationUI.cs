using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Unity.VisualScripting;

public class BattleSceneBGAnimationUI : MonoBehaviour
{
    public Image scene1, scene2, scene3;

    void Start()
    {
        StartCoroutine(PlayAnimation());
    }

    
    IEnumerator PlayAnimation()
    {
        while(true)
        {
            // �ʱ� ���� ����
            scene1.color = new Color(1, 1, 1, 0);
            scene2.color = new Color(1, 1, 1, 0);
            scene3.color = new Color(1, 1, 1, 0);


            //ù ��° ��� ����
            scene1.DOFade(1, 1.5f);
            yield return new WaitForSeconds(5f);
            scene1.DOFade(0, 1f).WaitForCompletion();


            //�� ��° ������� ��ȯ
            scene2.DOFade(1, 1.5f);
            yield return new WaitForSeconds(5f);
            scene2.DOFade(0, 1f).WaitForCompletion();

            //�� ��° ��� ��ȯ
            scene3.DOFade(1, 1.5f);
            yield return new WaitForSeconds(5f);
            scene3.DOFade(0, 1f).WaitForCompletion();

        }

    }


}
