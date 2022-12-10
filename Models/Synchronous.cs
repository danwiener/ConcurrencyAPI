using System.Text.Json.Serialization;

namespace ConcurrencyAPI.Models
{
	public class Synchronous
	{
		private TimeSpan synchTime;
		private double synchSum;

		[JsonPropertyName("synchtime")]
		public TimeSpan SynchTime { get => synchTime; set => synchTime = value; }
		[JsonPropertyName("synchsum")]
		public double SynchSum { get => synchSum; set => synchSum = value; }
		public Synchronous(TimeSpan synchTime, double synchSum)
		{
			this.synchTime = synchTime;
			this.synchSum = synchSum;
		}
			
	}
}
