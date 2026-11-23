using ASSLINQ;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment_11_Session13_Depi
{
    internal class LinqAssignmentSolver
    {
        public void RunElementOperators(List<Product> productList, int[] arr)
        {
            Console.WriteLine("================ 1. Element Operators ================");

            var firstOutOfStock = productList.FirstOrDefault(p => p.UnitsInStock == 0);
            Console.WriteLine($"1. First Out of Stock: {firstOutOfStock?.ProductName ?? "None"}");

            var firstExpensiveProduct = productList.FirstOrDefault(p => p.UnitPrice > 1000);
            Console.WriteLine($"2. First Product Price > 1000: {firstExpensiveProduct?.ProductName ?? "None"}");

            var secondGreaterThanFive = arr.Where(n => n > 5).ElementAtOrDefault(1);
            Console.WriteLine($"3. Second Number > 5: {secondGreaterThanFive}");
        }

        public void RunAggregateOperators(List<Product> productList, List<Customer> customerList, int[] arr, string[] words)
        {
            Console.WriteLine("\n================ 2. Aggregate Operators ================");

            int oddCount = arr.Count(n => n % 2 != 0);
            Console.WriteLine($"1. Odd Numbers Count: {oddCount}");

            var customerOrdersCount = customerList
                .Select(c => new { c.CustomerName, OrdersCount = c.Orders.Length });
            Console.WriteLine("2. Customer Orders Count (Sample 5):");
            foreach (var item in customerOrdersCount.Take(5))
                Console.WriteLine($"   {item.CustomerName}: {item.OrdersCount}");

            var categoryProductsCount = productList
                .GroupBy(p => p.Category)
                .Select(g => new { Category = g.Key, ProductsCount = g.Count() });
            Console.WriteLine("3. Category Products Count:");
            foreach (var item in categoryProductsCount)
                Console.WriteLine($"   {item.Category}: {item.ProductsCount}");

            int totalSum = arr.Sum();
            Console.WriteLine($"4. Array Sum: {totalSum}");

            int totalChars = words.Sum(w => w.Length);
            Console.WriteLine($"5. Total Chars in Dictionary: {totalChars}");

            int shortestWordLength = words.Min(w => w.Length);
            Console.WriteLine($"6. Shortest Word Length: {shortestWordLength}");

            int longestWordLength = words.Max(w => w.Length);
            Console.WriteLine($"7. Longest Word Length: {longestWordLength}");

            double averageWordLength = words.Average(w => w.Length);
            Console.WriteLine($"8. Average Word Length: {averageWordLength:F2}");

            var totalUnitsByCategory = productList
                .GroupBy(p => p.Category)
                .Select(g => new { Category = g.Key, TotalUnitsInStock = g.Sum(p => p.UnitsInStock) });
            Console.WriteLine("9. Total Units in Stock by Category:");
            foreach (var item in totalUnitsByCategory)
                Console.WriteLine($"   {item.Category}: {item.TotalUnitsInStock}");

            var minPriceByCategory = productList
                .GroupBy(p => p.Category)
                .Select(g => new { Category = g.Key, CheapestPrice = g.Min(p => p.UnitPrice) });
            Console.WriteLine("10. Cheapest Price by Category:");
            foreach (var item in minPriceByCategory)
                Console.WriteLine($"   {item.Category}: {item.CheapestPrice}");

            var cheapestProductsByCategory =
                from p in productList
                group p by p.Category into g
                let minPrice = g.Min(p => p.UnitPrice)
                select new
                {
                    Category = g.Key,
                    CheapestProducts = g.Where(p => p.UnitPrice == minPrice).ToList()
                };
            Console.WriteLine("11. Cheapest Products by Category:");
            foreach (var grp in cheapestProductsByCategory)
                Console.WriteLine($"   {grp.Category}: {string.Join(", ", grp.CheapestProducts.Select(p => p.ProductName))}");

            var maxPriceByCategory = productList
                .GroupBy(p => p.Category)
                .Select(g => new { Category = g.Key, MostExpensivePrice = g.Max(p => p.UnitPrice) });
            Console.WriteLine("12. Most Expensive Price by Category:");
            foreach (var item in maxPriceByCategory)
                Console.WriteLine($"   {item.Category}: {item.MostExpensivePrice}");

            var mostExpensiveProductsByCategory =
                from p in productList
                group p by p.Category into g
                let maxPrice = g.Max(p => p.UnitPrice)
                select new
                {
                    Category = g.Key,
                    MostExpensiveProducts = g.Where(p => p.UnitPrice == maxPrice).ToList()
                };
            Console.WriteLine("13. Most Expensive Products by Category:");
            foreach (var grp in mostExpensiveProductsByCategory)
                Console.WriteLine($"   {grp.Category}: {string.Join(", ", grp.MostExpensiveProducts.Select(p => p.ProductName))}");

            var avgPriceByCategory = productList
                .GroupBy(p => p.Category)
                .Select(g => new { Category = g.Key, AveragePrice = g.Average(p => p.UnitPrice) });
            Console.WriteLine("14. Average Price by Category:");
            foreach (var item in avgPriceByCategory)
                Console.WriteLine($"   {item.Category}: {item.AveragePrice:F2}");
        }

        public void RunSetOperators(List<Product> productList, List<Customer> customerList)
        {
            Console.WriteLine("\n================ 3. Set Operators ================");

            var uniqueCategories = productList.Select(p => p.Category).Distinct();
            Console.WriteLine("1. Unique Categories: " + string.Join(", ", uniqueCategories));

            var uniqueFirstLetters = productList.Select(p => p.ProductName[0])
                .Union(customerList.Select(c => c.CustomerName[0]));
            Console.WriteLine("2. Unique First Letters (Products U Customers): " + string.Join(", ", uniqueFirstLetters));

            var commonFirstLetters = productList.Select(p => p.ProductName[0])
                .Intersect(customerList.Select(c => c.CustomerName[0]));
            Console.WriteLine("3. Common First Letters (Products ∩ Customers): " + string.Join(", ", commonFirstLetters));

            var productOnlyFirstLetters = productList.Select(p => p.ProductName[0])
                .Except(customerList.Select(c => c.CustomerName[0]));
            Console.WriteLine("4. Product-Only First Letters (Products - Customers): " + string.Join(", ", productOnlyFirstLetters));

            var lastThreeCharsAll = productList
                .Select(p => p.ProductName.Length >= 3 ? p.ProductName[^3..] : p.ProductName)
                .Concat(customerList
                    .Select(c => c.CustomerName.Length >= 3 ? c.CustomerName[^3..] : c.CustomerName));
            Console.WriteLine("5. Last 3 Chars Count (Products + Customers Concat): " + lastThreeCharsAll.Count());
        }

        public void RunPartitioningOperators(List<Customer> customerList, int[] numbers)
        {
            Console.WriteLine("\n================ 4. Partitioning Operators ================");

            var first3WashingtonOrders = customerList
                .Where(c => c.Region == "WA")
                .SelectMany(c => c.Orders)
                .Take(3);
            Console.WriteLine("1. First 3 WA Orders:");
            foreach (var o in first3WashingtonOrders)
                Console.WriteLine($"   OrderID: {o.OrderID}, Total: {o.Total}");

            var skip2WashingtonOrders = customerList
                .Where(c => c.Region == "WA")
                .SelectMany(c => c.Orders)
                .Skip(2);
            Console.WriteLine("2. Skip First 2 WA Orders Count: " + skip2WashingtonOrders.Count());

            var takeWhileLessThanIndex = numbers
                .TakeWhile((num, index) => num >= index);
            Console.WriteLine("3. TakeWhile (num >= index): " + string.Join(", ", takeWhileLessThanIndex));

            var skipUntilDivisibleBy3 = numbers
                .SkipWhile(num => num % 3 != 0);
            Console.WriteLine("4. SkipWhile until divisible by 3: " + string.Join(", ", skipUntilDivisibleBy3));

            var skipUntilLessThanIndex = numbers
                .SkipWhile((num, index) => num >= index);
            Console.WriteLine("5. SkipWhile until (num < index): " + string.Join(", ", skipUntilLessThanIndex));
        }

        public void RunQuantifiers(List<Product> productList, string[] words)
        {
            Console.WriteLine("\n================ 5. Quantifiers ================");

            bool hasEiSubstring = words.Any(w => w.Contains("ei"));
            Console.WriteLine($"1. Any Word Contains 'ei': {hasEiSubstring}");

            var categoriesWithOutOfStock = productList
                .GroupBy(p => p.Category)
                .Where(g => g.Any(p => p.UnitsInStock == 0));
            Console.WriteLine("2. Categories With At Least One Out-Of-Stock Product: " +
                              string.Join(", ", categoriesWithOutOfStock.Select(g => g.Key)));

            var categoriesAllInStock = productList
                .GroupBy(p => p.Category)
                .Where(g => g.All(p => p.UnitsInStock > 0));
            Console.WriteLine("3. Categories With All Products In Stock: " +
                              string.Join(", ", categoriesAllInStock.Select(g => g.Key)));
        }

        public void RunGroupingOperators(string[] words)
        {
            Console.WriteLine("\n================ 6. Grouping Operators ================");

            List<int> groupNumbers = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
            var groupedByRemainder5 = groupNumbers.GroupBy(n => n % 5);
            Console.WriteLine("1. Group Numbers by Remainder % 5:");
            foreach (var g in groupedByRemainder5)
                Console.WriteLine($"   Remainder {g.Key}: {string.Join(", ", g)}");

            var groupedWordsByFirstLetter = words
                .Where(w => !string.IsNullOrWhiteSpace(w))
                .GroupBy(w => char.ToLowerInvariant(w[0]));
            Console.WriteLine("2. Words Grouped by First Letter (First 5 Letters):");
            foreach (var g in groupedWordsByFirstLetter.Take(5))
                Console.WriteLine($"   Letter '{g.Key}': {g.Count()} words");

            string[] anagramArr = { "from", "salt", "earn", " last", "near", "form" };
            var groupedAnagrams = anagramArr.GroupBy(w => w.Trim(), new AnagramEqualityComparer());
            Console.WriteLine("3. Grouped Anagrams:");
            foreach (var g in groupedAnagrams)
                Console.WriteLine($"   Group: {string.Join(", ", g.Select(x => x.Trim()))}");
        }
    }
}
