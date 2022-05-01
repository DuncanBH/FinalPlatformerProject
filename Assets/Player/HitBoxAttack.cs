using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxAttack : MonoBehaviour
{
    [SerializeField]
    int attackDamage = 1;
    private void OnTriggerEnter2D(Collider2D other)
    {
        var hit = other.GetComponent<IDamagable>();

        if (hit != null)
        {
            hit.takeDamage(attackDamage);
        }
    }
}
