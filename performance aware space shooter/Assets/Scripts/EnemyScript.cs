using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public GameObject player;
    private Rigidbody2D rb;

    public float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = player.GetComponent<Rigidbody2D>();
        GameManager.GM.enemyCount++;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    void Death(GameObject go)
    {
        GameManager.GM.points++;
        Destroy(go);
        Destroy(gameObject);
        GameManager.GM.enemyCount--;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.GM.enemyCount--;
            Destroy(gameObject);
        }
        if (collision.CompareTag("Bullet"))
        {
            Death(collision.gameObject);
        }
    }
}
