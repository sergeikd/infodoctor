﻿using System.Collections.Generic;
using Infodoctor.BL.DtoModels;

namespace Infodoctor.BL.Interfaces
{
    public interface IResortService
    {
        IEnumerable<DtoResortSingleLang> GetAllResorts(string pathToImage, string lang);
        DtoPagedResorts GetPagedResorts(int perPage, int numPage, string pathToImage, string lang, int type);
        DtoPagedResorts SearchResorts(int perPage, int numPage, DtoResortSearchModel searchModel, string pathToImage, string lang);
        DtoResortSingleLang GetResortById(int id, string pathToImage, string lang);
        DtoResortMultiLang GetResortById(int id, string pathToImage);
        void Add(DtoResortMultiLang res);
        void Update(DtoResortMultiLang res);
        void Delete(int id, string pathToImage);
    }
}
