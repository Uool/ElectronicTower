using UnityEngine;
using UnityEditor;
using System.IO;

public class CSVtoSo
{
    private static string _turretDataCSVPath = "/Editor/CSV/TurretData.csv";
    private static string _powerPoleDataCSVPath = "/Editor/CSV/PowerPoleData.csv";
    private static string _turretShopDataCSVPath = "/Editor/CSV/TurretShopData.csv";
    private static string _enemyDataCSVPath = "/Editor/CSV/EnemyData.csv";
    private static string _waveDataCSVPath = "/Editor/CSV/WaveData.csv";


    [MenuItem("CSV/TurretData")]
    public static void GenerateTurretData()
    {
        string[] allLines = File.ReadAllLines(Application.dataPath + _turretDataCSVPath);

        for (int i = 1; i < allLines.Length; i++)
        {
            string[] splitData = allLines[i].Split(',');
            TurretData turretData = ScriptableObject.CreateInstance<TurretData>();
            turretData.TurretDataInit(splitData);

            AssetDatabase.CreateAsset(turretData, $"Assets/Resources/ScriptableObject/Turret/{turretData.TurretName}.asset");
        }
        AssetDatabase.SaveAssets();
    }

    [MenuItem("CSV/TurretData")]
    public static void GeneratePowerPoleData()
    {
        string[] allLines = File.ReadAllLines(Application.dataPath + _turretDataCSVPath);

        for (int i = 1; i < allLines.Length; i++)
        {
            string[] splitData = allLines[i].Split(',');
            PowerPoleData powerPoleData = ScriptableObject.CreateInstance<PowerPoleData>();
            powerPoleData.PowerPoleDataInit(splitData);

            AssetDatabase.CreateAsset(powerPoleData, $"Assets/Resources/ScriptableObject/Turret/{powerPoleData.TurretName}.asset");
        }
        AssetDatabase.SaveAssets();
    }

    [MenuItem("CSV/TurretShopData")]
    public static void GenerateTurretShopData()
    {
        string[] allLines = File.ReadAllLines(Application.dataPath + _turretShopDataCSVPath);

        for (int i = 1; i < allLines.Length; i++)
        {
            string[] splitData = allLines[i].Split(',');
            TurretShopData turretShopData = ScriptableObject.CreateInstance<TurretShopData>();
            turretShopData.TurretShopDataInit(splitData);

            AssetDatabase.CreateAsset(turretShopData, $"Assets/Resources/ScriptableObject/Shop/{turretShopData.fileName}.asset");
        }
        AssetDatabase.SaveAssets();
    }

    [MenuItem("CSV/EnemyWave")]
    public static void GenerateEnemyWaveData()
    {
        string[] allLines = File.ReadAllLines(Application.dataPath + _waveDataCSVPath);

        for (int i = 1; i < allLines.Length; i++)
        {
            string[] splitData = allLines[i].Split(',');
            WaveData waveData = ScriptableObject.CreateInstance<WaveData>();
            waveData.WaveDataInit(splitData);

            AssetDatabase.CreateAsset(waveData, $"Assets/Resources/ScriptableObject/Wave/{waveData.fileName}.asset");
        }
        AssetDatabase.SaveAssets();
    }

    [MenuItem("CSV/EnemyData")]
    public static void GenerateEnemyData()
    {
        string[] allLines = File.ReadAllLines(Application.dataPath + _enemyDataCSVPath);

        for (int i = 1; i < allLines.Length; i++)
        {
            string[] splitData = allLines[i].Split(',');
            EnemyData enemyData = ScriptableObject.CreateInstance<EnemyData>();
            enemyData.EnemyDataInit(splitData);

            AssetDatabase.CreateAsset(enemyData, $"Assets/Resources/ScriptableObject/Enemy/{enemyData.fileName}.asset");
        }
        AssetDatabase.SaveAssets();
    }
}
