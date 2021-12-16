using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int speed = 1;
    // Update is called once per frame
    void Update()
    {
        MoveAhead();
    }

    private void MoveAhead()
    {
        transform.Translate(transform.forward * (speed * Time.deltaTime),Space.World);
    }

    public void DoDamage(Enemy enemy)
    {
        enemy.TakeDamage(1); //TODO apply damage from tower data
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            DoDamage(other.GetComponent<Enemy>());
            Destroy(gameObject);
        }
    }
}

