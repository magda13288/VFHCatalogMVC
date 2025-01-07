
﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
﻿using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Domain.Common;

namespace VFHCatalogMVC.Domain.Model
{
    public class AuditTrial
    {
        public Guid Id { get; set; }

        public string UserId { get; set; }

        public ApplicationUser? User { get; set; }

        public TrailType TrailType { get; set; }

        public DateTime DateUtc { get; set; }

        public string EntityName { get; set; }

        public string? PrimaryKey { get; set; }

        // JSON properties
        public string OldValuesJson { get; set; }
        public string NewValuesJson { get; set; }

        [NotMapped]
        public Dictionary<string, object?> OldValues
        {
            get
            {
                return string.IsNullOrEmpty(OldValuesJson)
                    ? new Dictionary<string, object?>()
                    : JsonConvert.DeserializeObject<Dictionary<string, object?>>(OldValuesJson);
            }
            set
            {
                OldValuesJson = JsonConvert.SerializeObject(value);
            }
        }

        [NotMapped]
        public Dictionary<string, object?> NewValues
        {
            get
            {
                return string.IsNullOrEmpty(NewValuesJson)
                    ? new Dictionary<string, object?>()
                    : JsonConvert.DeserializeObject<Dictionary<string, object?>>(NewValuesJson);
            }
            set
            {
                NewValuesJson = JsonConvert.SerializeObject(value);
            }
        }
        // Właściwość przechowywana w bazie danych jako JSON
        public string ChangedColumnsJson { get; set; }

        [NotMapped]
        public List<string> ChangedColumns
        {
            get
            {
                return string.IsNullOrEmpty(ChangedColumnsJson)
                    ? new List<string>() // if JSON is null, return null list
                    : JsonConvert.DeserializeObject<List<string>>(ChangedColumnsJson); // deseralize JSON
            }
            set
            {
                ChangedColumnsJson = JsonConvert.SerializeObject(value); // serailize to JSON
            }
        }
    }
}
