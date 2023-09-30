using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float startHunger = 100f;
    [SerializeField] private float startExercise = 100f;
    [SerializeField] private float startPersonalSpace = 100f;
    [SerializeField] private float startMoney = 50f;
    [SerializeField] private float startBoredom = 100f;

    // rate of decay for each stat
    [SerializeField] private float rateHunger = 1f;
    [SerializeField] private float rateExercise = 1f;
    [SerializeField] private float ratePersonalSpace = 1f;
    [SerializeField] private float rateBoredom = 1f;

    private float currentHunger = 100f;
    private float currentExercise = 100f;
    private float currentPersonalSpace = 100f;
    private float currentMoney = 50f;
    private float currentBoredom = 100f;

    // add get set for "current" variables
    public float CurrentHunger
    {
        get => currentHunger;
        set => currentHunger = value;
    }

    public float CurrentExercise
    {
        get => currentExercise;
        set => currentExercise = value;
    }

    public float CurrentPersonalSpace
    {
        get => currentPersonalSpace;
        set => currentPersonalSpace = value;
    }

    public float CurrentMoney
    {
        get => currentMoney;
        set => currentMoney = value;
    }

    public float CurrentBoredom
    {
        get => currentBoredom;
        set => currentBoredom = value;
    }

    public bool IsAnnoyed()
    {
        return CurrentPersonalSpace <= 0.0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHunger = startHunger;
        currentExercise = startExercise;
        currentPersonalSpace = startPersonalSpace;
        currentMoney = startMoney;
        currentBoredom = startBoredom;
    }

    // Update is called once per frame
    void Update()
    {
        // update stats values based on their rate of decay

        // not quite as simple as this some stats will not be decaying at times due

        // print current values to debug
        Debug.Log($"hunger - {currentHunger}");
        Debug.Log($"exercise - {currentExercise}");
        Debug.Log($"personal space - {currentPersonalSpace}");
        Debug.Log($"money - {currentMoney}");
        Debug.Log($"boredom - {currentBoredom}");

        if (IsAnnoyed())
        {
            Debug.Log("Player is annoyed: You lose!");
        }

    }
}
