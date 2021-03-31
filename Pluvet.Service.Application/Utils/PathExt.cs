using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Pluvet.Service.Application.Utils
{
    public class PathExt
    {
        /// <summary>
        /// 获取程序所在目录的完整字符串表示, 返回值不以路径分隔符结尾.
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public static string GetAppDir(string dir = "", bool unixStyle = true)
        {
            var baseDir = System.AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\');
            if (unixStyle)
            {
                baseDir = baseDir.Replace("\\", "/");
            }
            if (dir == string.Empty)
            {
                return baseDir;
            }
            return baseDir + "/" + dir.TrimEnd('/');
        }


        public static IList<string> GetFilesToDepth(string path, int depth, string[] exts)
        {
            var files = Directory.EnumerateFiles(path).ToList();
            if (depth == 0)
            {
                return files;
            }
            var folders = Directory.EnumerateDirectories(path);
            foreach (var folder in folders)
            {
                if (folder.StartsWith("_"))
                {
                    continue;
                }
                files.AddRange(GetFilesToDepth(folder, depth - 1, exts));
            }
            if (exts == null)
            {
                return files;
            }
            var valid = new List<string>();
            Array.ForEach(exts, (ext) =>
            {
                valid.AddRange(files.FindAll(x => x.EndsWith(ext)));
            });

            return valid;
        }

        public static void CloneDirectory(string root, string dest, bool overwrite = false)
        {
            foreach (var directory in Directory.GetDirectories(root))
            {
                string dirName = Path.GetFileName(directory);
                if (!Directory.Exists(Path.Combine(dest, dirName)))
                {
                    Directory.CreateDirectory(Path.Combine(dest, dirName));
                }
                CloneDirectory(directory, Path.Combine(dest, dirName));
            }

            foreach (var file in Directory.GetFiles(root))
            {
                var targetName = Path.Combine(dest, Path.GetFileName(file));
                if (File.Exists(targetName))
                {
                    continue;
                }
                File.Copy(file, targetName);
            }
        }

    }
}
