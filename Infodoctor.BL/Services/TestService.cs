using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
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
        private readonly IImagesRepository _imagesRepository;
        private readonly IImagesService _imagesService;
        private readonly Random _rnd = new Random();

        public TestService (IClinicRepository clinicRepository, IClinicSpecializationRepository clinicSpecializationRepository, 
            ICitiesRepository citiesRepository, IDoctorRepository doctorRepository, IImagesRepository imagesRepository, IImagesService imagesService)
        {
            _citiesRepository = citiesRepository ?? throw new ArgumentNullException(nameof(clinicRepository));
            _clinicSpecializationRepository = clinicSpecializationRepository ?? throw new ArgumentNullException(nameof(clinicSpecializationRepository));
            _doctorRepository = doctorRepository ?? throw new ArgumentNullException(nameof(doctorRepository));
            _clinicRepository = clinicRepository ?? throw new ArgumentNullException(nameof(citiesRepository));
            _imagesRepository = imagesRepository ?? throw new ArgumentNullException(nameof(imagesRepository));
            _imagesService = imagesService ?? throw new ArgumentNullException(nameof(imagesService));
        }

        public void Add10Clinics(string pathToImageFolder, Point[] imagesSizes)
        {
            var clinicList = new List<Clinic>();
            var maxClinicId = _clinicRepository.GetAllСlinics().Max(r => r.Id);
            var clinicSpecializationList = _clinicSpecializationRepository.GetAllClinicSpecializations().ToList();
            var cityList = _citiesRepository.GetAllCities().ToList();
            var doctrorList = _doctorRepository.GetAllDoctors().ToList();
            for (var i = maxClinicId + 1; i <= maxClinicId + 11; i++)
            {
                var clinic = new Clinic
                {
                    Name = "TestClinic" + i,
                    Email = "testclinic" + i + "@infodoctor.by",
                    Site = "TestClinic" + i + ".by",
                    Doctors = new List<Doctor>() {doctrorList[i % doctrorList.Count]},
                    CityAddresses = new List<ClinicAddress>()
                    {
                        new ClinicAddress()
                        {
                            City = cityList[i % 5],
                            Country = "TestCountry" + i,
                            Street = "TestStreet" + i,
                            Phones = new List<ClinicPhone>()
                            {
                                new ClinicPhone() {Description = "ClinicPhone" + i, Number = i + " 00 00"}
                            }
                        }
                    }
                };
                var clinicSpecializations = new List<ClinicSpecialization>();
                for (var j = 0; j < _rnd.Next(10); j++)
                {
                    clinicSpecializations.Add(clinicSpecializationList[_rnd.Next(clinicSpecializationList.Count/5)]);
                }
                clinic.ClinicSpecializations = clinicSpecializations.GroupBy(x => x.Id).Select(y => y.First()).ToList();
                clinic.Favorite = i%5 == 0;
                clinic.RatePoliteness = _rnd.Next(3) + 3;
                clinic.RatePrice = _rnd.Next(3) + 3;
                clinic.RateQuality = _rnd.Next(3) + 3;
                clinic.RateAverage = (clinic.RatePoliteness + clinic.RatePrice + clinic.RateQuality) / 3;

                var imagesFileNameList = new List<ImageFile>();
                for (var j = 0; j < 5; j++)
                {
                    var image = CreateBitmapImage(clinic.Name + "_" + j);
                    var imagesList = GetResizedImages(image, imagesSizes);
                    var imageFileName = GetImageFileName();
                    var isSuccess = SaveImagesToFiles(imagesList, pathToImageFolder, imageFileName); //TODO uncomment this after tests
                    if (isSuccess)
                    {
                        imagesFileNameList.Add(new ImageFile() { Name = imageFileName });
                    }
                }
                clinic.ImageName = imagesFileNameList;
            }
            _clinicRepository.AddMany(clinicList);
        }

        public void Add10Doctors()
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

        private List<Image> GetResizedImages(Image sourceFile, IEnumerable<Point> imagesSizes)
        {
            var bitmapList = new List<Image>();
            foreach (var imageSize in imagesSizes)
            {
                bitmapList.Add(_imagesService.GetResizedImage(sourceFile, imageSize.X, imageSize.Y));
            }
            
            return bitmapList;
        }

        private static Bitmap CreateBitmapImage(string imageText)
        {
            var imageWidth = 1024;
            var imageHeight = 768;
            var bitmap = new Bitmap(1, 1);

            // Создаем объект Font для "рисования" им текста.
            var font = new Font("Arial", 72, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);

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
            graphics.Clear(Color.LightCyan);
            // Задаем параметры анти-алиасинга
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            // Пишем (рисуем) текст
            graphics.DrawString(imageText, font, new SolidBrush(Color.FromArgb(0, 0, 0)), imageWidth / 2 - textWidth / 2, imageHeight / 2 - textHeight / 2);
            graphics.Flush();

            //string filePath = Server.MapPath(Url.Content("~/Content/Images/Image.jpg"));
            return bitmap;
        }

        private static string GetImageFileName()
        {
            return Guid.NewGuid().ToString().Replace("-", string.Empty) + ".jpg";
        }


        public bool SaveImagesToFiles(List<Image> images, string pathToImageFolder, string imageFileName)
        {
            if (!images.Any())
                throw new ArgumentNullException(nameof(images));
            var splittedFileName = imageFileName.Split('.');
            var fileNameForLarge = splittedFileName[0] + "_large." + splittedFileName[1];
            var fileNameForMedium = splittedFileName[0] + "_medum." + splittedFileName[1];
            var fileNameForSmall = splittedFileName[0] + "_small." + splittedFileName[1];
            var filePath = AppDomain.CurrentDomain.BaseDirectory + pathToImageFolder.Substring(1).Replace("/", @"\");
            try
            {
                images[0].Save(filePath + fileNameForLarge, ImageFormat.Jpeg);
                images[1].Save(filePath + fileNameForMedium, ImageFormat.Jpeg);
                images[2].Save(filePath + fileNameForSmall, ImageFormat.Jpeg);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
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

    }
}
