using TMPro;
using UnityEngine;

public class AppleUI : MonoBehaviour
{
    public TextMeshProUGUI applesText;

    void Update()
    {
        applesText.text = GameManager.Instance.apples.ToString();
    }
}