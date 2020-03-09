using System;
using System.Collections.Generic;

namespace UDemyCodeFirstVidzy
{
    public class Video
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public Genre Genre { get; set; }
    }
}
