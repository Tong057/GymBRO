using System;
using GymBro.Models.Data.EntityFramework.DbProviders;

namespace GymBro.Models.Data.EntityFramework.Repositories
{
	public class Repository
	{
        private IProvider _provider;

        public Repository(IProvider provider)
		{
			_provider = provider;
		}
	}
}

