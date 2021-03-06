using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
	public class MovieList
    {

		[Key][Column(Order = 0)]
		public string movieName { get; set; }
		public int seats  { get; set; }
		[Key][Column(Order = 1)]
		public string streamDate { get; set; }
		[Key]
		[Column(Order = 2)]
		public string streamTime { get; set; }
		public string imageLink { get; set; }
		[Key]
		[Column(Order = 3)]
		public string hall { get; set; }
		public int rating { get; set; }
		public float price { get; set; }
		public string ageLim { get; set; }
		public string category { get; set; }
		public float sale { get; set; }
		public string takenSeats { get; set; }
		
		
	}
}