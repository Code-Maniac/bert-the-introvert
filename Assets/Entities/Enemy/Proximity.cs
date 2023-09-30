using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proximity : MonoBehaviour
{
    float proximity;
    float annoyance;
    Vector3 playerPos;
    Vector3 enemyPos;
    [SerializeField] GameObject player;
    [SerializeField] float maxAnnoyance = 1f;
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

        Annoy(annoyance);

        Debug.Log($"enemy - {enemyPos}");
        Debug.Log($"player - {playerPos}");
        Debug.Log($"proximity - {proximity}");
        Debug.Log($"damage - {annoyance}");

    }
    // calculates damage rates based on distance to player
    private void SetDamage()
    {
        if (proximity < maxDist)
        {
            annoyance = (1 - proximity / maxDist) * maxAnnoyance * Time.deltaTime;
        }
        else
        {
            annoyance = 0f;
        }
    }
}
