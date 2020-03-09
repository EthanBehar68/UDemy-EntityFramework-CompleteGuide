using System.Collections.Generic;

namespace UDemyCodeFirstVidzy
{
    public class Tag
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Video> Videos { get; set; }
    }
}
