
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Build.Evaluation;


public class ProjectGenerator
{
    public static string _folderPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\project Layer";
    public static string PathDAL = "";
    public static string PathBLL = "";
    private static string projectName = "";

    private static bool CreateFullBLL(string TextClass, string DatabaseName, string TableName)
    {

        string projectName = DatabaseName + "Business" + "Layer";

        PathBLL = CreateDll(_folderPath, projectName).ToString();

        string filePath = Path.Combine(PathBLL, $"cls{TableName}.cs");

        Task.Run(() => SaveClassToFile(filePath, TextClass));


        return true;

    }

    //private static async Task AddReferenceForProject()
    //{
    //    var project = new Project(PathBLL);

    //    project.AddItem(projectName, PathDAL);
    //    project.Save();


    //}

    private static async Task<bool> IsExisst(string Path, string SettingClass)
    {
        if (!File.Exists(Path))
        {
           await Task.Run(()=> SaveClassToFile(Path, SettingClass));
            return true;
        }
        return false;
    }

    private static async Task<bool> CreateFullDAL(string TextClass, string DatabaseName, string TableName, string SettingClass,string CURD)
    {
         projectName = DatabaseName + "DataAccess" + "Layer";

        PathDAL = CreateDll(_folderPath, projectName).ToString();



        string pathClsSetting = Path.Combine(PathDAL, $"clsSetting.cs");
        await IsExisst(pathClsSetting, SettingClass);

       
            string pathCRUD = Path.Combine(PathDAL, $"clsPrimaryFunctions.cs");
            await IsExisst(pathCRUD, CURD);

        



        string filePath = Path.Combine(PathDAL, $"cls{TableName}Data.cs");

        await Task.Run(() => SaveClassToFile(filePath, TextClass));
        return true;

    }

    public static async Task<bool> CreateFullProject(string DAL, string BLL, string DatabaseName, string TableName, string SettingClass,string CRUD)
    {
        Task bllTask = Task.Run(() => CreateFullBLL(BLL, DatabaseName, TableName));
        await bllTask;

        Task dll = Task.Run(() => CreateFullDAL(DAL, DatabaseName, TableName, SettingClass,CRUD));
        await dll;

     //  await AddReferenceForProject();

        return bllTask.IsCompleted && dll.IsCompleted
;
    }

    private static string CreateDll(string folderPath, string projectName)
    {
        string path = "";

        try
        {
            path = Path.Combine(folderPath, projectName);


            if (!Directory.Exists(path))
            {



                path = Path.Combine(folderPath, projectName);

                Directory.CreateDirectory(folderPath);

                string createCommand = $"dotnet new classlib -o \"{path}\"";

                RunCommand(createCommand);

                string buildCommand = $"dotnet build \"{path}\"";

                RunCommand(buildCommand);
            }
            else
                path = Path.Combine(folderPath, projectName);

        }
        catch (Exception ex)
        {
            //ClsGloble.EntireInfoToEventLoge(ex.Message);
        }
        finally
        {
            folderPath = null;
            projectName = null;
        }
        return path;



    }

    private static void RunCommand(string command)
    {
        try
        {

            ProcessStartInfo startInfo = new ProcessStartInfo("cmd", $"/c {command}")
            {
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };


            using (Process process = new Process { StartInfo = startInfo })
            {
                try
                {
                    process.Start();
                    process.WaitForExit();
                }
                finally
                {
                    process.Close();

                }
            }

        }
        catch (Exception ex)
        {
            //MessageBox.Show(ex.Message);
        }

    }

    private static async Task SaveClassToFile(string FilePath, string TextClass)
    {
        try
        {
            // Code from class

           

            using (StreamWriter writer = new StreamWriter(FilePath))
            {

                await writer.WriteAsync(TextClass).ConfigureAwait(false);
            }

        }
        catch (Exception ex)
        {
            //ClsGloble.EntireInfoToEventLoge(ex.Message);
        }
        finally
        {

        }
    }


}
