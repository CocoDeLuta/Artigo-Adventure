using UnityEngine;

public class PatrolEnemy : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 2f;

    [Header("Points")]
    public Transform pointA;
    public Transform pointB;

    Transform target;
    Animator animator;

    void Start()
    {
        target = pointB;
        animator = GetComponent<Animator>();
        animator.Play(animator.GetCurrentAnimatorStateInfo(0).fullPathHash,0,Random.value);
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        transform.position = Vector2.MoveTowards(
            transform.position,
            target.position,
            speed * Time.deltaTime
        );

        if (Vector2.Distance(transform.position, target.position) < 0.1f)
        {
            if (target == pointA)
            {
                target = pointB;
            }
            else
            {
                target = pointA;
            }

            Flip();
        }
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void OnDrawGizmos()
    {
        if (pointA != null && pointB != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(pointA.position, pointB.position);
        }
    }
}