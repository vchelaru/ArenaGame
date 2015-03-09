using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace ArenaGame.CharacterProfiles
{
    public static class CharacterProfileSave
    {
        public static List<CharacterProfile> Profiles { get; set; }

        /// <summary>
        /// Saves the character profile to the filename 
        /// </summary>
        /// <param name="filename">Relative path to the save file</param>
        public static void ToFile(string filename = "profiles.xml")
        {
            var xs = new XmlSerializer(typeof(List<CharacterProfile>));

            using (var fs = new FileStream(filename, FileMode.Create))
            {
                xs.Serialize(fs, Profiles);
            }
        }

        /// <summary>
        /// Reads the character profile from the file
        /// </summary>
        /// <param name="filename">Relative path to the save file</param>
        public static void FromFile(string filename = "profiles.xml")
        {
            var xs = new XmlSerializer(typeof(CharacterProfile));
            List<CharacterProfile> profile;

            using (var fs = new FileStream(filename, FileMode.Open))
            {
                profile = (List<CharacterProfile>)xs.Deserialize(fs);
            }

            Profiles = profile;
        }
    }
}