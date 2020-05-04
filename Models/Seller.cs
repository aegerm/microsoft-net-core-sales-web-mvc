using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace sales_web_mvc.Models
{
    public class Seller
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} Obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Tamanho do {0} deve ser entre {2} e {1}")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} Obrigatório")]
        [EmailAddress(ErrorMessage = "Entre com um e-mail válido!")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} Obrigatório")]
        [Range(100.0, 35000.00, ErrorMessage = "{0} deve estar entre {1} e {2}")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double BaseSalary { get; set; }

        [Required(ErrorMessage = "{0} Obrigatório")]
        [Display(Name = "Data de Nascimento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }
        public Department Department { get; set; }
        public int DepartmentId { get; set; }
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Department = department;
        }

        public Seller()
        {
        }

        public void AddSales(SalesRecord sales)
        {
            Sales.Add(sales);
        }

        public void RemoveSales(SalesRecord sales)
        {
            Sales.Remove(sales);
        }

        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sales.Where(sr => sr.Date >= initial && sr.Date <= final).Sum(sr => sr.Amount);
        }
    }
}