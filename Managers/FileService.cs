using System.IO;
using System.Text;

namespace lab1.Managers
{
    public class FileService
    {
        public string ReadFile(string filePath)
        {
            return File.ReadAllText(filePath, Encoding.UTF8);
        }

        public void WriteFile(string filePath, string content)
        {
            File.WriteAllText(filePath, content, Encoding.UTF8);
        }

        public string GetFileFilter()
        {
            return "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
        }
    }
}