using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject hitEffect;
    public int damage = 1;
    public Enemy enemy;
    public Enemy enemyParent;
    //private void OnCollisionEnter(Collision collision)
    //{
    //    GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
    //    Destroy(effect, 3f);
    //}

    void OnTriggerEnter(Collider hitEnemy)
    {
        if(hitEnemy.GetComponent<Bullet>())
        {
            return;
        }

        //if (hitEnemy.CompareTag("Coworker"))
        //{
            enemy = hitEnemy.GetComponent<Enemy>();

            if (hitEnemy.GetComponent<RaycastCheck>() && transform.parent != hitEnemy)
            {
                BulletManager.GetInstance().Loss();
                enemy.DamageTaken(damage);
            }
            else if (enemy != null)
            {
                enemy.DamageTaken(damage);
                BulletManager.GetInstance().NoHit(this);
            }
            else
            {
                BulletManager.GetInstance().NoHit(this);
            }

            Destroy(gameObject);
        //}
    }
}
