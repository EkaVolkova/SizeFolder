using System;
using System.IO;

/// <summary>
/// Программа считает размер папки на диске (вместе со всеми вложенными папками и файлами).
/// На вход метод принимает URL директории, в ответ — размер в байтах.
/// </summary>
namespace SizeFolder
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var size = SizeFolder.GetSizeFolder(args[0]);
                Console.WriteLine(size);
            }
            catch (Exception ex)
            {
                //Выводим текст исключения в консоль
                Console.WriteLine("Ошибка расчета: " + ex.Message);
            }
            Console.ReadKey();
        }
    }
    static class SizeFolder
    {
        public static long GetSizeFolder(string path)
        {
            //Создали экземпляр класса для работы с директорией
            DirectoryInfo directoryInfo = new DirectoryInfo(path);

            //ПРоверили, что директория существует
            if (!directoryInfo.Exists)
                throw new ArgumentException("Папка не существует");

            long sum = 0;
            //Выгрузили информацию по всем файлам в директории
            var files = directoryInfo.GetFiles();
            //Просуммировали размеры файлов
            foreach (var file in files)
            {
                sum += file.Length;
            }
            //Выгрузили информацию по всем директориям в директории
            var directories = directoryInfo.GetDirectories();

            foreach (var directory in directories)
            {
                //для каждой директории запустили программу расчета размера
                sum += GetSizeFolder(directory.FullName);

            }
            return sum;
        }
    }
}
