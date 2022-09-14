using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;

#if WINDOWS_UWP
using System;
using Windows.Storage;
using Windows.System;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.Storage.Pickers;
#endif

/// <summary>
/// Class <c>CoordCSV</c> provides a representation of the needed data (Time, Position, Rotation, Finger) for a CSV export.
/// Using Windows Runtime API.
/// </summary>
public class CoordCSV
{
    public double Timestamp { get; set; }
    public string Position { get; set; }
    public string Rotation { get; set; }
    public string Finger { get; set; }

    /// <summary>
    /// Constructor creates a <c>CoordCSV</c> with given parameters
    /// </summary>
    /// <param name="finger">The finger ("Index", "Thumb", or "Marker") corresponding to the data</param>
    /// <param name="timestamp">Timestamp when the data has been taken</param>
    /// <param name="pos">Position (String representation of a Vector3) of the finger</param>
    /// <param name="rot">Rotation (String representation of a Quaternion) of the finger</param>
    public CoordCSV(string finger, double timestamp, string pos, string rot)
    {
        Finger = finger; Timestamp = timestamp; Position = pos; Rotation = rot;
    }

    /// <summary>
    /// Basic constructor.
    /// </summary>
    public CoordCSV()
    {
        Finger = ""; Timestamp = 0.0; Position = ""; Rotation = "";
    }

    /// <summary>
    /// Method <c>SaveRecord</c> saves the <c>CoordCSV</c> containing new data in the corresponding record list
    /// </summary>
    /// <param name="records">List containing all the data</param>
    /// <param name="item"> <c>CoordCSV</c> corresponding to the new data to save</param>
    public void SaveRecord(List<CoordCSV> records, CoordCSV item)
    {
        records.Add(item);
    }

    /// <summary>
    /// Method <c>WriteInCsv</c> writes the records in a .csv file. Files are named by corresponding hand, and the dateTime of writing.
    /// </summary>
    /// <param name="records">List of all the saved data</param>
    /// <param name="folderName">name of the folder in the Future Acces List (cache)</param>
    /// <param name="hand">Which hand has the data been recorded from : 0 for right hand, 1 for left hand, 2 for the object marker. </param>
    public void WriteInCsv(List<CoordCSV> records, string folderName, int hand=0)
    {
        if (records.Count > 0)
        {
            string path = ""; 
            string dateTime = System.DateTime.Now.Day + "-" + System.DateTime.Now.Month + "_" + System.DateTime.Now.Hour + "-" + System.DateTime.Now.Minute + "-" + System.DateTime.Now.Second;
            if (hand == 1)
            {
                path += "LeftHand_" + dateTime + ".csv";
            }
            else if (hand == 2)
            {
                path += "ObjectDetection_" + dateTime + ".csv";
            }
            else
            {
                path += "RightHand_" + dateTime + ".csv";
            }

            List<string> lines = new List<string>();
            if (hand == 2)
            {
                lines.Add("Part,TimeStamp,Position,Rotation(Quaternion)");
            }
            else
            {
                lines.Add("Finger,TimeStamp,Position,Rotation(Quaternion)");
            }
            foreach (CoordCSV data in records)
            {
                lines.Add(data.Finger +"," + data.Timestamp + "," + data.Position + "," + data.Rotation);
            }

#if !UNITY_EDITOR && UNITY_WSA_10_0
UnityEngine.WSA.Application.InvokeOnUIThread(async () =>
        {
                Windows.Storage.StorageFolder folder;
                if(Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.ContainsItem(folderName)){
                    folder =
 await Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.GetFolderAsync(folderName);
                }
                else{
                    var folderPicker = new Windows.Storage.Pickers.FolderPicker();
                    folderPicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
                    folderPicker.FileTypeFilter.Add("*");
                    folder = await folderPicker.PickSingleFolderAsync();
                }
UnityEngine.WSA.Application.InvokeOnAppThread(async () => 
            {
                if (folder != null)
                {
                    // Application now has read/write access to all contents in the picked folder
                    // (including other sub-folder contents)
                    Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.AddOrReplace(folderName, folder);
                    using (StreamWriter writer = new StreamWriter(await folder.OpenStreamForWriteAsync(path, CreationCollisionOption.OpenIfExists)))
                    {
                        foreach(string line in lines){
                            await writer.WriteLineAsync(line);
                        }
                    }
                }
                        }, false);
        }, false);
#endif
        }
    }
}
