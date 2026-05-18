using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 5f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Ground"))
    {
        Destroy(gameObject);
    }
}
}