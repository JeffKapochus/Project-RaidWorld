﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int range;
    public int damage;
    public int fireSpeed;
    public int armorIgnore;
    public int shieldIgnore;
    public int cost;
    private float timeToAttack;
    [SerializeField]
    private List<EnemyUnit> enemiesInRange;
    // Start is called before the first frame update
    void Start()
    {
        enemiesInRange = new List<EnemyUnit>();
        timeToAttack = fireSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemiesInRange.Count > 0)
        {
            if (enemiesInRange[0] == null)
            {
                enemiesInRange.RemoveAt(0);
            }
            else{
                timeToAttack -= Time.deltaTime;
                if(timeToAttack < 0)
                {
                    EnemyUnit enemy = enemiesInRange[0];
                    transform.LookAt(new Vector3(enemy.transform.position.x, transform.position.y, enemy.transform.position.z));
                    enemy.TakeDamage(damage);
                    timeToAttack = fireSpeed;
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnemyUnit")
        {
            enemiesInRange.Add(other.gameObject.GetComponent<EnemyUnit>());
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "EnemyUnit")
        {
            enemiesInRange.Remove(other.gameObject.GetComponent<EnemyUnit>());
        }
    }
}
