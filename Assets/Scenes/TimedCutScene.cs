using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimedCutScene : MonoBehaviour
{
    [SerializeField] int secondsToWait = 4;

    // Start is called before the first frame update
    void Start()
{
    StartCoroutine(waiter());
}

IEnumerator waiter()
{
        yield return new WaitForSeconds(secondsToWait);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
}
}

