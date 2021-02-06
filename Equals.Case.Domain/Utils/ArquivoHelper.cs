using System;
using System.IO;

namespace Equals.Case.Domain.Utils
{
    public static class ArquivoHelper
    {

        public static string ConvertBytesToFile(string baseDirectory, byte[] bytesFile)
        {
            string fileName = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss");
            try
            {
                File.WriteAllBytes($"{baseDirectory}/files/arquivo_{fileName}.txt", bytesFile);
                File.WriteAllBytes($"{baseDirectory}/filesBackup/arquivo_{fileName}.txt", bytesFile);
                return $"{fileName}.txt";

            }
            catch (System.Exception ex)
            {
                string message = ex.Message;
                throw new Exception("Erro ao gravar arquivo na pasta!");
            }
        }

        public static string ReadFileContent(string baseDirectory, string fileName)
        {
            try
            {
                string fileContent = File.ReadAllText($"{baseDirectory}/files/arquivo_{fileName}");
                return fileContent;
            }
            catch (System.Exception ex)
            {
                string message = ex.Message;
                throw new Exception("Erro ao ler dados de arquivo na pasta!");
            }
        }
    }
}