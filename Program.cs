using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Carregar as tarefas
        List<Task> tasks = TaskManager.LoadTasks();

        // Verifica se o usuário forneceu algum comando
        if (args.Length == 0)
        {
            Console.WriteLine("Por favor, forneça um comando.");
            return;
        }

        string command = args[0].ToLower();

        switch (command)
        {
            case "add":
                AddTask(args, tasks);
                break;
            case "update":
                UpdateTask(args, tasks);
                break;
            case "delete":
                DeleteTask(args, tasks);
                break;
            case "mark-in-progress":
                MarkTaskInProgress(args, tasks);
                break;
            case "mark-done":
                MarkTaskDone(args, tasks);
                break;
            case "list":
                ListTasks(args, tasks);
                break;
            default:
                Console.WriteLine("Comando desconhecido.");
                break;
        }

        // Salvar as tarefas após qualquer operação
        TaskManager.SaveTasks(tasks);
    }

    // Adicionar nova tarefa
    static void AddTask(string[] args, List<Task> tasks)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Por favor, forneça uma descrição para a tarefa.");
            return;
        }

        string description = string.Join(" ", args[1..]);
        int newId = tasks.Count == 0 ? 1 : tasks[^1].Id + 1;
        tasks.Add(new Task { Id = newId, Description = description });
        Console.WriteLine($"Tarefa adicionada com sucesso (ID: {newId}).");
    }

    // Atualizar uma tarefa existente
    static void UpdateTask(string[] args, List<Task> tasks)
    {
        if (args.Length < 3 || !int.TryParse(args[1], out int taskId))
        {
            Console.WriteLine("Por favor, forneça um ID válido e a nova descrição.");
            return;
        }

        Task? task = tasks.Find(t => t.Id == taskId);
        if (task == null)
        {
            Console.WriteLine($"Tarefa com ID {taskId} não encontrada.");
            return;
        }

        task.Description = string.Join(" ", args[2..]);
        Console.WriteLine($"Tarefa {taskId} atualizada com sucesso.");
    }

    // Deletar uma tarefa
    static void DeleteTask(string[] args, List<Task> tasks)
    {
        if (args.Length < 2 || !int.TryParse(args[1], out int taskId))
        {
            Console.WriteLine("Por favor, forneça um ID válido.");
            return;
        }

        Task? task = tasks.Find(t => t.Id == taskId);
        if (task == null)
        {
            Console.WriteLine($"Tarefa com ID {taskId} não encontrada.");
            return;
        }

        tasks.Remove(task);
        Console.WriteLine($"Tarefa {taskId} deletada com sucesso.");
    }

    // Marcar uma tarefa como "in progress"
    static void MarkTaskInProgress(string[] args, List<Task> tasks)
    {
        if (args.Length < 2 || !int.TryParse(args[1], out int taskId))
        {
            Console.WriteLine("Por favor, forneça um ID válido.");
            return;
        }

        Task? task = tasks.Find(t => t.Id == taskId);
        if (task == null)
        {
            Console.WriteLine($"Tarefa com ID {taskId} não encontrada.");
            return;
        }

        task.Status = "in-progress";
        Console.WriteLine($"Tarefa {taskId} marcada como 'in progress'.");
    }

    // Marcar uma tarefa como "done"
    static void MarkTaskDone(string[] args, List<Task> tasks)
    {
        if (args.Length < 2 || !int.TryParse(args[1], out int taskId))
        {
            Console.WriteLine("Por favor, forneça um ID válido.");
            return;
        }

        Task? task = tasks.Find(t => t.Id == taskId);
        if (task == null)
        {
            Console.WriteLine($"Tarefa com ID {taskId} não encontrada.");
            return;
        }

        task.Status = "done";
        Console.WriteLine($"Tarefa {taskId} marcada como 'done'.");
    }

    // Listar as tarefas, com ou sem filtro de status
    static void ListTasks(string[] args, List<Task> tasks)
    {
        string filter = args.Length > 1 ? args[1].ToLower() : "all";

        List<Task> filteredTasks = filter switch
        {
            "done" => tasks.FindAll(t => t.Status == "done"),
            "todo" => tasks.FindAll(t => t.Status == "todo"),
            "in-progress" => tasks.FindAll(t => t.Status == "in-progress"),
            _ => tasks
        };

        if (filteredTasks.Count == 0)
        {
            Console.WriteLine("Nenhuma tarefa encontrada.");
        }
        else
        {
            foreach (var task in filteredTasks)
            {
                Console.WriteLine($"ID: {task.Id}, Descrição: {task.Description}, Status: {task.Status}");
            }
        }
    }
}
