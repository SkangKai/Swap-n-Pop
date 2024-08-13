using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FireBullet : MonoBehaviour
{
    public static UnityEvent FireNow = new UnityEvent();
    public Transform firePoint;
    public GameObject filePrefab;
    public int turnCount = 2;
    public int countMultiplier;

    public float projectileSpeed = 10f;

    private Animator anim;

    private void Start()
    {
        turnCount = countMultiplier;
        FireNow.AddListener(Fire);
        
        //get animation
        anim = gameObject.GetComponentInParent<Animator>();
        Debug.Log(anim);
    }

    void Fire()
    {
        //animate throw
        anim.SetInteger("AnimThrow", 1);
        

        if (!transform.parent.gameObject.activeInHierarchy)
        {
            return;
        }
        if (transform.parent.tag == "Coworker" || transform.parent.tag == "PlayerCharacter" 
            || (transform.parent.tag == "Coworker Round 2" && BulletManager.GetInstance().GetCurrentEnemy().GetComponent<SwapWithPlayer>().GetSwapCount() == 0 && BulletManager.GetInstance().GetCurrentLevel() >= 8))
        {
            GameObject bullet = Instantiate(filePrefab, firePoint.position, firePoint.rotation);
            bullet.GetComponent<Bullet>().enemyParent = transform.parent.GetComponent<Enemy>();
            transform.parent.GetComponent<Enemy>().bulletChild = bullet.GetComponent<Bullet>();
            bullet.name = bullet.GetComponent<Bullet>().enemyParent + "'s bullet";
            Rigidbody fileProjectile = bullet.GetComponent<Rigidbody>();
            fileProjectile.AddForce(firePoint.forward * projectileSpeed, ForceMode.Impulse);

            if (bullet.GetComponent<Bullet>().enemyParent.gameObject.activeInHierarchy)
            {
                BulletManager.GetInstance().RegisterBullets(bullet.GetComponent<Bullet>());
            }
        }

        turnCount = countMultiplier;
    }
}
