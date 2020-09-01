using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DevExpress.Pdf.Native;
using DevExpress.XtraPrinting.BarCode;
using DevExpress.XtraReports.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace devexReports.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class WeatherForecastController : ControllerBase
	{
		private static readonly string[] Summaries = new[]
		{
			"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
		};

		private readonly ILogger<WeatherForecastController> _logger;

		public WeatherForecastController(ILogger<WeatherForecastController> logger)
		{
			_logger = logger;
		}

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			XtraReport report = new XtraReport1();
			Stream stream = new MemoryStream();
			await report.ExportToPdfAsync(stream);
			stream.Position = 0;
			return File(stream, "application/pdf", "devex-report.pdf");

			//Export();
			return null;
			//var rng = new Random();
			//return Enumerable.Range(1, 5).Select(index => new WeatherForecast
			//{
			//	Date = DateTime.Now.AddDays(index),
			//	TemperatureC = rng.Next(-20, 55),
			//	Summary = Summaries[rng.Next(Summaries.Length)]
			//})
			//.ToArray();
		}

		private void Export()
		{
			Debug.Write($"Start Time: {DateTime.Now.ToLongTimeString()}");
			Parallel.For(0, 20000, (i) =>
			{
				XtraReport report = new XtraReport1()
				{
					//Bands = {
					//	new DetailBand() {
					//		Name = "DetaiBand",
					//		Controls = {
					//			new XRLabel() {
					//				Text = "Simple Report"
					//			}
					//		}
					//	}
					//}
				};

				report.ExportToPdf($"E:\\Reports1\\{i}-{Guid.NewGuid()}.pdf");
			});
			Debug.Write($"Start Time: {DateTime.Now.ToLongTimeString()}");
			//for (int i = 0; i < 20000; i++)
			//{
			//}
		}
	}
}
