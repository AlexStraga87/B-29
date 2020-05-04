using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaBullet : Bullet
{
    private void Update()
    {
        Aging();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StandartCollision(collision);
    }
}

