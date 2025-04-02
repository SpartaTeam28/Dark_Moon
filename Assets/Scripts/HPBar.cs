using UnityEngine;

public class HPBar : MonoBehaviour
{
    public CharacterStat stat;
    public Transform hpForeground; // 빨간 바

    private Vector3 originalScale;

    void Start()
    {
        originalScale = hpForeground.localScale;
    }

    //void Update()
    //{
    //    SetHpBar();
    //}

    public void SetHpBar()
    {
        float ratio = Mathf.Clamp01(stat.health.curHealth / stat.health.value);
        hpForeground.localScale = new Vector3(originalScale.x * ratio, originalScale.y, originalScale.z);

        // 왼쪽 기준으로 줄어들게 위치도 같이 보정
        float offset = (1 - ratio) * originalScale.x * 0.5f;
        hpForeground.localPosition = new Vector3(-offset, 0, 0);
    }
}
