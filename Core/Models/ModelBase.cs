using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{

    public abstract class ModelBase
    {

        [Key]
        public int Id { get; set; }

        //[DefaultValue("GETUTCDATE()")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? AddTime { get; set; } = DateTime.UtcNow;

    }

}
