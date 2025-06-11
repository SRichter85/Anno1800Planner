using Anno1800Planner.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

/// <summary>
/// A simple data-transfer-object used purely for serialization.
/// It holds the lists of data that are saved to and loaded from the disk.
/// </summary>
internal class DatabaseIO
{
    public List<Plan> Plans { get; set; } = new();
    public List<ProductionChain> ProductionChains { get; set; } = new();
}

/// <summary>
/// Provides a global static API for accessing and managing the application's user data.
/// Handles loading, saving, and change tracking.
/// </summary>
public static class DB
{
    // --- Data ---
    // The database now starts empty. Data is only loaded via an explicit call to DB.Load().
    private static DatabaseIO _data = new DatabaseIO();

    public static List<Plan> Plans => _data.Plans;
    public static List<ProductionChain> ProductionChains => _data.ProductionChains;

    public static ProductionChain GetProductionChain(Guid guid) => ProductionChains.First(x => x.Id == guid);
    public static Func<Guid, ProductionChain> ProductionChainResolver = id => GetProductionChain(id);

    public static Plan GetPlan(Guid guid) => Plans.First(x => x.Id == guid);
    public static Func<Guid, Plan> PlanResolver = id => GetPlan(id);

    // --- Dirty State & Events ---
    public static bool HasUnsavedChanges { get; private set; }
    public static event Action<bool>? DirtyStateChanged;
    public static event Action? DatabaseLoaded;

    public static void MarkDirty()
    {
        if (!HasUnsavedChanges)
        {
            HasUnsavedChanges = true;
            DirtyStateChanged?.Invoke(true);
        }
    }

    public static void MarkClean()
    {
        if (HasUnsavedChanges)
        {
            HasUnsavedChanges = false;
            DirtyStateChanged?.Invoke(false);
        }
    }

    // --- Save / Load ---
    private static readonly JsonSerializerOptions _jsonOptions = new()
    {
        WriteIndented = true,
        Converters = { new JsonStringEnumConverter() }
    };

    public static string GetSavePath()
    {
        return Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "Anno1800Planner",
            "db.json");
    }

    private static string GetBackupPath()
    {
        string savePath = GetSavePath();
        return Path.Combine(
            Path.GetDirectoryName(savePath)!,
            $"db_{DateTime.Now:yyyyMMdd_HHmmss}.bak.json");
    }

    public static void Save()
    {
        string savePath = GetSavePath();
        Directory.CreateDirectory(Path.GetDirectoryName(savePath)!);

        if (File.Exists(savePath))
        {
            File.Copy(savePath, GetBackupPath(), overwrite: true);
        }

        // Serialize the internal _data object
        var json = JsonSerializer.Serialize(_data, _jsonOptions);
        File.WriteAllText(savePath, json);
        MarkClean();
    }

    /// <summary>
    /// Loads the database from the disk, replacing the data in the current singleton instance.
    /// This method will throw exceptions (e.g., JsonException, IOException) if loading fails.
    /// </summary>
    public static void Load()
    {
        string savePath = GetSavePath();
        if (!File.Exists(savePath))
            return; // Nothing to load, not an error.

        // The try-catch block is removed. Exceptions will now be passed to the caller.
        var json = File.ReadAllText(savePath);
        var loadedData = JsonSerializer.Deserialize<DatabaseIO>(json, _jsonOptions);

        if (loadedData != null)
        {
            _data = loadedData;
            MarkClean();
            DatabaseLoaded?.Invoke();
        }
        else
        {
            // This can happen if the JSON file just contains "null".
            throw new JsonException("The database file could not be deserialized or is empty.");
        }
    }
}
