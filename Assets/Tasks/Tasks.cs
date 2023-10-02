using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[Serializable]
public class Task
{
    public bool completed;
    public GameObject taskObject;
    public string jobDescription;
}


public class Tasks : MonoBehaviour
{
    GameObject nextTask;
    int taskIndex;
    [SerializeField] bool jobsComplete = false;
    float dayTime;

    [SerializeField] GameObject clock;

    [SerializeField] float dayLength = 30f;
    [SerializeField] private List<Task> tasks;
    [SerializeField] private TMP_Text textField;

    private void CheckProgress()
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine("<u>TO DO</u>");
        sb.AppendLine();
        bool firstTask = true;

        for (int i = 0; i < tasks.Count; i++)
        {
            if (tasks[i].completed)
            {
                sb.Append("<s>");
            }
            if (!tasks[i].completed && firstTask)
            {
                sb.Append("<b>");
            }
            else if (!tasks[i].completed)
            {
                sb.Append("<color=\"grey\">");
            }
            sb.Append(tasks[i].jobDescription);
            if (!tasks[i].completed && firstTask)
            {
                sb.Append("</b>");
                firstTask = false;
            }
            else if (!tasks[i].completed)
            {
                sb.Append("</color=\"grey\">");
            }
            if (tasks[i].completed)
            {
                sb.Append("</s>");
            }
            sb.Append("\n");
        }
        textField.SetText(sb.ToString());

    }

    void Start()
    {
        taskIndex = 0;
        dayTime = dayLength;
        nextTask = tasks[taskIndex].taskObject;
    }

    private void OnValidate()
    {
        CheckProgress();
    }

    // Update is called once per frame
    void Update()
    {
        CheckProgress();

        IncreaseTime();

        if (!jobsComplete && nextTask.GetComponent<Proximity>().inUse)
        {
            Debug.Log($"{tasks[taskIndex].jobDescription} JOB COMPLETE");
            tasks[taskIndex].completed = true;
            taskIndex += 1;
            if (taskIndex >= tasks.Count)
            {
                Debug.Log("TASKS COMPLETE!!");
                jobsComplete = true;
            }
            else
            {
                Debug.Log($"Setting Task {tasks[taskIndex].jobDescription}");
                nextTask = tasks[taskIndex].taskObject;
            }


        }
    }

    private void IncreaseTime()
    {
        dayTime -= Time.deltaTime;
        clock.GetComponent<Image>().fillAmount = dayTime / dayLength;
        if (dayTime <= 0)
        {
            if (jobsComplete)
            {
                NextLevel();
            }
            else
            {
                GameOver();
            }
        }
    }

    private static void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private static void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
}





