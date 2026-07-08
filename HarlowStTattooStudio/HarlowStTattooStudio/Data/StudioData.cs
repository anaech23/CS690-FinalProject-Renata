using HarlowStTattooStudio.DomainModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace HarlowStTattooStudio.Data
{
    public class StudioData
    {
        public List<Client> Clients { get; set; } = new List<Client>();

        public static List<Artist> Artists = new List<Artist>
        {
            new Artist { ArtistId = 1, ArtistFirstName = "Raul", ArtistLastName = "Jimenez" },
            new Artist { ArtistId = 2, ArtistFirstName = "Trinity", ArtistLastName = "Rodman" },
            new Artist {ArtistId = 3, ArtistFirstName = "Martin", ArtistLastName = "Odegaard" },
            new Artist { ArtistId = 4, ArtistFirstName = "Kiana", ArtistLastName = "Palacios" }
        };

        public List<Appointment> Appointments { get; set; } = new List<Appointment>();
        public List<Leave> Leaves { get; set; } = new List<Leave>();
        public List<Payment> Payments { get; set; } = new List<Payment>();
    }
}
