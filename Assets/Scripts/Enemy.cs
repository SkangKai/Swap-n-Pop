using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 1;
    public GameObject deathEffect;
    public Bullet bulletChild;

    private void Awake()
    {
        if (GetComponent<RaycastCheck>() == null)
        {
            BulletManager.GetInstance().SetCurrentEnemy(this);
        }
    }

    public void DamageTaken(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Transform EffectRotation = gameObject.transform;
        EffectRotation.rotation = Quaternion.Euler(-90.0f, transform.rotation.y, transform.rotation.z);

        if(bulletChild != null)
        {
            BulletManager.GetInstance().NoHit(bulletChild);
        }

        GameObject effect = Instantiate(deathEffect, transform.position, EffectRotation.rotation);
        Destroy(effect, 1f);
        gameObject.SetActive(false);
    }
}