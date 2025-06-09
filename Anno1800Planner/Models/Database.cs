using Anno1800Planner.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

public class Database
{
    // --- Singleton ---
    public static Database Instance { get; } = LoadOrCreate();
    private Database() { }

    // --- Data ---
    public List<Plan> Plans { get; set; } = new();
    public List<ProductionChain> ProductionChains { get; set; } = new();

    public static ProductionChain Get(Guid guid) => Instance.ProductionChains.First(x => x.Id == guid);

    public static Func<Guid,ProductionChain> ProductionChainResolver = id => Get(id);

    // --- Dirty State ---
    public bool HasUnsavedChanges { get; private set; }
    public event Action<bool>? DirtyStateChanged;

    public void MarkDirty()
    {
        if (!HasUnsavedChanges)
        {
            HasUnsavedChanges = true;
            DirtyStateChanged?.Invoke(true);
        }
    }

    public void MarkClean()
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

    private static readonly string SavePath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
        "Anno1800Planner",
        "db.json");

    private static string BackupPath => Path.Combine(
        Path.GetDirectoryName(SavePath)!,
        $"db_{DateTime.Now:yyyyMMdd_HHmmss}.bak.json");

    public void Save()
    {
        Directory.CreateDirectory(Path.GetDirectoryName(SavePath)!);

        if (File.Exists(SavePath))
        {
            File.Copy(SavePath, BackupPath, overwrite: true);
        }

        var json = JsonSerializer.Serialize(this, _jsonOptions);
        File.WriteAllText(SavePath, json);
        MarkClean();
    }

    private static Database LoadOrCreate()
    {
        if (!File.Exists(SavePath))
            return new Database();

        var json = File.ReadAllText(SavePath);
        return JsonSerializer.Deserialize<Database>(json, _jsonOptions) ?? new Database();
    }
}