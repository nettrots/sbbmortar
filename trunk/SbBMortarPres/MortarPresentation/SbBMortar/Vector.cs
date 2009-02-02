using System;
using System.Collections;
using System.Collections.Generic;

namespace SbBMortar.SbB
{
    public class Vector: ICloneable, IEnumerable, IEnumerator
    {
        //Змінні класу
        private List<double> elements = new List<double>();
        private int pos = -1;


        //Конструктори
        public Vector() { }
        public Vector(int size)
        {
            for (int i = 0; i < size; i++)
                elements.Add(0.0);
        }
        public Vector(int size, double element)
        {
            for (int i = 0; i < size; i++)
                elements.Add(element);
        }
        public Vector(double[] elements)
        {
            for (int i = 0; i < elements.Length; i++)
                this.elements.Add(elements[i]);
        }


        //Властивості (Properties)
        public double this[int index]
        {
            get { return (double)elements[index]; }
            set { elements[index] = value; }
        }
        public int Length
        {
            get { return elements.Count; }
            set 
            {
                for (int i = elements.Count; i <= value; i++)
                    elements.Add(0.0);
                for (int i = elements.Count; i > value; i--)
                    elements.RemoveAt(i - 1);
            }
        }


        //Оператори
        //унарні
        public static Vector operator +(Vector v)
        {
            double[] rez = new double[v.Length];
            for (int i = 0; i < v.Length; i++)
                rez[i] = (double)v.elements[i];
            return new Vector(rez);
        }
        public static Vector operator -(Vector v)
        {
            double[] rez = new double[v.Length];
            for (int i = 0; i < v.Length; i++)
                rez[i] = -(double)v.elements[i];
            return new Vector(rez);
        }
        //бінарні
        public static Vector operator +(Vector vLeft, Vector vRight)
        {
            if (vLeft.Length != vRight.Length)
                throw new Exception("Different length");
            else
            {
                double[] rez = new double[vLeft.Length];
                for (int i = 0; i < vLeft.Length; i++)
                    rez[i] = (double)vLeft.elements[i] + (double)vRight.elements[i];
                return new Vector(rez);
            }
        }
        public static Vector operator -(Vector vLeft, Vector vRight)
        {
            if (vLeft.Length != vRight.Length)
                throw new Exception("Different length");
            else
            {
                double[] rez = new double[vLeft.Length];
                for (int i = 0; i < vLeft.Length; i++)
                    rez[i] = (double)vLeft.elements[i] - (double)vRight.elements[i];
                return new Vector(rez);
            }
        }
        //скалярне множення векторів
        public static double operator *(Vector vLeft, Vector vRight)
        {
            if (vLeft.Length != vRight.Length)
                throw new Exception("Different length");
            else
            {
                double rez = 0;
                for (int i = 0; i < vLeft.Length; i++)
                    rez += (double)vLeft.elements[i] * (double)vRight.elements[i];
                return rez;
            }
        }
        //множення на константу
        public static Vector operator *(double k, Vector vRight)
        {
            double[] rez = new double[vRight.Length];
            for (int i = 0; i < vRight.Length; i++)
                rez[i] = k * (double)vRight.elements[i];
            return new Vector(rez);
        }
        public static Vector operator *(Vector vLeft, double k)
        {
            double[] rez = new double[vLeft.Length];
            for (int i = 0; i < vLeft.Length; i++)
                rez[i] = k * (double)vLeft.elements[i];
            return new Vector(rez);
        }
        //ділення на константу
        public static Vector operator /(Vector vLeft, double k)
        {
            double[] rez = new double[vLeft.Length];
            for (int i = 0; i < vLeft.Length; i++)
                rez[i] = (double)vLeft.elements[i] / k;
            return new Vector(rez);
        }
        //оператори відношення
        public static bool operator ==(Vector vLeft, Vector vRight)
        {
            if ((object)vLeft==null || (object)vRight==null) 
                return (object)vLeft == (object) vRight;
            if (vLeft.Length != vRight.Length)
                throw new Exception("Can't be compare");
            for (int i = 0; i < vLeft.Length; i++)
                if ((double)vLeft.elements[i] != (double)vRight.elements[i])
                    return false;
            return true;
        }
        public static bool operator !=(Vector vLeft, Vector vRight)
        {
            /*if ((object)vLeft == null || (object)vRight == null) return true;
            if (vLeft.Length != vRight.Length)
                throw new Exception("Can't be compare");*/
            return !(vLeft == vRight);
        }


