using ASSLINQ;

namespace Assignment_11_Session13_Depi
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var productList = ListGenerators.ProductList;
            var customerList = ListGenerators.CustomerList;
            int[] arr = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            string[] words = File.ReadAllLines("dictionary_english.txt");

            var solver = new LinqAssignmentSolver();

            solver.RunElementOperators(productList, arr);
            solver.RunAggregateOperators(productList, customerList, arr, words);
            solver.RunSetOperators(productList, customerList);
            solver.RunPartitioningOperators(customerList, numbers);
            solver.RunQuantifiers(productList, words);
            solver.RunGroupingOperators(words);
        }
    }
}
