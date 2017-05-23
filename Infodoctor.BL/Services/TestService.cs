using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using Infodoctor.BL.Interfaces;
using Infodoctor.DAL.Interfaces;
using Infodoctor.Domain.Entities;

namespace Infodoctor.BL.Services
{
    public class TestService : ITestService
    {
        private readonly IClinicRepository _clinicRepository;
        private readonly ICitiesRepository _citiesRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IClinicSpecializationRepository _clinicSpecializationRepository;
        private readonly Random _rnd = new Random();

        public TestService (IClinicRepository clinicRepository, IClinicSpecializationRepository clinicSpecializationRepository, ICitiesRepository citiesRepository, IDoctorRepository doctorRepository)
        {
            if (clinicRepository == null)
                throw new ArgumentNullException(nameof(citiesRepository));
            if (clinicSpecializationRepository == null)
                throw new ArgumentNullException(nameof(clinicSpecializationRepository));
            if (citiesRepository == null)
                throw new ArgumentNullException(nameof(clinicRepository));
            if (doctorRepository == null)
                throw new ArgumentNullException(nameof(doctorRepository));

            _citiesRepository = citiesRepository;
            _clinicSpecializationRepository = clinicSpecializationRepository;
            _doctorRepository = doctorRepository;
            _clinicRepository = clinicRepository;
        }

        public void Add100Clinics(string pathToImage)
        {
            var clinic = new Clinic();
            var maxClinicId = _clinicRepository.GetAllСlinics().Max(r => r.Id);
            var clinicSpecializationList = _clinicSpecializationRepository.GetAllClinicSpecializations().ToList();
            var cityList = _citiesRepository.GetAllCities().ToList();
            var doctrorList = _doctorRepository.GetAllDoctors().ToList();
            
            for (var i = maxClinicId + 1; i <= maxClinicId + 101; i++)
            {
                clinic.Name = "TestClinic" + i;
                clinic.Email = "testclinic" + i + "@infodoctor.by";
                clinic.Site = "TestClinic" + i + ".by";
                clinic.Doctors = new List<Doctor>() {doctrorList[i%doctrorList.Count]};
                clinic.CityAddresses = new List<ClinicAddress>()
                {
                    new ClinicAddress(){
                        City = cityList[i % 5],
                        Country = "TestCountry" + i,
                        Street = "TestStreet" + i,
                        Phones = new List<ClinicPhone>() { new ClinicPhone() {Description = "ClinicPhone" + i, Number = i + " 00 00"}
                }}};
                var clinicSpecializations = new List<ClinicSpecialization>();
                for (var j = 0; j < _rnd.Next(10); j++)
                {
                    clinicSpecializations.Add(clinicSpecializationList[_rnd.Next(clinicSpecializationList.Count/5)]);
                }
                clinic.ClinicSpecializations = clinicSpecializations.GroupBy(x => x.Id).Select(y => y.First()).ToList();
                clinic.Favorite = i%10 == 0;
                _clinicRepository.Add(clinic);
                CreateBitmapImage(clinic.Name, "");
            }
        }
        public void Add100Doctors()
        {
            var doctor = new Doctor();
            var maxDoctorId = _clinicRepository.GetAllСlinics().Max(r => r.Id);
        }

        public Clinic PrepareClinic()
        {
            var clinic = new Clinic();
            var clinicSpecializationList = _clinicSpecializationRepository.GetAllClinicSpecializations().ToList();
            var cityList = _citiesRepository.GetAllCities().ToList();
            var doctrorList = _doctorRepository.GetAllDoctors().ToList();
            clinic.Name = "";
            clinic.Email = "";
            clinic.Site = "";
            clinic.Doctors = new List<Doctor>();
            clinic.CityAddresses = new List<ClinicAddress>();
            clinic.ClinicSpecializations = new List<ClinicSpecialization>();
            clinic.Favorite = false;
            return clinic;
        }

