using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;

namespace SauceDemo.Tests.Data
{
    public class ProductsCsvData
    {
        public static IEnumerable<TestCaseData> GetProducts()
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(baseDir, "Resources", "products.csv");

            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                var parts = line.Split(';');
                string productName = parts[0];
                string price = parts[1];

                yield return new TestCaseData(productName, price)
                    .SetName($"Buy_{productName.Replace(" ", "_")}");
            }
        }
    }
}
