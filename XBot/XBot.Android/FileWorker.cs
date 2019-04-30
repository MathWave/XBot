using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;

[assembly: Dependency(typeof(XBot.Droid.FileWorker))]
namespace XBot.Droid
{
    public class FileWorker : IFileWorker
    {

        public Task DeleteAsync(string filename)
        {
            File.Delete(GetFilePath(filename));
            return Task.FromResult(true);
        }

        public Task<bool> ExistsAsync(string filename)
        {
            string filepath = GetFilePath(filename);
            bool exists = File.Exists(filepath);
            return Task<bool>.FromResult(exists);
        }

        public Task<IEnumerable<string>> GetFilesAsync()
        {
            IEnumerable<string> filenames = from filepath in Directory.EnumerateFiles(GetDocsPath())
                                            select Path.GetFileName(filepath);
            return Task<IEnumerable<string>>.FromResult(filenames);
        }

        public async Task<string> LoadTextAsync(string filename)
        {
            string filepath = GetFilePath(filename);
            using (StreamReader reader = File.OpenText(filepath))
            {
                return await reader.ReadToEndAsync();
            }
        }

        public Task CreateFileAsync(string filename)
        {
            File.Create(GetFilePath(filename));
            return Task.FromResult(true);

        }

        public async Task SaveTextAsync(string filename, string text)
        {
            string filepath = GetFilePath(filename);
            byte[] bytetext = System.Text.Encoding.UTF8.GetBytes(text);
            using (FileStream fs = new FileStream(filepath, FileMode.OpenOrCreate))
            {
                await fs.WriteAsync(bytetext, 0, bytetext.Length);
            }
        }
        // вспомогательный метод для построения пути к файлу
        string GetFilePath(string filename)
        {
            string filepath = Path.Combine(GetDocsPath(), filename);
            return filepath.Substring(0, filepath.Length - 1).Replace(" ", "_") + ".html";
        }
        // получаем путь к папке MyDocuments
        string GetDocsPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }
    }
}
