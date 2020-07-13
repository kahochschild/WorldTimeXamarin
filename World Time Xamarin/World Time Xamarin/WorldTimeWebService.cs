using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Org.Json;

namespace World_Time_Xamarin
{
    public class WorldTimeWebService
    {

        public WorldTimeWebService()
        {
           
        }

        public async Task<TimeZones[]> GetTimeZonesAsync()
        {
            try
            {
                var client = new System.Net.Http.HttpClient();

                client.BaseAddress = new Uri("http://worldtimeapi.org/");

                var response = await client.GetAsync("api/timezone/America");

                var jsonString = response.Content.ReadAsStringAsync().Result;

                JSONArray itemArray = new JSONArray(jsonString);
                
                TimeZones[] time_zones = new TimeZones[itemArray.Length()];

                for (int i = 0; i < itemArray.Length(); i++)
                {
                    string value = itemArray.GetString(i);
                    time_zones[i] = new TimeZones
                    {
                        Name = value
                    };
                }

                return time_zones;
            }
            catch (Exception ex)
            {
                return new TimeZones[1];
            }

        }

        public async Task<TimeZoneResult> GetTimeZoneResultAsync(string timezone)
        {
            try
            {
                var client = new System.Net.Http.HttpClient();

                client.BaseAddress = new Uri("http://worldtimeapi.org/");

                var response = await client.GetAsync("api/timezone/" + timezone);

                var jsonString = response.Content.ReadAsStringAsync().Result;

                var timezoneResult = JsonConvert.DeserializeObject<TimeZoneResult>(jsonString);

                return timezoneResult;
            }
            catch (Exception ex)
            {
                return new TimeZoneResult();
            }

        }
    }
}