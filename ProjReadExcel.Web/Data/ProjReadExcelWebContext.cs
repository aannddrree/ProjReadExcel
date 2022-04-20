using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjReadExcel.Web.Models;
using ProjReadExcel.Web.Data;

namespace ProjReadExcel.Web.Data
{
    public class ProjReadExcelWebContext : DbContext
    {
        public ProjReadExcelWebContext (DbContextOptions<ProjReadExcelWebContext> options)
            : base(options)
        {
        }

        public DbSet<ProjReadExcel.Web.Models.DataColumn> DataColumn { get; set; }

        public DbSet<ProjReadExcel.Web.Data.FileReceived> FileReceived { get; set; }
    }
}
