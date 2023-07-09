using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Models
{
    public class CassettesInfo
    {
        [Key]
        public int Denomination { get; set; }
        public int CountOfBanknotes { get; set; }
    }
}
