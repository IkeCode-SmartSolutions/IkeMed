using IkeMed.Model.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IkeMed.Model
{
    public class Address : BaseModel
    {
        [Required(ErrorMessage = "O campo 'Rua' é obrigatório.")]
        [MaxLength(250, ErrorMessage = "O campo 'Rua' deve ter no máximo 250 caracteres.")]
        public string Street { get; set; }

        [Required(ErrorMessage = "O campo 'Número' é obrigatório.")]
        [MaxLength(10, ErrorMessage = "O campo 'Número' deve ter no máximo 10 caracteres.")]
        public string Number { get; set; }

        [MaxLength(50, ErrorMessage = "O campo 'Complemento' deve ter no máximo 50 caracteres.")]
        public string Complement { get; set; }

        [Required(ErrorMessage = "O campo 'Bairro' é obrigatório.")]
        [MaxLength(100, ErrorMessage = "O campo 'Bairro' deve ter no máximo 100 caracteres.")]
        public string Neighborhood { get; set; }

        [Required(ErrorMessage = "O campo 'CEP' é obrigatório.")]
        [MaxLength(20, ErrorMessage = "O campo 'CEP' deve ter no máximo 20 caracteres.")]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "O campo 'Cidade' é obrigatório.")]
        [MaxLength(150, ErrorMessage = "O campo 'Cidade' deve ter no máximo 150 caracteres.")]
        public string City { get; set; }

        [Required(ErrorMessage = "O campo 'Estado' é obrigatório.")]
        [MaxLength(3, ErrorMessage = "O campo 'Estado' deve ter no máximo 3 caracteres.")]
        public string State { get; set; }

        [Required(ErrorMessage = "O campo 'Tipo de Endereço' é obrigatório.")]
        public AddressTypeEnum AddressType { get; set; }

        [JsonIgnore]
        [Display(Name = "Pessoa")]
        public virtual Person Person { get; set; }

        public Address()
            : base()
        {
        }
    }
}
