using UnityEngine;

public class Apple : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play(animator.GetCurrentAnimatorStateInfo(0).fullPathHash,0,Random.value);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.AddApple();
            animator.Play("obj_collected");
            Destroy(gameObject, 0.5f);
        }
    }
}