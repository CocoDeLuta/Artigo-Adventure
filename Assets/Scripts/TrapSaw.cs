using UnityEngine;

public class TrapSaw : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play(animator.GetCurrentAnimatorStateInfo(0).fullPathHash,0,Random.value);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
