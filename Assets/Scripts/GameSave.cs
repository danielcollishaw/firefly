#region Copyright
/*---------------------------------------------------------------*/
/*        File: GameSave.cs                                      */
/*        Firefly                                                */
/*        AI For Game Programming - CAP 4053                     */
/*        Copyright (c) 2023 Serenity Studios                    */
/*        All rights reserved.                                   */
/*        Made with love, by Justin Sasso.                       */
/*---------------------------------------------------------------*/
#endregion

using System;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class GameSave
{
    public bool[] LevelsUnlocked { get; set; } = new bool[] { };

    private const string SAVE_PATH = "save.json";
    public GameSave() {  }

    public static GameSave LoadGameSave()
    {
        string jsonData;

        try
        {
            using (StreamReader streamRdr = new StreamReader(CreatePath()))
            {
                jsonData = streamRdr.ReadToEnd();
            }
        }
        catch (IOException ex)
        {
            Debug.Log("Couldn't LoadGameSave. Could be first startup: " + ex);
            return new GameSave();
        }

        return DeserializeGameSave(jsonData);
    }
    public static bool SaveGameSave(GameSave gameSave)
    {
        string jsonData = SerializeGameSave(gameSave);

        using (StreamWriter outputFile = new StreamWriter(CreatePath()))
        {
            outputFile.Write(jsonData);
        }

        return true;
    }
    private static string CreatePath()
    {
        string path = string.Format("{0}{1}", Application.dataPath, SAVE_PATH);
        Debug.Log("Path created: " + path); // DEBUG
        return path;
    }

    private static string SerializeGameSave(GameSave gameSave) => JsonConvert.SerializeObject(gameSave);
    private static GameSave DeserializeGameSave(string jsonData) => JsonConvert.DeserializeObject<GameSave>(jsonData);
}
