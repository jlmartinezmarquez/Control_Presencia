using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.DataAccess.Contracts
{
    public interface IGenericRepository<Entity> where Entity:class
    {
        public int Add(Entity entity);
        public int Edit(Entity entity);
        public int Delete(int Id);
        public IEnumerable<Entity> GetAll();
    }
}
