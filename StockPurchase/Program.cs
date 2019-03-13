using System;
using System.Linq;
using System.Collections.Generic;

namespace StockPurchase
{
    class Program
    {
        /*
         Create a total ownership report that computes the total value of each stock that you have purchased.
         This is the basic relational database join algorithm between two tables.

        Define a new Dictionary to hold the aggregated purchase information. 
            The key should be a string that is the full company name. 
            The value will be the valuation of each stock (price*amount) { "General Electric": 35900, "AAPL": 8445, ... }
        Iterate over the purchases and update the valuation for each stock
        foreach ((string ticker, int shares, double price) purchase in purchases)
        { 
            // Does the company name key already exist in the report dictionary?
            // If it does, update the total valuation
            // If not, add the new key and set its value
        }
        Helpful Links: ContainsKey, Add
        
        */
        static void Main(string[] args)
        {
            Dictionary<string, string> stocks = new Dictionary<string, string>();
            stocks.Add("GM", "General Motors");
            stocks.Add("CAT", "Caterpillar");
            stocks.Add("MSFT", "Microsoft");
            stocks.Add("TWTR", "Twitter");
            stocks.Add("ORCL", "Oracle");
            // Add a few more of your favorite stocks


            List<(string ticker, int shares, double price)> purchases = new List<(string, int, double)>();
            purchases.Add((ticker: "GM", shares: 150, price: 23.21));
            purchases.Add((ticker: "GM", shares: 32, price: 17.87));
            purchases.Add((ticker: "GM", shares: 80, price: 19.02));
            purchases.Add((ticker: "MSFT", shares: 120, price: 113.62));
            purchases.Add((ticker: "MSFT", shares: 45, price: 110.32));
            purchases.Add((ticker: "MSFT", shares: 27, price: 108.45));
            purchases.Add((ticker: "TWTR", shares: 34, price: 45.34));
            purchases.Add((ticker: "TWTR", shares: 27, price: 42.87));
            purchases.Add((ticker: "TWTR", shares: 68, price: 47.47));
            purchases.Add((ticker: "ORCL", shares: 27, price: 65.98));
            purchases.Add((ticker: "CAT", shares: 178, price: 25.98));
            purchases.Add((ticker: "CAT", shares: 157, price: 27.76));
            purchases.Add((ticker: "CAT", shares: 35, price: 32.76));
            purchases.Add((ticker: "ORCL", shares: 13, price: 35.65));
            purchases.Add((ticker: "ORCL", shares: 45, price: 32.76));
            // Add more for each stock you added to the stocks dictionary

            Dictionary<string, double> aggregateData = new Dictionary<string, double>();

            var missingLinq = stocks
                .Join(purchases,
                    x => x.Key,
                    y => y.ticker,
                    (x, y) => new { Ticker = x.Value, Quantity = y.shares, Price = y.price });
            

            foreach (var stockEntry in missingLinq)
            {
                // Does the company name key already exist in the report dictionary?
                if (aggregateData.ContainsKey(stockEntry.Ticker))
                // If it does, update the total valuation
                {
                    aggregateData[stockEntry.Ticker] = aggregateData[stockEntry.Ticker] + (stockEntry.Quantity * stockEntry.Price);
                }
                else
                // If not, add the new key and set its value
                {
                    aggregateData.Add(stockEntry.Ticker, (stockEntry.Quantity * stockEntry.Price));
                }
            }

            foreach (var stock in aggregateData)
            {
                Console.WriteLine("Stock: {0,-15}\tTotal Value: {1:C2}\n", stock.Key, stock.Value);
            }
            Console.ReadKey();
        }
    }
}
