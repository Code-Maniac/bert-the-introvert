// using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

enum StatAction
{
    Annoy,
    Entertain,
    Exercise,
    Feed,
    None
}
// [Serializable]
// public class Audio
// {
//     public AudioClip audioclip;
// }

public class Proximity : MonoBehaviour
{
    float proximity;
    float change;
    bool inRange;
    public bool inUse = false;
    Vector3 playerPos;
    Vector3 enemyPos;
    AudioSource audioSource;

    [SerializeField] GameObject text;
    [SerializeField] bool alwaysShowText = false;
    [SerializeField] bool actioned = false;
    [SerializeField] GameObject player;

    [SerializeField] private List<AudioClip> audioclips;


    [SerializeField] StatAction statAction;
    [SerializeField] float maxChange = 1f;
    [SerializeField] float maxDist = 10f;


    private PlayerController _playerController;

    // Start is called before the first frame update
    void Start()
    {
        _playerController = player.GetComponent<PlayerController>();
        audioSource = GetComponent<AudioSource>();
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
        CheckAction();
    }

    private void CheckAction()
    {
        if ((!actioned || Input.GetKey("space")) && inRange)
        {
            inUse = true;

            CalculateChange();
            MakeChange();
            PlayAudio();

        }
        else if (Input.GetKeyUp("space"))
        {
            inUse = false;
        }
    }

    private void PlayAudio()
    {
        if (!audioSource.isPlaying && audioclips.Count > 0)
        {
            audioSource.PlayOneShot(audioclips[Random.Range(0, audioclips.Count)]);
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

        if (inRange || alwaysShowText)
        {
            text.GetComponent<MeshRenderer>().enabled = true;
        }
        else
        {
            text.GetComponent<MeshRenderer>().enabled = false;
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
