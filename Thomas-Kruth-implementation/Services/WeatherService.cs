using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThomasKruthimplementation.Models.Response;

namespace ThomasKruthimplementation.Services
{
    public class WeatherService
    {
        List<GetCurrentWeather> CurrentRepo { get; set; }
        List<GetWeekWeather> WeekRepo { get; set; }

        public WeatherService()
        {
            CurrentRepo = new List<GetCurrentWeather>(){
                new GetCurrentWeather(){
                    City = "Santa Monica",
                    Temp = 70.5m,
                    Measurement = "F"
                },
                new GetCurrentWeather(){
                    City = "Venice",
                    Temp = 72.5m,
                    Measurement = "F"
                },
                new GetCurrentWeather(){
                    City = "Marina Del Rey",
                    Temp = 73.5m,
                    Measurement = "F"
                }
            };

            WeekRepo = new List<GetWeekWeather>(){
                new GetWeekWeather(){
                    City = "Santa Monica",
                    WeekData = new List<Weather>(){
                        new Weather(){
                            Temp = 75.3m,
                            Date = DateTime.Now.Date.ToString(),
                            Measurement = "F"
                        },
                        new Weather(){
                            Temp = 75.3m,
                            Date = DateTime.Now.AddDays(1).Date.ToString(),
                            Measurement = "F"
                        },
                        new Weather(){
                            Temp = 75.3m,
                            Date = DateTime.Now.AddDays(2).Date.ToString(),
                            Measurement = "F"
                        },
                        new Weather(){
                            Temp = 75.3m,
                            Date = DateTime.Now.AddDays(3).Date.ToString(),
                            Measurement = "F"
                        },
                        new Weather(){
                            Temp = 75.3m,
                            Date = DateTime.Now.AddDays(4).Date.ToString(),
                            Measurement = "F"
                        }
                    }
                }
            };
 
        }

        public async Task<GetCurrentWeather> GetCurrenWeather(string city)
        {
            return CurrentRepo.FirstOrDefault(n => n.City == city);
        }

        public async Task<GetWeekWeather> GetWeekWeather(string city)
        {
            return WeekRepo.FirstOrDefault(n => n.City == city);
        }
    }
}
