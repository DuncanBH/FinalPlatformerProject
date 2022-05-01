using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    int attackDamage;
    
    public Vector2 direction;
    [SerializeField]
    float speed;

    // Start is called before the first frame update
    void Start()
    {
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
        if(collision.tag != "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
