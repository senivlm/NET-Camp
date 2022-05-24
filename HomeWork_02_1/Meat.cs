﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET_CAMP_HomeWork_s1_02_01
{
    public class Meat : Product
    {
        //Properties
        //Ідентифікаторів є безліч!
        public Category Category { get; set; }
        public Kind Kind { get; }

        //Constructors
        //Свідомо не робив конструктор без параметрів, 3oоб захиститісь від створення об'ектів без ініціализованих обов`язкових полів\властивостей. Відкритий до дискусії стосовно цього.
        //Погано для колекцій екземплярів
        public Meat(string name, Category category, Kind kind) : base(name)
        {
          this.Category = category;
          this.Kind = kind;
        }
        public Meat(string name, Category category, Kind kind, float price, float weight) : base(name, price, weight)
        {
            this.Category = category;
            this.Kind = kind;
        }

        //Methods
        public override string? ToString()
        {
            return String.Format($"Meat name={this.Name} Category={this.Category} kind={this.Kind} price={this.Price} weight={this.Weight}");
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override bool Equals(object? obj)
        {
            if (obj is Meat)
            {
                return base.Equals(obj)
                    && this.Category == (obj as Meat)?.Category
                    && this.Kind == (obj as Meat)?.Kind
                    ;
            }
            else
            {
                return false;
            }
        }

        public override void SetPrice(float percent)
        {
            base.SetPrice(percent);
//Не зовсім зрозуміла ідея
            this.Price = this.Price * (1 + ((float)this.Category) / 100);
        }


    }
}
