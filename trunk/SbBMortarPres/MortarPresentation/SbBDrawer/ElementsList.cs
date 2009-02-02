using System;
using System.Collections.Generic;
using SbBMortar.SbB;

namespace MortarPresentation
{
   
    public class ElementsList<T> : List<T>
    {
        
    public event EmptyDelegate onListChage=new EmptyDelegate(empty);
    static void empty(){}
    new public void Add(T o)
     {
        base.Add(o);
        onListChage();
     }
    new public void Remove(T o)
    {
        base.Remove(o);
        onListChage();
    }
    new public void RemoveAt(int i)
    {
        base.RemoveAt(i);
        onListChage();
    }
    new public void Clear()
    {
        base.Clear();
        onListChage();
    }    

    }
}