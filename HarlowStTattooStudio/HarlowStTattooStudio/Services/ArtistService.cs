using HarlowStTattooStudio.Data;
using HarlowStTattooStudio.DomainModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace HarlowStTattooStudio.Services
{
    public class ArtistService
    {
        private readonly StudioData studioData;

        public ArtistService(StudioData studioData)
        {
            this.studioData = studioData;
        }

        public List<Artist> GetAllArtists() 
        { 
            return studioData.Artists; 
        }
    }
}
