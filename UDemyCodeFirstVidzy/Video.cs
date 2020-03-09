using System;
using System.Collections.Generic;

namespace UDemyCodeFirstVidzy
{
    public enum Classification : byte
    {
        Silver = 1,
        Gold = 2,
        Platinum = 3
    }

    public class Video
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public DateTime ReleaseDate { get; set; }
        
        public Genre Genre { get; set; }
        public byte GenreId { get; set; }

        public Classification Classification { get; set; }
    }
}
