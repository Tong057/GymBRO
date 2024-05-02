using System;

namespace GymBro.Models.Data.EntityFramework.DbProviders
{
	public class Provider : IProvider
	{
		private ApplicationContext _context;

		public Provider(ApplicationContext applicationContext)
		{
			_context = applicationContext;
		}
	}
}

