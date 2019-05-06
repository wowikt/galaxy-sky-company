using Abp.Domain.Repositories;
using Abp.Domain.Services;
using GalaxySkyCompany.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxySkyCompany.Domain.Services
{
    public class RandomFillDataService : DomainService, IRandomFillDataService
    {
        private const int TotalPilotCount = 200;
        private const int TotalPlaneCount = 200;
        private const int MinPlanesPerPilot = 3;
        private const int MaxPlanesPerPilot = 6;

        private readonly IRepository<Airport> _airportRepository;
        private readonly IRepository<Plane> _planeRepository;
        private readonly IRepository<Pilot> _pilotRepository;

        private IEnumerable<int> _airportIds;
        private Random _random = new Random();
        private IEnumerable<int> _letterCodes = Enumerable.Range('A', 26);
        private IEnumerable<int> _digitCodes = Enumerable.Range('0', 10);

        public RandomFillDataService(
            IRepository<Airport> airportRepository,
            IRepository<Plane> planeRepository,
            IRepository<Pilot> pilotRepository)
        {
            _airportRepository = airportRepository;
            _planeRepository = planeRepository;
            _pilotRepository = pilotRepository;
        }

        private readonly Airport[] _airports =
        {
            new Airport { Code = "SVO", Name = "Москва (Шереметьево)", Address = "г. Москва", },
            new Airport { Code = "DME", Name = "Москва (Домодедово)", Address = "г. Москва", },
            new Airport { Code = "VKO", Name = "Москва (Внуково)", Address = "г. Москва", },
            new Airport { Code = "LED", Name = "Санкт-Петербург (Пулково)", Address = "г. Санкт-Петербург", },
            new Airport { Code = "AER", Name = "Сочи", Address = "г. Сочи", },
            new Airport { Code = "SVX", Name = "Екатеринбург (Кольцово)", Address = "г. Екатеринбург", },
            new Airport { Code = "OVB", Name = "Новосибирск (Толмачёво)", Address = "г. Новосибирск", },
            new Airport { Code = "SIP", Name = "Симферополь", Address = "г. Симферополь", },
            new Airport { Code = "KRR", Name = "Краснодар (Пашковский)", Address = "г. Краснодар", },
            new Airport { Code = "UFA", Name = "Уфа", Address = "г. Уфа", },
            new Airport { Code = "ROV", Name = "Ростов-на-Дону (Платов)", Address = "г. Ростов-на-Дону", },
            new Airport { Code = "KZN", Name = "Казань", Address = "г. Казань", },
            new Airport { Code = "KUF", Name = "Самара (Курумоч)", Address = "г. Самара", },
            new Airport { Code = "VVO", Name = "Владивосток (Кневичи)", Address = "г. Владивосток", },
            new Airport { Code = "KJA", Name = "Красноярск (Емельяново)", Address = "г. Красноярск", },
            new Airport { Code = "MRV", Name = "Минеральные Воды", Address = "г. Минеральные Воды", },
            new Airport { Code = "IKT", Name = "Иркутск", Address = "г. Иркутск", },
            new Airport { Code = "KGD", Name = "Калининград (Храброво)", Address = "г. Калининград", },
            new Airport { Code = "KHV", Name = "Хабаровск (Новый)", Address = "г. Хабаровск", },
            new Airport { Code = "TJM", Name = "Тюмень (Рощино)", Address = "г. Тюмень", },
            new Airport { Code = "SGC", Name = "Сургут", Address = "г. Сургут", },
            new Airport { Code = "CEK", Name = "Челябинск (Баландино)", Address = "г. Челябинск", },
            new Airport { Code = "PEE", Name = "Пермь (Большое Савино)", Address = "г. Пермь", },
            new Airport { Code = "AAQ", Name = "Анапа (Витязево)", Address = "г. Анапа", },
            new Airport { Code = "MCX", Name = "Махачкала (Уйташ)", Address = "г. Махачкала", },
            new Airport { Code = "ZIA", Name = "Жуковский (Раменское)", Address = "г. Жуковский", },
            new Airport { Code = "VOG", Name = "Волгоград (Гумрак)", Address = "г. Волгоград", },
            new Airport { Code = "GOJ", Name = "Нижний Новгород (Стригино)", Address = "г. Нижний Новгород", },
            new Airport { Code = "OMS", Name = "Омск (Центральный)", Address = "г. Омск", },
            new Airport { Code = "UUS", Name = "Южно-Сахалинск (Хомутово)", Address = "г. Южно-Сахалинск", },
            new Airport { Code = "NUX", Name = "Новый Уренгой", Address = "г. Новый Уренгой", },
            new Airport { Code = "ARH", Name = "Архангельск (Талаги)", Address = "г. Архангельск", },
            new Airport { Code = "MMK", Name = "Мурманск", Address = "г. Мурманск", },
            new Airport { Code = "YKS", Name = "Якутск", Address = "г. Якутск", },
            new Airport { Code = "NBC", Name = "Нижнекамск (Бегишево)", Address = "г. Нижнекамск", },
            new Airport { Code = "VOZ", Name = "Воронеж (Чертовицкое)", Address = "г. Воронеж", },
            new Airport { Code = "REN", Name = "Оренбург (Центральный)", Address = "г. Оренбург", },
            new Airport { Code = "PKC", Name = "Петропавловск-Камчатский (Елизово)", Address = "г. Петропавловск-Камчатский", },
            new Airport { Code = "NJC", Name = "Нижневартовск", Address = "г. Нижневартовск", },
            new Airport { Code = "TOF", Name = "Томск (Богашёво)", Address = "г. Томск", },
            new Airport { Code = "ASF", Name = "Астрахань (Нариманово)", Address = "г. Астрахань", },
            new Airport { Code = "BAX", Name = "Барнаул (Михайловка)", Address = "г. Барнаул", },
            new Airport { Code = "SCW", Name = "Сыктывкар", Address = "г. Сыктывкар", },
            new Airport { Code = "NSK", Name = "Норильск (Алыкель)", Address = "г. Норильск", },
            new Airport { Code = "KEJ", Name = "Кемерово", Address = "г. Кемерово", },
            new Airport { Code = "EGO", Name = "Белгород", Address = "г. Белгород", },
            new Airport { Code = "RTW", Name = "Саратов (Центральный)", Address = "г. Саратов", },
            new Airport { Code = "BQS", Name = "Благовещенск (Игнатьево)", Address = "г. Благовещенск", },
            new Airport { Code = "HTA", Name = "Чита (Кадала)", Address = "г. Чита", },
            new Airport { Code = "STW", Name = "Ставрополь (Шпаковское)", Address = "г. Ставрополь", },
        };

        private readonly PlaneModel[] _planeModels =
        {
            new PlaneModel { Type = "Пассажирский самолёт", Brand = "Boeing", Model = "737-800" },
            new PlaneModel { Type = "Пассажирский самолёт", Brand = "Boeing", Model = "737-MAX8" },
            new PlaneModel { Type = "Пассажирский самолёт", Brand = "Boeing", Model = "777" },
            new PlaneModel { Type = "Пассажирский самолёт", Brand = "Airbus", Model = "A321" },
            new PlaneModel { Type = "Пассажирский самолёт", Brand = "Airbus", Model = "A320NEO" },
            new PlaneModel { Type = "Пассажирский самолёт", Brand = "Airbus", Model = "A380" },
            new PlaneModel { Type = "Пассажирский самолёт", Brand = "Bombardier", Model = "CRJ900" },
            new PlaneModel { Type = "Пассажирский самолёт", Brand = "Bombardier", Model = "CS100" },
            new PlaneModel { Type = "Пассажирский самолёт", Brand = "Embraer", Model = "ERJ 140" },
            new PlaneModel { Type = "Пассажирский самолёт", Brand = "Embraer", Model = "175" },
            new PlaneModel { Type = "Пассажирский самолёт", Brand = "Туполев", Model = "Ту-154" },
            new PlaneModel { Type = "Пассажирский самолёт", Brand = "Туполев", Model = "Ту-204" },
            new PlaneModel { Type = "Пассажирский самолёт", Brand = "Ильюшин", Model = "Ил-86" },
            new PlaneModel { Type = "Пассажирский самолёт", Brand = "Ильюшин", Model = "Ил-96-300" },
            new PlaneModel { Type = "Пассажирский самолёт", Brand = "Яковлев", Model = "Як-40" },
            new PlaneModel { Type = "Пассажирский самолёт", Brand = "Яковлев", Model = "Як-42" },
            new PlaneModel { Type = "Пассажирский самолёт", Brand = "Антонов", Model = "Ан-26" },
            new PlaneModel { Type = "Пассажирский самолёт", Brand = "Антонов", Model = "Ан-28" },
            new PlaneModel { Type = "Пассажирский самолёт", Brand = "Sukhoi", Model = "SSJ-100" },
            new PlaneModel { Type = "Пассажирский самолёт", Brand = "Иркут", Model = "МС-21" },
        };

        private readonly string[] _pilotFirstNames =
        {
            "Андрей",
            "Сергей",
            "Иван",
            "Дмитрий",
            "Александр",
            "Пётр",
            "Василий",
            "Джон",
            "Владимир",
            "Владислав",
            "Артём",
            "Тимофей",
            "Николай",
            "Максим",
            "Виктор",
            "Алексей",
            "Борис",
            "Геннадий",
            "Дмитрий",
            "Егор",
            "Игорь",
            "Константин",
            "Леонид",
            "Олег",
            "Роман",
            "Фёдор",
            "Эдуард",
            "Юрий",
            "Яков"
        };

        private readonly string[] _pilotLastNames =
        {
            "Авдеев",
            "Борисов",
            "Васильев",
            "Глушко",
            "Давыдов",
            "Емелин",
            "Ёлкин",
            "Жуков",
            "Звонков",
            "Иванов",
            "Киров",
            "Ладушкин",
            "Максименко",
            "Новиков",
            "Огнев",
            "Петров",
            "Раменский",
            "Свиридов",
            "Тульский",
            "Ульянов",
            "Фомин",
            "Харитонов",
            "Цыцкин",
            "Чувиков",
            "Шишкин",
            "Щукин",
            "Эммануилов",
            "Юрский",
            "Яковлев"
        };

        public async Task FillRandomData()
        {
            await FillAirports();
            await FillPilots();
            await FillPlanes();
            await LinkPlanesToPilots();
        }

        private async Task FillAirports()
        {
            foreach (var airport in _airports)
            {
                await _airportRepository.InsertAsync(airport);
            }

            await CurrentUnitOfWork.SaveChangesAsync();

            _airportIds = await _airportRepository.GetAll().Select(a => a.Id).ToListAsync();
        }

        private async Task FillPilots()
        {
            var airportIds = _airportIds.ToList();

            var airportId = SelectRandom(airportIds);
            var airport = SelectRandom(_airports);
            await _pilotRepository.InsertAsync(new Pilot
            {
                Name = "Денис Окань",
                Code = CreateRandomPilotCode(),
                Num = CreateRandomPilotNum(),
                AirportId = airportId,
                Address = airport.Address,
            });

            airportId = SelectRandom(airportIds);
            airport = SelectRandom(_airports);
            await _pilotRepository.InsertAsync(new Pilot
            {
                Name = "Алексей Кочемасов",
                Code = CreateRandomPilotCode(),
                Num = CreateRandomPilotNum(),
                AirportId = airportId,
                Address = airport.Address,
            });

            for (int i = 0; i < TotalPilotCount - 2; i++)
            {
                airportId = SelectRandom(airportIds);
                airport = SelectRandom(_airports);
                await _pilotRepository.InsertAsync(new Pilot
                {
                    Name = $"{SelectRandom(_pilotFirstNames)} {SelectRandom(_pilotLastNames)}",
                    Code = CreateRandomPilotCode(),
                    Num = CreateRandomPilotNum(),
                    AirportId = airportId,
                    Address = airport.Address,
                });
            }

            await CurrentUnitOfWork.SaveChangesAsync();
        }

        private async Task FillPlanes()
        {
            var airportIds = _airportIds.ToList();

            for (int i = 0; i < TotalPlaneCount; i++)
            {
                var airportId = SelectRandom(airportIds);
                var planeModel = SelectRandom(_planeModels);
                await _planeRepository.InsertAsync(new Plane
                {
                    Brand = planeModel.Brand,
                    Model = planeModel.Model,
                    Type = planeModel.Type,
                    AirportId = airportId,
                    Code = CreateRandomPlaneCode(),
                    TailNumber = CreateRandomPlaneTailNum(),
                    Name = $"Самолёт № {i + 1}",
                });
            }

            await CurrentUnitOfWork.SaveChangesAsync();
        }

        private async Task LinkPlanesToPilots()
        {
            var planeIds = await _planeRepository.GetAll().Select(p => p.Id).ToListAsync();

            foreach (var pilot in _pilotRepository.GetAll().Include(p => p.PilotPlanes))
            {
                var planesForPilotIds = new List<int>();
                int planeForPilotCount = MinPlanesPerPilot + _random.Next(MaxPlanesPerPilot - MinPlanesPerPilot + 1);
                while (planesForPilotIds.Count < planeForPilotCount)
                {
                    int planeId = SelectRandom(planeIds);
                    if (planesForPilotIds.Contains(planeId))
                    {
                        continue;
                    }

                    planesForPilotIds.Add(planeId);
                }

                foreach (var planeId in planesForPilotIds)
                {
                    pilot.PilotPlanes.Add(new PilotPlane
                    {
                        PilotId = pilot.Id,
                        PlaneId = planeId,
                    });
                }

                await CurrentUnitOfWork.SaveChangesAsync();
            }
        }

        private string CreateRandomPilotCode()
        {
            StringBuilder sb = new StringBuilder();
            var letterCodes = _letterCodes.ToList();
            var digitCodes = _digitCodes.ToList();
            sb.Append((char)SelectRandom(letterCodes));
            sb.Append((char)SelectRandom(letterCodes));
            sb.Append((char)SelectRandom(digitCodes));
            sb.Append((char)SelectRandom(digitCodes));
            sb.Append('-');
            sb.Append((char)SelectRandom(digitCodes));
            sb.Append((char)SelectRandom(digitCodes));
            sb.Append((char)SelectRandom(digitCodes));
            return sb.ToString();
        }

        private string CreateRandomPilotNum()
        {
            StringBuilder sb = new StringBuilder();
            var digitCodes = _digitCodes.ToList();
            sb.Append((char)SelectRandom(digitCodes));
            sb.Append((char)SelectRandom(digitCodes));
            sb.Append((char)SelectRandom(digitCodes));
            sb.Append((char)SelectRandom(digitCodes));
            sb.Append((char)SelectRandom(digitCodes));
            sb.Append((char)SelectRandom(digitCodes));
            sb.Append((char)SelectRandom(digitCodes));
            sb.Append((char)SelectRandom(digitCodes));
            return sb.ToString();
        }

        private string CreateRandomPlaneTailNum()
        {
            StringBuilder sb = new StringBuilder("RA-");
            var digitCodes = _digitCodes.ToList();
            sb.Append((char)SelectRandom(digitCodes));
            sb.Append((char)SelectRandom(digitCodes));
            sb.Append((char)SelectRandom(digitCodes));
            sb.Append((char)SelectRandom(digitCodes));
            sb.Append((char)SelectRandom(digitCodes));
            return sb.ToString();
        }

        private string CreateRandomPlaneCode()
        {
            StringBuilder sb = new StringBuilder();
            var letterCodes = _letterCodes.ToList();
            var digitCodes = _digitCodes.ToList();
            sb.Append((char)SelectRandom(letterCodes));
            sb.Append((char)SelectRandom(letterCodes));
            sb.Append((char)SelectRandom(digitCodes));
            sb.Append((char)SelectRandom(digitCodes));
            sb.Append('-');
            sb.Append((char)SelectRandom(digitCodes));
            sb.Append((char)SelectRandom(digitCodes));
            sb.Append((char)SelectRandom(digitCodes));
            return sb.ToString();
        }

        private T SelectRandom<T>(IList<T> list)
        {
            return list[_random.Next(list.Count)];
        }

        private class PlaneModel
        {
            public string Brand { get; set; }
            public string Model { get; set; }
            public string Type { get; set; }
        }
    }
}
