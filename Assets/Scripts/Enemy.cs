using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float minSpeed;
    public float maxSpeed;

    float speed;

    PlayerController playerScript;

    void Start()
    {
        speed = UnityEngine.Random.Range(minSpeed, maxSpeed);
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        if (transform.position.x < 1 && transform.position.x > -1.5 && transform.position.y < 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (transform.position.y < 3 && transform.position.y > -5 && transform.position.x < 0)
        {
            transform.eulerAngles = new Vector3(0, 0, -90);
        }
        else if (transform.position.y < 3 && transform.position.y > -5 && transform.position.x > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 90);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 180);
        }
    }

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D hitObject)
    {
        if (hitObject.tag == "Player")
        {
            Destroy(gameObject);
            playerScript.TakeDamage();
        }

        if (hitObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
