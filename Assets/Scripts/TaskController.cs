using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Task
{
    public const int BASE_POINTS = 10;

    public string TaskName { get; private set; }
    public string Message { get; protected set; }
    public int Points { get; protected set; }
    public GameObject Sticky { get; set; }
    public GameObject Arrow { get; set; }

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

    public virtual bool IsDone()
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

public class StorageTask : Task
{
    private int correctSpot;
    public StorageTask(string TaskName, string Message, int Points = BASE_POINTS) : base(TaskName, Message, Points)
    {
    }

    public override void AddObject(GameObject obj)
    {
        base.AddObject(obj);
        obj.AddComponent<StorageController>();
    }

    public override bool IsDone()
    {
        for (int i = 0; i < objects.Count; ++i)
        {
            if (objects[i].GetComponent<InteractiveController>().Done)
            {
                if (i != correctSpot)
                {
                    Points /= -2;
                }
                return true;
            }
        }

        return false;
    }

    public void AddObjects()
    {
        GameObject[] storageObjects = GameObject.FindGameObjectsWithTag("Storage");
        for (int i = 0; i < storageObjects.Length; ++i)
        {
            AddObject(storageObjects[i]);
            storageObjects[i].GetComponent<InteractiveController>().TurnOn();
        }
    }

    public void NewCorrectSpot()
    {
        correctSpot = Random.Range(0, objects.Count);
        Message = "put food into the " + objects[correctSpot].name;
    }
}


public class TelephoneTask : Task
{
    public TelephoneTask(string TaskName, string Message, int Points = BASE_POINTS) : base(TaskName, Message, Points) { }

    public override void AddObject(GameObject obj)
    {
        base.AddObject(obj);
        obj.AddComponent<TelephoneController>();
    }
}

public class ToiletTask : Task
{
    public ToiletTask(string TaskName, string Message, int Points = BASE_POINTS) : base(TaskName, Message, Points){}

    public override void AddObject(GameObject obj)
    {
        base.AddObject(obj);
        obj.AddComponent<ToiletController>();
    }
}

public class TaskController : MonoBehaviour
{
    private MCController player;

    public Text productivity;
    private int tasksComplete = 0;

    public List<Task> tasks = new List<Task>() { };
    private GameObject stickyNotePrefab, stickyNoteContainer, arrowPrefab;

    public static bool GameOver = false;

    public void Start()
    {
        // Init references
        player = GameObject.Find("Player").GetComponent<MCController>();
        stickyNotePrefab = (GameObject)Resources.Load("Sticky-note-prefab");
        stickyNoteContainer = GameObject.Find("List_of_tasks");
        arrowPrefab = (GameObject)Resources.Load("Arrow-prefab");

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

        // Phone task
        TelephoneTask pickupPhone = new TelephoneTask("phone", "pick up phone");
        AddTask(pickupPhone);

        // Toilet task
        ToiletTask unclogToilet = new ToiletTask("toilet", "unclog toilet");
        unclogToilet.AddObject(GameObject.Find("Toilet"));
        AddTask(unclogToilet);

        // Storage Task
        StorageTask storeFood = new StorageTask("store", "put food away");
        storeFood.AddObjects();
        storeFood.NewCorrectSpot();
        AddTask(storeFood);
    }

    public void Update()
    {
        RemoveFinishedTasks();

        productivity.text = "Productivity: \n" + tasksComplete + " Tasks complete";
    }

    private void AddTask(Task task)
    {
        tasks.Add(task);
        GameObject newNote = Instantiate(stickyNotePrefab);
        newNote.GetComponentInChildren<Text>().text = task.Message;
        newNote.transform.SetParent(stickyNoteContainer.transform);
        task.Sticky = newNote;

        foreach (GameObject obj in task.objects)
        {
            GameObject newArrow = Instantiate(arrowPrefab, obj.transform);
            task.Arrow = newArrow;
        }
    }

    private void RemoveFinishedTasks()
    {
        List<Task> removeList = new List<Task>();
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

                removeList.Add(task);
                tasksComplete++;
            }
        }
        foreach(Task task in removeList)
        {
            tasks.Remove(task);
        }
    }

}