        //Методи
        //додавання елемента
        public void add(double element)
        {
            elements.Add(element);
        }
        //вставити елемент
        public void insert(int index, double element)
        {
            elements.Insert(index, element);
        }
        //видалення елемента з вектора
        public void remove(double element)
        {
            elements.Remove(element);
        }
        //вилучення з позиції
        public void removeAt(int index)
        {
            elements.RemoveAt(index);
        }
        //занулення всіх елементів
        public void clear()
        {
            for (int i = 0; i < elements.Count; i++)
                elements[i] = 0;
        }
        //стирання(знищення) всіх елементів
        public void erase()
        {
            elements.Clear();
        }
        //знаходження мінімуму в векторі
        public double min()
        {
            double m = (double)elements[0];
            for (int i = 1; i < Length; i++)
                if (m > (double)elements[i])
                    m = (double)elements[i];
            return m;
        }
        //знаходження максимуму в векторі
        public double max()
        {
            double m = (double)elements[0];
            for (int i = 1; i < Length; i++)
                if (m < (double)elements[i])
                    m = (double)elements[i];
            return m;
        }
        // норма
        public double norm(Norma v)
        {
            if (v==Norma.Maximum)
            {
                double m = Math.Abs((double)elements[0]);
                for (int i = 1; i < elements.Count; i++)
                      m = m < Math.Abs((double)elements[i]) ? Math.Abs((double)elements[i]) : m;
                return m;
            }

            int deg = (int)v;
            double rez = 0;
            for (int i = 0; i < elements.Count; i++)
                rez += Math.Pow(Math.Abs((double)elements[i]), deg);
            rez = Math.Pow(rez, 1 / (double)deg);
            return rez;
        }
        public double norm()
        {
            return norm(Norma.Euclidean);
        }
        //визначення чи знаходиться елемент в векторі
        public bool contains(double element)
        {
            return elements.Contains(element);
        }
        //знаходження першого входження елемента в вектор
        public int indexOf(double element)
        {
            return elements.IndexOf(element);
        }
        //знаходження останнього входження елемента в вектор
        public int lastIndexOf(double element)
        {
            return elements.LastIndexOf(element);
        }
        //сортування елементів вектора
        public void sort()
        {
            elements.Sort();
        }
        //перевернути вектор
        public void reverse()
        {
            elements.Reverse();
        }
        //двійковий пошук
        public int binarySearch(double element)
        {
            return elements.BinarySearch(element);
        }
        //переведення у звичайний масив
        public double[] ToArray()
        {
            return elements.ToArray();
        }
        //символьне представлення (перевантажена від System.Object)
        public override string ToString()
        {
            string str = "";
            for (int i = 0; i < elements.Count; i++)
            {
                str += elements[i].ToString() + " "; 
            }
            return str;

        }
        // порівняння об'єктів
        public override bool Equals(object obj)
        {
            return this == (Vector)obj;
        }
        // хеш-код
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }


        //інтерфейси
        //ICloneable
        public object Clone()
        {
            return new Vector(elements.ToArray());
        }
        //IEnumerator
        public bool MoveNext()
        {
            if (pos < elements.Count-1)
            {
                pos++;
                return true;
            }
            else return false;
        }
        public void Reset()
        {
            pos = 0;
        }
        public object Current
        {
            get { return elements[pos]; }
        }
        //IEnumerable
        public IEnumerator GetEnumerator()
        {
            return (IEnumerator)this;
        }
    }
}
