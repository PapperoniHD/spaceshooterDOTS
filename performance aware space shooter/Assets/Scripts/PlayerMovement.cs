using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Values")]
    public float speed = 10f;
    public float rotSpeed = 10f;
    public float maxSpeed = 5f;
    public float bulletSpeed = 20f;

    [Header("Health & Points")]
    public int health = 10;

    [Header("Components")]
    private Rigidbody2D rb;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private Transform bulletSpawnPos;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        //rb.AddForce(movement.y * speed * transform.up);
        rb.velocity = (movement.y * speed * transform.up);
        rb.rotation += movement.x * rotSpeed * Time.deltaTime;

        if (rb.velocity.y >= maxSpeed)
        {
            rb.velocity = new Vector2(rb.velocity.x,maxSpeed);
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPos.position, bulletSpawnPos.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = bulletSpeed * transform.up;
        yield return new WaitForSeconds(1f);
        Destroy(bullet);
    }

}
