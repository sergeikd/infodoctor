﻿using System.Collections.Generic;
using Infodoctor.BL.DtoModels;

namespace Infodoctor.BL.Interfaces
{
    public interface IClinicService
    {
        IEnumerable<DtoClinicSingleLang> GetAllClinics(string pathToImage, string lang);
        DtoClinicSingleLang GetClinicById(int id, string pathToImage, string lang);
        DtoClinicMultiLang GetClinicById(int id, string pathToClinicImage);
        DtoPagedClinic GetPagedClinics(int perPage, int pageNum, string pathToImage, string lang, int type);
        DtoPagedClinic SearchClinics(int perPage, int numPage, DtoClinicSearchModel searchModel, string pathToImage, string lang, int type);
        void Add(DtoClinicMultiLang clinic);
        void AddMany(List<DtoClinicMultiLang> clinics);
        void Update(DtoClinicMultiLang clinic);
        void Delete(int id);
    }
}
