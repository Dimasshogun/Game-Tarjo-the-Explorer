using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Code.Scripts.SaveSystem
{
    public static class SaveDataManager
    {
	    private static readonly BinaryFormatter Formatter = new BinaryFormatter();
	    private const string LevelProgressFile = "levelProgress";

	    public static void SaveLevelProgress(LevelProgress levelProgress)
		{
			levelProgress.lastSave = DateTime.Now;

			string savePath = $"{Application.persistentDataPath}/{LevelProgressFile}";
			FileStream stream = new FileStream(savePath, FileMode.Create);

			SavedLevelProgress data = new SavedLevelProgress(levelProgress);

			Formatter.Serialize(stream, data);

			stream.Close();
		}

		public static LevelProgress LoadLevelProgress()
		{
			string savePath = $"{Application.persistentDataPath}/{LevelProgressFile}";
			if (!File.Exists(savePath))
			{
				return new LevelProgress();
			}

			FileStream stream = new FileStream(savePath, FileMode.Open);

			SavedLevelProgress data = Formatter.Deserialize(stream) as SavedLevelProgress;

			stream.Close();

			return new LevelProgress(data);
		}

		public static void ClearSave(string fileName)
		{
			string savePath = $"{Application.persistentDataPath}/{fileName}";
			if (!File.Exists(savePath))
			{
				return;
			}

			File.Delete(savePath);
			Debug.Log("Save cleared from" + savePath);
		}

		public static bool CheckSave(string fileName)
		{
			string savePath = $"{Application.persistentDataPath}/{fileName}";
			return File.Exists(savePath);
		}
    }
}