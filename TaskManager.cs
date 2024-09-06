using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class TaskManager
{
    private static string filePath = "tasks.json";
    private static List<Task> tasks = new List<Task>();

    public static List<Task> LoadTasks()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            tasks = JsonSerializer.Deserialize<List<Task>>(json) ?? new List<Task>();
        }
        return tasks;
    }

    public static void SaveTasks(List<Task> tasks)
    {
        string json = JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(filePath, json);
    }
}
