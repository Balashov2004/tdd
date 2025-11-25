using TagsCloudVisualization.Interface;

namespace TagsCloudVisualization;

public class WorkWithWorkWithFile :  IWorkWithFile
{
    public string ReadAllText(string path)
    {
        return File.ReadAllText(path);
    }
}