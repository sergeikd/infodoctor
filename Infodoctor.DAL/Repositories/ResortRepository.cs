using System;
using System.Collections.Generic;
using System.Linq;
using Infodoctor.DAL.Interfaces;
using Infodoctor.Domain.Entities;

namespace Infodoctor.DAL.Repositories
{
    public class ResortRepository : IResortRepository
    {
        private readonly AppDbContext _context;

        public ResortRepository(AppDbContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            _context = context;
        }

        public IQueryable<Resort> GetAllResorts()
        {
            return _context.Resorts.OrderBy(r => r.Id);
        }

        public IQueryable<Resort> GetSortedResorts(string sortBy, bool descending, string lang)
        {
            switch (sortBy)
            {

                default:
                    return descending ? _context.Resorts.OrderByDescending(c => c.Localized.FirstOrDefault(l => l.Language.Code.ToLower() == lang.ToLower()).Name) : _context.Resorts.OrderBy(c => c.Localized.FirstOrDefault(l => l.Language.Code.ToLower() == lang.ToLower()).Name);
                case "alphabet":
                    return descending ? _context.Resorts.OrderByDescending(c => c.Localized.FirstOrDefault(l => l.Language.Code.ToLower() == lang.ToLower()).Name) : _context.Resorts.OrderBy(c => c.Localized.FirstOrDefault(l => l.Language.Code.ToLower() == lang.ToLower()).Name);
                case "rate":
                    return descending ? _context.Resorts.OrderByDescending(c => c.RateAverage) : _context.Resorts.OrderBy(c => c.RateAverage);
                case "price":
                    return descending ? _context.Resorts.OrderByDescending(c => c.RatePrice) : _context.Resorts.OrderBy(c => c.RatePrice);
            }
        }

        public Resort GetResortById(int id)
        {
            try
            {
                var result = _context.Resorts.First(r => r.Id == id);
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void Add(Resort res)
        {
            _context.Resorts.Add(res);
            _context.SaveChanges();
        }

        public void Update(Resort res)
        {
            var updated = _context.Resorts.First(r => r.Id == res.Id);
            updated = res;
        }

        public void Delete(Resort res)
        {
            var adr = new ResortAddress();
            var phones = new List<ResortPhone>();
            var reviews = new List<ResortReview>();

            //todo: Проверить нормаль но работает удаление
            //if (res.Address != null)
            //{
            //    adr = res.Address;

            //    if (res.Address.Localized.ToArray()[0].Phones.Any())
            //        phones = res.Address.Phones.ToList();
            //}

            if (res.Reviews.Any())
                reviews = res.Reviews.ToList();

            _context.Resorts.Remove(res);
            //_context.ResortAddresses.Remove(adr);
            //_context.ResortPhones.RemoveRange(phones);
            _context.ResortReviews.RemoveRange(reviews);

            _context.SaveChanges();
        }
    }
}
