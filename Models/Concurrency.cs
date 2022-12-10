using System.Text.Json.Serialization;

namespace ConcurrencyAPI.Models
{
	public class Concurrency
	{
		private TimeSpan concurrencyTime;
		private double concurrencySum;

		[JsonPropertyName("concurrencytime")]
		public TimeSpan ConcurrencyTime { get => concurrencyTime; set => concurrencyTime = value; }
		[JsonPropertyName("concurrencysum")]
		public double ConcurrencySum { get => concurrencySum; set => concurrencySum = value; }
		public Concurrency(TimeSpan concurrencyTime, double concurrencySum)
		{
			this.ConcurrencyTime = concurrencyTime;
			this.ConcurrencySum = concurrencySum;
		}
	}
}
