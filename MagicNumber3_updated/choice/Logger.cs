using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace MagicNumber3
{
    internal class Logger
    {
        public void Savelog(Loglevels level, string message, [CallerLineNumber] int lineNumber = 0)
        {
            //magic_number_logというファイルパスに短い現在の日時とログの種類と行数とメッセージをログに残す
            string filepath = @"magic_number_log";
            string logEntry = $"{DateTime.Now:g},{level},{lineNumber + "行目"},{message}";

            using (StreamWriter writer = File.AppendText(filepath))
            {
                writer.WriteLine(logEntry);
            }
        }
    }

    public enum Loglevels
    {
        ERROR,
        INFO
    }
}