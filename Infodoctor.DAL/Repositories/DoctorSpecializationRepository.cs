﻿using System;
using System.Linq;
using Infodoctor.DAL.Interfaces;
using Infodoctor.Domain.Entities;

namespace Infodoctor.DAL.Repositories
{
    public class DoctorSpecializationRepository : IDoctorSpecializationRepository
    {
        private readonly AppDbContext _context;

        public DoctorSpecializationRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IQueryable<DoctorSpecialization> GetAllSpecializations()
        {
            return _context.DoctorSpecializations;
        }

        public DoctorSpecialization GetSpecializationById(int id)
        {
            return _context.DoctorSpecializations.First(ds => ds.Id == id);
        }

        public void Add(DoctorSpecialization specialization)
        {
            _context.DoctorSpecializations.Add(specialization);
            _context.SaveChanges();
        }

        public void Update(DoctorSpecialization specialization)
        {
            var updated = _context.DoctorSpecializations.First(ds => ds.Id == specialization.Id);
            updated = specialization;
            _context.SaveChanges();
        }

        public void Delete(DoctorSpecialization specialization)
        {
            _context.DoctorSpecializations.Remove(specialization);
            _context.SaveChanges();
        }
    }
}
