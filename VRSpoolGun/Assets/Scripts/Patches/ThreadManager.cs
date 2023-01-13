using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreadManager : MonoBehaviour
{
    private static ThreadManager instance;

    public static ThreadManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ThreadManager>();

                if (instance == null)
                    instance = new GameObject("ThreadManager").AddComponent<ThreadManager>();
            }

            return instance;
        }
    }

    [SerializeField] private List<Thread> threads;

    private Thread current;


    private void Start()
    {
        if (current == null && threads.Count > 0)
            current = threads[0];
    }

    // Set the active thread material for use in patch creation.
    public void SetActiveThread(ThreadType type)
    {
        current = threads.Find((thread) => type == thread.threadType);
    }

    public Thread GetActiveThread()
    {
        return current;
    }

    public Thread GetThread(ThreadType type)
    {
        return threads[(int)type];
    }
}
