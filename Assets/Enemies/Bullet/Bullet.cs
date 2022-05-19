using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IDamagable
{
    [SerializeField]
    int attackDamage;
    
    public Vector2 direction;
    [SerializeField]
    float speed;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Destroy(gameObject, 10);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var hit = collision.GetComponent<IDamagable>();

        if (hit != null&&collision.tag == "Player")
        {
            hit.takeDamage(attackDamage);

        }
        if(collision.tag != "Enemy"&& collision.tag != "Sword")
        {
            Destroy(gameObject);
        }
    }

    public void takeDamage(int damage)
    {
        audioSource.Play();
        gameObject.GetComponent<Collider2D>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        //Destroy(this.gameObject,1f);
    }
}
