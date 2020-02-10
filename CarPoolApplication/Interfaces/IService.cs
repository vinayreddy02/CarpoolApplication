using System;
using System.Collections.Generic;
using System.Text;

namespace CarPoolApplication.Services
{
    interface IService<T>
    {
        public bool Add(T item);
        public List<T> GetAll();
    }
}