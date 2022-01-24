using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
public static class SaveSystem
{
  public static void SavePlayer(PlayerCombat player)
    {
        //Save player stream
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.dat";
        FileStream stream = new FileStream(path, FileMode.Create);
        PlayerData data = new PlayerData(player);
        formatter.Serialize(stream, data);
        stream.Close();
    }


    public static PlayerData LoadPlayer()
    {
        //Load player stream
        string path = Application.persistentDataPath + "/player.dat";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
          PlayerData data =  formatter.Deserialize(stream) as PlayerData;
            stream.Close(); 
            return data;
        }
        else
        {
            Debug.LogError("Save File Not Found " + path);
            return null;
        }
    }
}
