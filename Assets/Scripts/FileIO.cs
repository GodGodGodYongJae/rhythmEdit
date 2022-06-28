using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class FileIO : MonoBehaviour
{
    public Sheet sheet;
    public SheetParser sheetParser;
    public SheetWriter SheetWriter;
    public SheetEditor sheetEditor;
    public GridGenerator gridGenerator;
    public NoteGenerator noteGenerator;
    public Music music;
    public InputField musicName;
    string basePath;
    string filePath = Path.Combine(Application.streamingAssetsPath, "Example.txt");
    private void Start()
    {
        basePath = Application.dataPath + "/Resources/" + sheet.fileName;
    }

    public void SetBasePath()
    {
        sheet.fileName = musicName.text;
        basePath = Application.dataPath + "/Resources/" + sheet.fileName;
    }

    public void Save()
    {
        sheet.noteLine1.Sort();
        sheet.noteLine2.Sort();
        sheet.noteLine3.Sort();
        sheet.noteLine4.Sort();

        DirectoryInfo directoryInfo = new DirectoryInfo(basePath);
        if (!directoryInfo.Exists)
        {
            directoryInfo.Create();
        }
        string filePath = Path.Combine(Application.streamingAssetsPath, sheet.fileName + "_data.txt");


        using (StreamWriter streamWriter = new StreamWriter(new FileStream(filePath, FileMode.Create, FileAccess.Write), System.Text.Encoding.Unicode))
        {
            streamWriter.Write(SheetWriter.WriteSheetInfo());
            streamWriter.Write(SheetWriter.WriteContentInfo());
            streamWriter.Write(SheetWriter.WriteNoteInfo());
        }
    }

    public void Load()
    {
        string data = "";
        sheet.Init();

        string filePath = Path.Combine(Application.streamingAssetsPath, sheet.fileName + "_data.txt");
        using (StreamReader streamReader = new StreamReader(filePath))
        {
            while ((data = streamReader.ReadLine()) != null)
            {
                sheetParser.Parse(data);
            }

            music.Init();
            sheetEditor.Init();
            gridGenerator.Init();
            noteGenerator.GenNote();
        }
        sheetParser.isfirstRead = false;


        //TextAsset asset = Resources.Load(sheet.fileName + "/" +sheet.fileName+"_data") as TextAsset;
        //string str = asset.text;
        //TextReader txtreader = new StringReader(str);
        //StringReader streader = new StringReader(str);

        //while ((data = streader.ReadLine()) != null)
        //{
        //    sheetParser.Parse(data);
        //}

        //music.Init();
        //sheetEditor.Init();
        //gridGenerator.Init();
        //noteGenerator.GenNote();

        //sheetParser.isfirstRead = false;
    }
}
