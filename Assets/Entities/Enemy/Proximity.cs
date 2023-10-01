using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

enum StatAction
{
    Annoy,
    Entertain,
    Exercise,
    Feed,
    None
}

public class Proximity : MonoBehaviour
{
    float proximity;
    float change;
    bool inRange;
    public bool inUse = false;
    Vector3 playerPos;
    Vector3 enemyPos;

    [SerializeField] bool actioned = false;
    [SerializeField] GameObject player;

    [SerializeField] StatAction statAction;
    [SerializeField] float maxChange = 1f;
    [SerializeField] float maxDist = 10f;


    private PlayerController _playerController;

    // Start is called before the first frame update
    void Start()
    {
        _playerController = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        enemyPos = transform.position;
        playerPos = player.transform.position;
        proximity = Vector3.Distance(playerPos, enemyPos);

        // if (this.tag == "Fridge")
        // {
        //     Debug.Log($"Proximity = {proximity}");
        // }

        CheckRange();

        if ((!actioned || Input.GetKey("space")) && inRange)
        {
            inUse = true;

            CalculateChange();
            MakeChange();
        }
        else if (Input.GetKeyUp("space"))
        {
            inUse = false;
        }



    }

    private void MakeChange()
    {
        switch (statAction)
        {
            case StatAction.Annoy:
                _playerController.Annoy(change);
                break;
            case StatAction.Entertain:
                _playerController.Entertain(change);
                break;
            case StatAction.Feed:
                _playerController.Feed(change);
                break;
            case StatAction.Exercise:
                _playerController.Exercise(change);
                break;
            case StatAction.None:
                break;
        }
    }

    private void CheckRange()
    {
        if (proximity < maxDist)
        {
            inRange = true;
        }
        else
        {
            inRange = false;
        }
    }

    // calculates damage rates based on distance to player
    private void CalculateChange()
    {
        if (proximity < maxDist && !actioned)
        {
            // Debug.Log($"proximity = {proximity}");
            change = (1 - proximity / maxDist) * maxChange * Time.deltaTime;
        }
        else if (actioned)
        {
            change = maxChange * Time.deltaTime;
        }
        else
        {
            change = 0f;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, player.transform.position);
        Gizmos.DrawSphere(transform.position, maxDist);
    }
}
