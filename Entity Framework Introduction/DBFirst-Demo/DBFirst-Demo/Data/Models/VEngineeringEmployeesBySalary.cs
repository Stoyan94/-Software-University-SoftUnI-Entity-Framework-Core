using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DBFirst_Demo.Data.Models
{
    [Keyless]
    public partial class VEngineeringEmployeesBySalary
    {
        [StringLength(50)]
        [Unicode(false)]
        public string Име { get; set; } = null!;
        [StringLength(50)]
        [Unicode(false)]
        public string Фамилия { get; set; } = null!;
        [Column(TypeName = "money")]
        public decimal Заплата { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Отдел { get; set; } = null!;
    }
}
