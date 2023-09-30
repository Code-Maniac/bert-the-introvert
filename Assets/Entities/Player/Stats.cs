using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField] float startHunger = 100f;
    [SerializeField] float startExercise = 100f;
    [SerializeField] float startPersonalSpace = 100f;
    [SerializeField] float startMoney = 50f;
    [SerializeField] float startBoredom = 100f;
    
    [SerializeField] float rateHunger = 1f;
    [SerializeField] float rateExercise = 1f;
    [SerializeField] float ratePersonalSpace = 1f;
    [SerializeField] float rateMoney = 0f;
    [SerializeField] float rateBoredom = 1f;


    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
