using DataAccessLayer.Abstract;
using DataAccessLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class GenericRepository<T> : IGenericDal<T> where T : class
    {
        DatabaseContext con=new DatabaseContext();
        public void Add(T t)
        {
            con.Add(t);
            con.SaveChanges();
        }

        public void Delete(T t)
        {
            con.Remove(t);
            con.SaveChanges();
        }

        public T GetById(int id)
        {
            return con.Set<T>().Find(id);
        }

        public List<T> GetList()
        {
            return con.Set<T>().ToList();
        }

        public void Update(T t)
        {
            con.Update(t);
            con.SaveChanges();
        }
    }
}
