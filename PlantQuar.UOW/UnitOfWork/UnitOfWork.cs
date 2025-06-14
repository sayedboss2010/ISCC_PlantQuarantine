using PlantQuar.DAL;
using PlantQuar.UOW.Repository;
using Privilages.DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlantQuar.UOW.UnitOfWork
{
    public class UnitOfWork
    {
        private dynamic entities;

        public UnitOfWork(int x = 0)
        {
            if (x == 1)
            {
                entities = new dbPrivilageEntities();
            }
            else
            {
                entities = new PlantQuarantineEntities();
            }
        }

        public Dictionary<Type, object> repositories = new Dictionary<Type, object>();

        public Repository<T> Repository<T>() where T : class
        {
            if (repositories.Keys.Contains(typeof(T)) == true)
            {
                return repositories[typeof(T)] as Repository<T>;
            }
            Repository<T> repo = new Repository<T>(entities);
            repositories.Add(typeof(T), repo);
            return repo;
        }

        public bool SaveChanges()
        {
            return entities.SaveChanges() > 0;
        }



        //private bool disposed = false;

        //protected virtual void Dispose(bool disposing)
        //{
        //    if (!this.disposed)
        //    {
        //        if (disposing)
        //        {
        //            entities.Dispose();
        //        }
        //    }
        //    this.disposed = true;
        //}

        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}
    }
}
