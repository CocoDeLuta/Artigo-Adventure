using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;

    public float shootInterval = 2f;
    public float bulletSpeed = 8f;

    float timer;

    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();

        // começa idle em frame aleatório
        anim.Play("enemy_turret_idle", 0, Random.value);

        // timer aleatório pra dessincronizar tiros
        timer = Random.Range(0f, shootInterval);
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            Shoot();

            timer = shootInterval;
        }
    }

    void Shoot()
    {
        anim.Play("enemy_turret_shoot");
    }

    public void SpawnBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        float direction = transform.localScale.x > 0 ? 1f : -1f;

        bullet.GetComponent<Rigidbody2D>().linearVelocity =
            new Vector2(direction * bulletSpeed, 0);
    }
}