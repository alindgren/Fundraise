using System;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Fundraise.MvcExample.Tests.Config
{
    public interface IProjectLocation
    {
        string FullPath { get; }
        string ProjectName { get; }
    }

    public class ProjectLocation : IProjectLocation
    {
        public string FullPath { get; private set; }

        public string ProjectName { get; private set; }

        private ProjectLocation(string fullPath)
        {
            var folder = new DirectoryInfo(fullPath);
            if (!folder.Exists)
            {
                throw new DirectoryNotFoundException();
            }
            FullPath = fullPath;
            ProjectName = GetProjectNameFromFolder(fullPath);
        }

        public static ProjectLocation FromPath(string webProjectFullPath)
        {
            return new ProjectLocation(webProjectFullPath);
        }

        public static ProjectLocation FromFolder(string webProjectFolderName)
        {
            string solutionFolder = GetSolutionFolderPath();
            string projectPath = FindSubFolderPath(solutionFolder, webProjectFolderName);
            return new ProjectLocation(projectPath);
        }

        private static string GetSolutionFolderPath()
        {
            var directory = new DirectoryInfo(Environment.CurrentDirectory);

            while (directory.GetFiles("*.sln").Length == 0)
            {
                directory = directory.Parent;
            }
            return directory.FullName;
        }

        private static string FindSubFolderPath(string rootFolderPath, string folderName)
        {
            var directory = new DirectoryInfo(rootFolderPath);

            directory = (directory.GetDirectories("*", SearchOption.AllDirectories)
                .Where(folder => folder.Name.ToLower() == folderName.ToLower()))
                .FirstOrDefault();

            if (directory == null)
            {
                throw new DirectoryNotFoundException();
            }

            return directory.FullName;
        }

        private static string GetProjectNameFromFolder(string folderPath)
        {
            var projectDirectory = new DirectoryInfo(folderPath);
            var projectName = projectDirectory.EnumerateFiles("*.csproj").SingleOrDefault();
            if (projectName == null)
                throw new FileNotFoundException("No '.csproj' file found in specified web application folder.");
            return projectName.FullName;
        }

        private static string GetTestProjectDirectory()
        {
            string codeBase = typeof(IProjectLocation).Assembly.CodeBase;
            var uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path);
        }
    }
}
