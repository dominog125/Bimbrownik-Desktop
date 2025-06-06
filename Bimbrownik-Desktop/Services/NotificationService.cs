using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows;
using Bimbrownik_Desktop.Models;

namespace Bimbrownik_Desktop.Services;

public class NotificationService
{
    private static readonly string OfflineFolder =
        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Offline");

    private static readonly string FilePath =
        Path.Combine(OfflineFolder, "notifications.json");

    public NotificationService()
    {
        if (!Directory.Exists(OfflineFolder))
            Directory.CreateDirectory(OfflineFolder);

        if (!File.Exists(FilePath))
            File.WriteAllText(FilePath, "[]");
    }

    public List<Notification> LoadAll()
    {
        try
        {
            var json = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<List<Notification>>(json) ?? new List<Notification>();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Błąd wczytywania powiadomień: " + ex.Message);
            return new List<Notification>();
        }
    }

    public void SaveAll(List<Notification> notifications)
    {
        var json = JsonSerializer.Serialize(notifications, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        File.WriteAllText(FilePath, json, Encoding.UTF8);
    }

    public void AddNotification(string message)
    {
        if (string.IsNullOrWhiteSpace(message))
            return;

        var all = LoadAll();
        all.Add(new Notification
        {
            Id = all.Count + 1,
            Message = message.Trim(),
            CreatedAt = DateTime.Now
        });

        SaveAll(all);
    }
}