        private Bitmap CreateBitmapImage(string imageText, string path)
        {
            var imageWidth = 960;
            var imageHeight = 720;
            var bitmap = new Bitmap(1, 1);

            // Создаем объект Font для "рисования" им текста.
            var font = new Font("Arial", 20, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);

            // Создаем объект Graphics для вычисления высоты и ширины текста.
            var graphics = Graphics.FromImage(bitmap);

            // Определение размеров изображения.
            var textWidth = (int)graphics.MeasureString(imageText, font).Width;
            var textHeight = (int)graphics.MeasureString(imageText, font).Height;

            // Пересоздаем объект Bitmap с откорректированными размерами под текст и шрифт.
            bitmap = new Bitmap(bitmap, new Size(imageWidth, imageHeight));

            // Пересоздаем объект Graphics
            graphics = Graphics.FromImage(bitmap);

            // Задаем цвет фона.
            graphics.Clear(Color.White);
            // Задаем параметры анти-алиасинга
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            // Пишем (рисуем) текст
            graphics.DrawString(imageText, font, new SolidBrush(Color.FromArgb(0, 0, 0)), imageWidth / 2 - textWidth / 2, imageHeight / 2 - textHeight / 2);
            graphics.Flush();

            //string filePath = Server.MapPath(Url.Content("~/Content/Images/Image.jpg"));
            return bitmap;
        }

        private string GetImageFileName()
        {
            return Guid.NewGuid().ToString().Replace("-", string.Empty) + ".jpg";
        }

        private void SaveImageToFile(Bitmap imageFile, string imageFolderPath, string pathToImage)
        {
            
        }

        public string Add(Bitmap imageFile, string imageFolderPath, string pathToImage, int maxImageWidth)
        {
            if (imageFile == null)
                throw new ArgumentNullException(nameof(imageFile));

            var imgFileName = Guid.NewGuid().ToString().Replace("-", string.Empty) + ".jpg";
            var filePath = AppDomain.CurrentDomain.BaseDirectory + imageFolderPath.Replace("~/", string.Empty).Replace("/", @"\") + imgFileName;
            //var image = Image.FromStream(imageFile.InputStream, true, true);

            //bool result = false;

            //if (maxImageWidth == 0)
            //    result = ResizeImage(image, imgFileName, imageFolderPath, image.Width);
            //else
            //    result = ResizeImage(image, imgFileName, imageFolderPath, maxImageWidth);


            var img = new ImageFile() { Name = imgFileName, Path = filePath };

            //if (result)
            //    _imageRepository.Add(img);

            return pathToImage + imgFileName;
        }

    }

    internal class CreateImage
    {
        private Bitmap CreateBitmapImage(string imageText, string path)
        {
            var imageWidth = 960;
            var imageHeight = 720;
            var bitmap = new Bitmap(1, 1);

            // Создаем объект Font для "рисования" им текста.
            var font = new Font("Arial", 20, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);

            // Создаем объект Graphics для вычисления высоты и ширины текста.
            var graphics = Graphics.FromImage(bitmap);

            // Определение размеров изображения.
            var textWidth = (int)graphics.MeasureString(imageText, font).Width;
            var textHeight = (int)graphics.MeasureString(imageText, font).Height;

            // Пересоздаем объект Bitmap с откорректированными размерами под текст и шрифт.
            bitmap = new Bitmap(bitmap, new Size(imageWidth, imageHeight));

            // Пересоздаем объект Graphics
            graphics = Graphics.FromImage(bitmap);

            // Задаем цвет фона.
            graphics.Clear(Color.White);
            // Задаем параметры анти-алиасинга
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            // Пишем (рисуем) текст
            graphics.DrawString(imageText, font, new SolidBrush(Color.FromArgb(0, 0, 0)), imageWidth/2 - textWidth/2, imageHeight/2 - textHeight/2);
            graphics.Flush();

            //string filePath = Server.MapPath(Url.Content("~/Content/Images/Image.jpg"));
            return bitmap;
        }
    }
}
