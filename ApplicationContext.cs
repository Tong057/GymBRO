using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymBro
{
    partial class ApplicationContext : ObservableObject
    {
        [ObservableProperty]
        private string pep;
    }
}
