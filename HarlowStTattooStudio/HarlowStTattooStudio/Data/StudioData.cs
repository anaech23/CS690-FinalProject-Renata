using HarlowStTattooStudio.DomainModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.Json;
    
namespace HarlowStTattooStudio.Data
{
    public class StudioData
    {
        // Lists
        public List<Client> Clients { get; set; } = new List<Client>();
        public List<Artist> Artists { get; set; } = new List<Artist>();
        public List<Appointment> Appointments { get; set; } = new List<Appointment>();
        public List<Leave> Leaves { get; set; } = new List<Leave>();
        public List<Payment> Payments { get; set; } = new List<Payment>();

        // ID Counters
        public int NextClientId { get; set; } = 1;
        public int NextAppointmentId { get; set; } = 1;
        public int NextPaymentId { get; set; } = 1;
        public int NextLeaveId { get; set; } = 1;

        public StudioData()
        {
            Artists = new List<Artist>
            {
                new Artist { ArtistId = 1, ArtistFirstName = "Raul", ArtistLastName = "Jimenez" },
                new Artist { ArtistId = 2, ArtistFirstName = "Trinity", ArtistLastName = "Rodman" },
                new Artist { ArtistId = 3, ArtistFirstName = "Martin", ArtistLastName = "Odegaard" },
                new Artist { ArtistId = 4, ArtistFirstName = "Kiana", ArtistLastName = "Palacios" },
            };
        }

        private static readonly string FileName = "studio_data.json";

        public void Save()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };

            string jsonString = JsonSerializer.Serialize(this, options);

            File.WriteAllText(FileName, jsonString);
        }
        
        public static StudioData Load()
        {
            if (!File.Exists(FileName))
            {
                return new StudioData();
            }

            string jsonString = File.ReadAllText(FileName);
            return JsonSerializer.Deserialize<StudioData>(jsonString);
        }

    }
}

