using System;
using System.IO;

namespace ShoppingBasket
{
	public static class Logger
	{
		public static void LogInfo(string logEntry)
		{
			string path = @"log.txt";
			if(File.Exists(path))
			{
				using(StreamWriter sw = File.AppendText(path))
				{
					sw.WriteLine(Environment.NewLine);
					sw.WriteLine(DateTime.Now + Environment.NewLine);
					sw.Write(logEntry);
				}
			}
			else
			{
				using(StreamWriter sw = File.CreateText(path))
				{
					sw.WriteLine(Environment.NewLine);
					sw.WriteLine(DateTime.Now + Environment.NewLine);
					sw.Write(logEntry);
				}
			}
		}
	}
}