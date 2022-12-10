using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ConcurrencyAPI.Models;

// Created by Henrik Wiener 12/09/22

namespace ConcurrencyAPI.Controllers
{
	[ApiController]
	[Route("api")]
	public class CalculatorController : Controller
	{
		// Data Fields


		private double _sum; // sum of values

		private object myLock = new object(); // for locking _sum

		// Time property
		public TimeSpan ConcurrentTimeElapsed { get; set; }

		private IConfiguration configuration;

		public CalculatorController(IConfiguration configuration)
		{

		}

		[HttpGet("synchronous")]
		public IActionResult Synchronous()
		{
			double synchronousHeader = Convert.ToDouble(Request.Headers["synchronous"]);

			Synchronous synchronousObject = SumToInput(synchronousHeader);
			

			return Ok(synchronousObject);
		} // End method

		[HttpGet("concurrent")]
		public IActionResult Concurrent()
		{
			double concurrencyHeader = Convert.ToDouble(Request.Headers["concurrency"]);


			Concurrency concurrencyObject = AddToNumberConcurrently(concurrencyHeader);

			return Ok(concurrencyObject);
		} // End method

		// This method will concurrently run 100 threads each summing 100 million numbers up to 10 billion
		public Concurrency AddToNumberConcurrently(double totalSum)
		{
			Thread[] taskList = new Thread[10];

			double start = 1;
			double end = totalSum / 10;

			Stopwatch sw = Stopwatch.StartNew();

			// Sum in increments of 1/10 total number to sum to, 10 times concurrently up to sumTo number
			for (int i = 0; i < 10; i++)
			{
				int index = i;
				taskList[index] = new Thread(() => AddToOneTenth(start, end));

				// Ensure that thread starts before continuation of program
				Task task = Task.Factory.StartNew(() => taskList[index].Start());
				task.Wait();
				start += (totalSum / 10); // add 1/10 total
				end += (totalSum / 10); // add 1/10 total
			}
			// Wait for collection of tasks to finish running before stopping the stopwatch. 
			foreach (Thread t in taskList)
			{
				t.Join();
			}
			sw.Stop();
			TimeSpan TotalElapsed = sw.Elapsed;

			Concurrency concurrency= new Concurrency(TotalElapsed, _sum);
			return (concurrency);
		} // end method


		// This method will add to 1/10 the totalSum request number in increments
		public void AddToOneTenth(double start, double end)
		{
			double sum = 0;

			for (double i = start; i <= end; i++)
			{
				sum += i;
			}

			lock (myLock)
			{
				_sum += sum;
			}

		} // End method


		// This method syncrhonously sums to input number
		public Synchronous SumToInput(double sumTo)
		{
			double sum = 0;
			Stopwatch sw = Stopwatch.StartNew();
			//double sum = (TenBillion * (TenBillion + 1)) / 2;     // Using Gauss Summation
			for (double i = 1; i <= sumTo; i++)
			{
				sum += i;
			}
			sw.Stop();

			TimeSpan TotalElapsed = sw.Elapsed;
			
			Synchronous synchronous = new Synchronous(TotalElapsed, sum);
			return synchronous;
		} // end method
	}
}

