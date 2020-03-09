using System;
using System.Collections.Generic;

namespace UDemyCodeFirstVidzy
{
    public class Genre
    {
        public byte Id { get; set; }
        public string Name { get; set; }
        public ICollection<Video> Videos { get; set; }
    }
}