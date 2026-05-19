using TMPro;
using UnityEngine;

public class FlagUI : MonoBehaviour
{
    public TextMeshProUGUI text;

    void Update()
    {
        text.text =
            GameManager.Instance.apples.ToString() +
            "/" +
            GameManager.Instance.requiredApples.ToString();
    }
}