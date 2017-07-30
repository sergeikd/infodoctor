using System;
using System.Collections.Generic;

namespace Infodoctor.BL.DtoModels
{
    public class DtoPaged
    {
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling((decimal)TotalCount / PageSize);  // total pages 
    }

    public class DtoPagedArticles : DtoPaged
    {
        public List<DtoArticle> Articles { get; set; }
    }

    public class DtoPagedArticleComments : DtoPaged
    {
        public IEnumerable<DtoArticleComment> Comments { get; set; }
    }

    public class DtoPagedClinic : DtoPaged
    {
        public List<DtoClinicSingleLang> Clinics { get; set; }
    }

    public class DtoPagedClinicReview : DtoPaged
    {
        public IEnumerable<DtoClinicReview> ClinicReviews { get; set; }
    }

    public class DtoPagedDoctor : DtoPaged
    {
        public List<DtoDoctorSingleLang> Doctors { get; set; }
    }

    public class DtoPagedDoctorReview : DtoPaged
    {
        public IEnumerable<DtoDoctorReview> DoctorReviews { get; set; }
    }

    public class DtoPagedResorts : DtoPaged
    {
        public List<DtoResortSingleLang> Resorts;
    }

    public class DtoPagedResortReview : DtoPaged
    {
        public IEnumerable<DtoResortReview> ResortReviews { get; set; }
    }
}
