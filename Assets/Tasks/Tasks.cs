using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tasks : MonoBehaviour
{
    // Start is called before the first frame update

    GameObject nextTask;
    int jobIndex;
    int taskIndex;



    static String[] job1 = { "Fridge", "Treadmill" };
    static String[] job2 = { "", "" };
    String[][] jobs = { job1, job2 };


    void Start()
    {
        taskIndex = 0;
        jobIndex = 0;
        Debug.Log($"Setting Task {jobs[jobIndex][taskIndex]}");
        nextTask = GameObject.FindWithTag(jobs[jobIndex][taskIndex]);
    }


    // Update is called once per frame
    void Update()
    {
        if (nextTask.GetComponent<Proximity>().inUse)
        {
            Debug.Log($"{jobs[jobIndex][taskIndex]} JOB COMPLETE");
            taskIndex += 1;
            if (taskIndex >= jobs[jobIndex].Length)
            {
                Debug.Log("TASK COMPLETE!!");
                Debug.Log($"taskIndex: {taskIndex}");
                Debug.Log($"jobIndex: {jobIndex}");
                jobIndex += 1;
                taskIndex = 0;
            }
            Debug.Log($"Setting Task {jobs[jobIndex][taskIndex]}");
            nextTask = GameObject.FindWithTag(jobs[jobIndex][taskIndex]);

        }
    }
}
