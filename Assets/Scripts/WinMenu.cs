using UnityEngine;

public class WinMenu : MonoBehaviour
{
    public GameObject winMenu;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowWinMenu()
    {
        winMenu.SetActive(true);
        Time.timeScale = 0f; // Pause the game
    }
}
