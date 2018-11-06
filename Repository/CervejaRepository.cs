using System.Collections.Generic;
using System.Linq;
using webServiceNet.Models;

namespace webServiceNet.Repository
{
    public class CervejaRepository : ICervejaRepository
    {

        private readonly CervejaDbContext _context;
        public CervejaRepository(CervejaDbContext ctx)
        {
            _context = ctx;
        }
        public void Add(Cerveja cerveja)
        {
            _context.Cervejas.Add(cerveja);
            _context.SaveChanges();
        }

        public Cerveja Find(long id)
        {
            return _context.Cervejas.FirstOrDefault(c=>c.Id==id);
        }

        public IEnumerable<Cerveja> GetAll()
        {
            return _context.Cervejas.ToList();
        }

        public void Remove(long id)
        {
            var cerveja = _context.Cervejas.First(c=>c.Id==id);
            _context.Cervejas.Remove(cerveja);
            _context.SaveChanges();
        }

        public void Update(Cerveja cerveja)
        {
            _context.Cervejas.Update(cerveja);
            _context.SaveChanges();
        }
    }
}