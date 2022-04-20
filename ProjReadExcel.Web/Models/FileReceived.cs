using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjReadExcel.Web.Data
{
    public class FileReceived
    {
        public int Id { get; set; }

        [DisplayName("Descrição")]
        public string Description { get; set; }

        [DisplayName("Nome do Arquivo")]
        public string FileDescription { get; set; }

        [NotMapped]
        [DisplayName("Arquivo")]
        public IFormFile File { get; set; }
    }
}
