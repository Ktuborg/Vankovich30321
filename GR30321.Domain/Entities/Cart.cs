﻿using GR30321.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GR30321.Domain.Entities
{
    public class Cart
    {
        public int Id { get; set; }
        // <summary>
        // Список объектов в корзине
        // key - идентификатор объекта
        // </summary>

        // [NotMapped] 
        public Dictionary<int, CartItem> CartItems { get; set; } = new();
        /// <summary>
        /// Добавить объект в корзину
        /// </summary>
        /// <param name="picture">Добавляемый объект</param>
        public virtual void AddToCart(Picture picture)
        {
            if (CartItems.ContainsKey(picture.Id))
            {
                CartItems[picture.Id].Qty++;
            }
            else
            {
                CartItems.Add(picture.Id, new CartItem
                {
                    Item = picture,
                    Qty = 1
                });
            };
        }
        /// <summary>
        /// Удалить объект из корзины
        /// </summary>
        /// <param name="picture">удаляемый объект</param>
        public virtual void RemoveItems(int id)
        {
            CartItems.Remove(id);
        }
        /// <summary>
        /// Очистить корзину
        /// </summary>
        public virtual void ClearAll()
        {
            CartItems.Clear();
        }
        /// <summary>
        /// Количество объектов в корзине
        /// </summary>
        public int Count { get => CartItems.Sum(item => item.Value.Qty); }
        /// <summary>
        /// Общее количество калорий
        /// </summary>
        public double TotalSum
        {
            get => CartItems.Sum(item => item.Value.Item.Price * item.Value.Qty);
        }
    }


}