using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proximity : MonoBehaviour
{
    float proximity;
    float damage;
    Vector3 playerPos;
    Vector3 enemyPos;
    [SerializeField] GameObject player;
    [SerializeField] float annoyance = 1f;
    [SerializeField] float maxDist = 10f;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        enemyPos = transform.position;
        playerPos = player.transform.position;
        proximity = Vector3.Distance(playerPos, enemyPos);

        SetDamage();

        Debug.Log($"enemy - {enemyPos}");
        Debug.Log($"player - {playerPos}");
        Debug.Log($"proximity - {proximity}");
        Debug.Log($"damage - {damage}");

    }
    // calculates damage rates based on distance to player
    private void SetDamage()
    {
        if (proximity < maxDist)
        {
            damage = (1 - proximity / maxDist) * annoyance;
        }
        else
        {
            damage = 0f;
        }
    }
}
