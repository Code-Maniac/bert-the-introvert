using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tasks : MonoBehaviour
{

    GameObject nextTask;
    int jobIndex;
    int taskIndex;
    bool jobsComplete = false;



    static String[] job1 = { "Fridge", "Treadmill", "Bin" };
    static String[] job2 = { "Fridge", "Treadmill" };
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
        if (!jobsComplete && nextTask.GetComponent<Proximity>().inUse)
        {
            Debug.Log($"{jobs[jobIndex][taskIndex]} JOB COMPLETE");
            taskIndex += 1;
            if (taskIndex >= jobs[jobIndex].Length)
            {
                Debug.Log("TASK COMPLETE!!");
                jobIndex += 1;
                taskIndex = 0;
            }
            if (jobIndex < jobs.Length)
            {
                Debug.Log($"Setting Task {jobs[jobIndex][taskIndex]}");
                nextTask = GameObject.FindWithTag(jobs[jobIndex][taskIndex]);
            }
            else
            {
                Debug.Log("ALL JOBS COMPLETE!!");
                jobsComplete = true;
            }

        }
    }
}







// public class Tasks : MonoBehaviour
// {
//     [System.Serializable]
//     public struct Jobs
//     {
//         public GameObject taskName;
//         public int jobNumber;
//     }
//     public Jobs[] jobStruct;

//     GameObject nextTask;
//     int jobIndex;
//     int taskIndex;
//     bool jobsComplete = false;




//     static String[] job1 = { "Fridge", "Treadmill", "Bin" };
//     static String[] job2 = { "Fridge", "Treadmill" };
//     String[][] jobs = { job1, job2 };


//     void Start()
//     {
//         Debug.Log(jobStruct[0]);


//         taskIndex = 0;
//         jobIndex = 0;
//         Debug.Log($"Setting Task {jobs[jobIndex][taskIndex]}");
//         nextTask = GameObject.FindWithTag(jobs[jobIndex][taskIndex]);
//     }

//     void Update()
//     {
//         if (!jobsComplete && nextTask.GetComponent<Proximity>().inUse)
//         {
//             Debug.Log($"{jobs[jobIndex][taskIndex]} JOB COMPLETE");
//             taskIndex += 1;
//             if (taskIndex >= jobs[jobIndex].Length)
//             {
//                 Debug.Log("TASK COMPLETE!!");
//                 jobIndex += 1;
//                 taskIndex = 0;
//             }
//             if (jobIndex < jobs.Length)
//             {
//                 Debug.Log($"Setting Task {jobs[jobIndex][taskIndex]}");
//                 nextTask = GameObject.FindWithTag(jobs[jobIndex][taskIndex]);
//             }
//             else
//             {
//                 Debug.Log("ALL JOBS COMPLETE!!");
//                 jobsComplete = true;
//             }

//         }
//     }
// }
