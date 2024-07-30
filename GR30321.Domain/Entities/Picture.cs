using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GR30321.Domain.Entities
{
    public class Picture
    {
        public int Id { get; set; }
        public string Name { get; set; } //наименование картины
        public string Description { get; set; } //описание картины
        public string? Image { get; set; } //фото

        public string Avtor { get; set; } //автор картины

        public int CreationDate { get; set; } //год создание
        public double Price { get; set; } //цена



        //навигационные поля
        public int CategoryId { get; set; }
        //[JsonIgnore] //игнорирование при сериализации
        public Category? Category { get; set; }

    }
}
