using System.Collections.Generic;
using webServiceNet.Models;

namespace webServiceNet.Repository
{
    public interface ICervejaRepository
    {
         void Add(Cerveja cerveja);
         IEnumerable<Cerveja> GetAll();
         Cerveja Find(long id);
         void Remove(long id);
         void Update(Cerveja cerveja);
    }
}