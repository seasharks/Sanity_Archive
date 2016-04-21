using System.Collections.Generic;
using System.IO;

namespace Sanity_Archive
{
    public class FileSearch
    {
        /// <summary>
        /// This method searches files by filename in a given folder.
        /// The search is recursive by default and incasesensitive.
        /// </summary>
        /// <param name="searchTerm">simple string keyword</param>
        /// <param name="path">full path of the root directory as string</param>
        /// <returns>List of found files' FileInfo</returns>
        public static List<FileInfo> Search(string searchTerm, string path)
        {
            DirectoryInfo currentDirectory = new DirectoryInfo(path);
            FileInfo[] filesInDir = currentDirectory.GetFiles();
            List<FileInfo> result = new List<FileInfo>();

            if (searchTerm != "")
            {
                foreach (FileInfo curFile in filesInDir)
                {
                    if (curFile.Name.ToUpper().IndexOf(searchTerm.ToUpper()) != -1)
                    {
                        result.Add(curFile);
                    }
                }
            }
            DirectoryInfo [] subDirectories = currentDirectory.GetDirectories();
            foreach (DirectoryInfo subDirInfo in subDirectories)
            {
                result.AddRange(Search(searchTerm, subDirInfo.FullName));
            }

            return result;
        }
    }
}
