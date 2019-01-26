using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Task
{
    public string TaskName { get; private set; }
    public string Message { get; private set; }
    public bool Done { get; private set; }
    public int Points { get; private set; }

    private List<GameObject> objects = new List<GameObject>() { };

    public Task(string TaskName, string Message, int Points = 10)
    {
        this.TaskName = TaskName;
        this.Message = Message;
        this.Points = Points;
        Done = false;
    }

    public bool IsDone()
    {
        bool done = true;

        foreach (GameObject obj in objects)
        {
            //if (obj.GetComponent<InteractiveController>().st)
        }

        return done;
    }
}

public class TaskController : MonoBehaviour
{
    public List<Task> tasks = new List<Task>() { };
    private GameObject stickyNotePrefab, stickyNoteContainer;

    public void Start()
    {
        // Init references
        stickyNotePrefab = (GameObject)Resources.Load("Sticky-note-prefab");
        stickyNoteContainer = GameObject.Find("List_of_tasks");

        Task task1 = new Task("task1", "hello1"), task2 = new Task("task2", "hello2");

        AddTask(task1);
        AddTask(task2);
    }

    public void Update()
    {
        
    }

    private void AddTask(Task task)
    {
        tasks.Add(task);
        GameObject newNote = Instantiate(stickyNotePrefab);
        newNote.GetComponentInChildren<Text>().text = task.Message;
        newNote.transform.SetParent(stickyNoteContainer.transform);
    }

}