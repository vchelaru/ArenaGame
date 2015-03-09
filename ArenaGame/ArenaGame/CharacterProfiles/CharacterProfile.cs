using System.IO;
using System.Xml.Serialization;

namespace ArenaGame.CharacterProfiles
{
    public class CharacterProfile
    {
        public string Name { get; set; }
        public int Gold { get; set; }

        public int Speed { get; set; }
        public int Melee { get; set; }
        public int Ranged { get; set; }
        public int Health { get; set; }

        /// <summary>
        /// Saves the character profile to the filename 
        /// </summary>
        /// <param name="filename">Relative path to the save file</param>
        public void SaveToFile(string filename)
        {
            var xs = new XmlSerializer(typeof (CharacterProfile));

            using (var fs = new FileStream(filename, FileMode.Create))
            {
                xs.Serialize(fs, this);
            }
        }

        /// <summary>
        /// Reads the character profile from the file
        /// </summary>
        /// <param name="filename">Relative path to the save file</param>
        public void ReadFromFile(string filename)
        {
            var xs = new XmlSerializer(typeof (CharacterProfile));
            CharacterProfile profile;

            using (var fs = new FileStream(filename, FileMode.Open))
            {
                profile = (CharacterProfile)xs.Deserialize(fs);
            }

            this.Name = profile.Name;
            this.Gold = profile.Gold;
            this.Speed = profile.Speed;
            this.Melee = profile.Melee;
            this.Ranged = profile.Ranged;
            this.Health = profile.Health;
        }
    }
}
