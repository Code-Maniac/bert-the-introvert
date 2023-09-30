using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] GameObject hungerUi;
    [SerializeField] GameObject fitnessUi;
    [SerializeField] GameObject boredomUi;
    [SerializeField] GameObject personalSpaceUi;
    [SerializeField] private float startHunger = 100f;
    [SerializeField] private float startFitness = 100f;
    [SerializeField] private float startPersonalSpace = 100f;
    [SerializeField] private float startMoney = 50f;
    [SerializeField] private float startBoredom = 100f;

    // rate of decay for each stat (timetaken in s to go from full to zero)
    [SerializeField] private float rateHunger = 20f;
    [SerializeField] private float rateFitness = 10f;
    [SerializeField] private float ratePersonalSpace = 30f;
    [SerializeField] private float rateBoredom = 25f;

    private float currentHunger = 100f;
    private float currentFitness = 100f;
    private float currentPersonalSpace = 100f;
    private float currentMoney = 50f;
    private float currentBoredom = 100f;

    

    // add get set for "current" variables
    public float CurrentHunger
    {
        get => currentHunger;
        set => currentHunger = Mathf.Min(value,startHunger);
    }

    public float CurrentHealth
    {
        get => currentFitness;
        set => currentFitness = Mathf.Min(value,startFitness);
    }

    public float CurrentPersonalSpace
    {
        get => currentPersonalSpace;
        set => currentPersonalSpace = Mathf.Min(value,startPersonalSpace);
    }


    public float CurrentBoredom
    {
        get => currentBoredom;
        set => currentBoredom = Mathf.Min(value,startBoredom);
    }

    // public float CurrentMoney
    // {
    //     get => currentMoney;
    //     set => currentMoney = value;
    // }

    public bool IsAnnoyed()
    {
        return CurrentPersonalSpace <= 0.0f;
    }
    public bool IsUnfit()
    {
        return CurrentHealth <= 0.0f;
    }
    public bool IsBored()
    {
        return CurrentBoredom <= 0.0f;
    }
    public bool IsStarved()
    {
        return CurrentHunger <= 0.0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHunger = startHunger;
        currentFitness = startFitness;
        currentPersonalSpace = startPersonalSpace;
        currentMoney = startMoney;
        currentBoredom = startBoredom;
    }

    // Update is called once per frame
    void Update()
    {
        // update stats values based on their rate of decay
        DecayStats();
        UpdateUI();

        if (IsAnnoyed())
        {
            Debug.Log("Player is annoyed: You lose!");
        }


    }

    private void DecayStats()
    {
        currentBoredom = Mathf.Max(currentBoredom-((startBoredom / rateBoredom) * Time.deltaTime),0);
        currentFitness = Mathf.Max(currentFitness-((startFitness / rateFitness) * Time.deltaTime),0);
        currentHunger = Mathf.Max(currentHunger-((startHunger / rateHunger) * Time.deltaTime),0);
        
    }

    private void UpdateUI()
    {
        hungerUi.GetComponent<TMP_Text>().text = $"Hunger: {Mathf.Round(currentHunger)}";
        fitnessUi.GetComponent<TMP_Text>().text = $"Fitness: {Mathf.Round(currentFitness)}";
        boredomUi.GetComponent<TMP_Text>().text = $"Boredom: {Mathf.Round(currentBoredom)}";
        personalSpaceUi.GetComponent<TMP_Text>().text = $"Personal Space: {Mathf.Round(currentPersonalSpace)}";
    }
}
