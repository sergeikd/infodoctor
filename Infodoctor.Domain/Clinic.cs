using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Infodoctor.Domain
{
    public class Clinic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public string Email { get; set; }
        public Guid OwnershipId { get; set; }
        [NotMapped]
        public List<Guid> ProfilesIds { get; set; }
        [NotMapped]
        public List<Guid> Branches { get; set; }
        [NotMapped]
        public Dictionary<string, string> Phones { get; set; } = new Dictionary<string, string>();

        [Obsolete("Only for EntityFramework")]
        public string ProfilesJson
        {
            get
            {
                return ProfilesIds == null || !ProfilesIds.Any() ? null : JsonConvert.SerializeObject(ProfilesIds);
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    ProfilesIds.Clear();
                else
                    ProfilesIds = JsonConvert.DeserializeObject<List<Guid>>(value);
            }
        }

        [Obsolete("Only for EntityFramework")]
        public string BranchesJson
        {
            get
            {
                return Branches == null || !Branches.Any() ? null : JsonConvert.SerializeObject(Branches);
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    Branches.Clear();
                else
                    Branches = JsonConvert.DeserializeObject<List<Guid>>(value);
            }
        }

        [Obsolete("Only for EntityFramework")]
        public string PhonesJson
        {
            get
            {
                return Phones == null || !Phones.Any() ? null : JsonConvert.SerializeObject(Phones);
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    Phones.Clear();
                else
                    Phones = JsonConvert.DeserializeObject<Dictionary<string, string>>(value);
            }
        }
    }
}
