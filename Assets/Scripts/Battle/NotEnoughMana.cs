using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NotEnoughMana : MonoBehaviour
{

    Image Image;
    TextMeshProUGUI TextMeshProUGUI;

    private void Start()
    {
        Image = GetComponent<Image>();
        TextMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void OnManaNotEnough()
    {
        StartCoroutine(NotEn());
    }
    public IEnumerator NotEn()
    {
        Image.enabled = true;
        TextMeshProUGUI.enabled = true;
        yield return new WaitForSeconds(1f);
        Image.enabled = false;
        TextMeshProUGUI.enabled = false;
    }
}
