using System.Collections;
using System.Collections.Generic;
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
}
