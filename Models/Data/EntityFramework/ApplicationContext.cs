using System;
using Microsoft.EntityFrameworkCore;

namespace GymBro.Models.Data.EntityFramework
{
	public class ApplicationContext : DbContext
	{
		public ApplicationContext(DbContextOptions options) : base(options)
		{

		}

		//DB Sets below
	}
}

