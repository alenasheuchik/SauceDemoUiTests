using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;

namespace SauceDemo.Tests.Data
{
    public class UsersCsvData
    {
        public static IEnumerable<TestCaseData> GetUsers()
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(baseDir, "Resources", "users.csv");

            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                var parts = line.Split(';');
                string username = parts[0];
                string password = parts[1];
                string result = parts[2];

                yield return new TestCaseData(username, password, result)
                    .SetName($"Login_{username}_{result}");
            }
        }
    }
}
