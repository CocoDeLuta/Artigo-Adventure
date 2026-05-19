using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalFlag : MonoBehaviour
{
    Animator animator;
    public GameObject canvas;
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play(animator.GetCurrentAnimatorStateInfo(0).fullPathHash,0,Random.value);
    }
    void Update() 
    {
        if (GameManager.Instance.HasEnoughApples())
        {
            animator.Play("obj_flag");
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && GameManager.Instance.HasEnoughApples())
        {
            if(GameManager.Instance.apples >= 30)
            {
                canvas.GetComponent<WinMenu>().ShowWinMenu();
                return;
            }
                
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}