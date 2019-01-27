﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Task
{
    public const int BASE_POINTS = 10;

    public string TaskName { get; private set; }
    public string Message { get; private set; }
    public int Points { get; private set; }
    public GameObject Sticky { get; set; }

    public List<GameObject> objects = new List<GameObject>() { };

    public Task(string TaskName, string Message, int Points = BASE_POINTS)
    {
        this.TaskName = TaskName;
        this.Message = Message;
        this.Points = Points;
    }

    public virtual void AddObject(GameObject obj)
    {
        objects.Add(obj);
    }

    public bool IsDone()
    {
        bool done = true;

        foreach (GameObject obj in objects)
        {
            if (!obj.GetComponent<InteractiveController>().Done)
                done = false;
        }

        return done;
    }
}

public class HomeworkTask : Task
{
    public HomeworkTask(string TaskName, string Message, int Points = BASE_POINTS) : base(TaskName, Message, Points){}

    public override void AddObject(GameObject obj)
    {
        base.AddObject(obj);
        obj.AddComponent<DeskController>();
    }
}

public class LaundryTask : Task
{
    public LaundryTask(string TaskName, string Message, int Points = BASE_POINTS) : base(TaskName, Message, Points){}

    public override void AddObject(GameObject obj)
    {
        base.AddObject(obj);
        //obj.AddComponent<DeskController>();
    }
}

public class BedTask : Task
{
    public BedTask(string TaskName, string Message, int Points = BASE_POINTS) : base(TaskName, Message, Points){}

    public override void AddObject(GameObject obj)
    {
        base.AddObject(obj);
        obj.AddComponent<BedController>();
    }
}

public class TaskController : MonoBehaviour
{
    private MCController player;

    public Text productivity;
    private int tasksComplete = 0;

    public List<Task> tasks = new List<Task>() { };
    private GameObject stickyNotePrefab, stickyNoteContainer;

    public void Start()
    {
        // Init references
        player = GameObject.Find("Player").GetComponent<MCController>();
        stickyNotePrefab = (GameObject)Resources.Load("Sticky-note-prefab");
        stickyNoteContainer = GameObject.Find("List_of_tasks");

        // Homework task
        HomeworkTask doHomework = new HomeworkTask("homework", "do homework");
        doHomework.AddObject(GameObject.Find("Desk"));
        AddTask(doHomework);

        // Laundry task
        LaundryTask doLaundry = new LaundryTask("laundry", "do laundry");
        AddTask(doLaundry);

        // Bed task
        BedTask makeBed = new BedTask("bed", "make bed");
        makeBed.AddObject(GameObject.Find("Bed"));
        AddTask(makeBed);
    }

    public void Update()
    {
        RemoveFinishedTasks();

        productivity.text = "Productvity: \n" + tasksComplete + " Tasks";
    }

    private void AddTask(Task task)
    {
        tasks.Add(task);
        GameObject newNote = Instantiate(stickyNotePrefab);
        newNote.GetComponentInChildren<Text>().text = task.Message;
        newNote.transform.SetParent(stickyNoteContainer.transform);
        task.Sticky = newNote;
    }

    private void RemoveFinishedTasks()
    {
        foreach (Task task in tasks)
        {
            if (task.objects.Count > 0 && task.IsDone())
            {
                player.PlayTaskComplete(true);

                Destroy(task.Sticky);
                
                foreach (GameObject obj in task.objects)
                {
                    obj.GetComponent<InteractiveController>().SelfDestruct();
                }

                tasks.Remove(task);
                tasksComplete++;
            }
        }
    }

}