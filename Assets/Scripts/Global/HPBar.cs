using UnityEngine;

public class HPBar : MonoBehaviour
{
    public CharacterStat stat;
    public Transform hpForeground; // ���� ��

    private Vector3 originalScale;

    void Start()
    {
        originalScale = hpForeground.localScale;
    }

    void Update()
    {
        SetHpBar();
    }

    public void SetHpBar()
    {
        float maxHealth = stat.health.value;
        float currentHealth = stat.health.curHealth;

        if (currentHealth > maxHealth) 
        { 
            currentHealth = maxHealth;
        }

        float ratio = maxHealth > 0 ? Mathf.Clamp01(currentHealth / maxHealth) : 0f;

        hpForeground.localScale = new Vector3(originalScale.x * ratio, originalScale.y, originalScale.z);

        // ���� �������� �پ��� ��ġ�� ���� ����
        float offset = (1 - ratio) * originalScale.x * 0.5f;
        hpForeground.localPosition = new Vector3(-offset, 0, 0);
    }
}
